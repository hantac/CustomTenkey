using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MadWizard.WinUSBNet;

namespace CustomTenkey {

    public delegate void RealhackKeyEventDelegate(List<ushort> keys);

    public class HIDKBDPseudoDriver {
        private ushort vid;
        private ushort pid;
        private Guid guid = new Guid("{3F0BA7CE-DFD0-4C9B-8ABD-E431D411B5DD}");
        private USBDeviceInfo deviceInfo = null;
        private USBDevice device = null;
        private USBInterface iface = null;
        private USBNotifier notifier = null;
        private bool isNumLock = false;
        private CancellationTokenSource cts = null;
        private CancellationTokenSource ctsNumLock = null;
        public event RealhackKeyEventDelegate KeyEvent = null;

        public void SetupUSBNotifier(Control control) {
            notifier = new USBNotifier(control, guid);
            notifier.Removal += USBEventHandler;
            notifier.Arrival += USBEventHandler;
            control.Select();
        }

        public void InitDevice() {
            InitDevice(0x0853, 0x0117);
        }

        public void InitDevice(ushort vid, ushort pid) {
            this.vid = vid;
            this.pid = pid;

            USBDeviceInfo[] details = USBDevice.GetDevices(guid);
            if (details == null) {
                return;
            }

            try {
                deviceInfo = details.First(info => info.VID == vid && info.PID == pid);
                if (deviceInfo != null) {
                    device = new USBDevice(deviceInfo);
                    iface = device.Interfaces.Find(USBBaseClass.HID);
                    enableNumLock();
                    StartMonitoring();
                }
            } catch (System.InvalidOperationException) {
                iface = null;
            }
        }

        public void DeinitDevice() {
            if (ctsNumLock != null && !ctsNumLock.IsCancellationRequested) {
                ctsNumLock.Cancel();
            }
            StopMonitoring();
            iface = null;
        }

        private void USBEventHandler(object sender, USBEvent e) {
            if (!e.Guid.Equals(this.guid)) {
                return;
            }
            if (e.Type == USBEventType.DeviceRemoval) {
                DeinitDevice();
            } else {
                InitDevice();
            }
        }

        //public void toggleNumLock() {
        //    if (!isNumLock) {
        //        enableNumLock();
        //    } else {
        //        disableNumLock();
        //    }
        //}

        private void controlNumLockLed(bool on) {
            if (iface == null) {
                return;
            }
            try {
                if (on) {
                    iface.Device.ControlOut(0x21, 0x09, 0x0200, 0x0000, new byte[] { 0x01 });
                } else {
                    iface.Device.ControlOut(0x21, 0x09, 0x0200, 0x0000, new byte[] { 0x00 });
                }
            } catch (USBException e) {
            }
        }

        public void enableNumLock() {
            controlNumLockLed(true);
            isNumLock = true;
        }

        public void disableNumLock() {
            controlNumLockLed(false);
            isNumLock = false;
        }

        private static ushort[][] blinkPattern = {
            new ushort[] { 1000 },
            null,
            new ushort[] { 50, 950, },
            new ushort[] { 50, 50, 50, 850, },
            new ushort[] { 50, 50, 50, 50, 50, 750, },
            new ushort[] { 500, 500, },
            new ushort[] { 250, 250, 250, 250, },
        };

        private BlockingCollection<ushort[]> queue = new BlockingCollection<ushort[]>();

        public void ChangeBlinkPattern(int ptnNum) {
            queue.Add(blinkPattern[ptnNum]);
            if (ctsNumLock == null) {
                ctsNumLock = new CancellationTokenSource();
                Task.Run(() => BlinkTask());
            }
        }

        private async void BlinkTask() {
            disableNumLock();
            int count = 0;
            ushort[] pattern = null;
            while (!ctsNumLock.IsCancellationRequested) {
                pattern = queue.Take();
                if (pattern == null) {
                    disableNumLock();
                    continue;
                } else if (pattern.Length == 1) {
                    enableNumLock();
                    continue;
                }

                disableNumLock();
                count = 0;
                while (true) {
                    if (queue.Count > 0) {
                        break;
                    }
                    controlNumLockLed(count % 2 == 0);
                    await Task.Delay(pattern[count]);
                    count++;
                    if (count == pattern.Length) {
                        count = 0;
                    }
                }
            }
            ctsNumLock.Dispose();
            ctsNumLock = null;
        }

        public void StartMonitoring() {
            if (cts == null) {
                cts = new CancellationTokenSource();
                Task.Run(() => KeyEventLoop());
            }
        }

        public void StopMonitoring() {
            if (cts != null && !cts.IsCancellationRequested) {
                cts.Cancel();
                //iface.InPipe.Abort();
            }
        }

        private void KeyEventLoop() {
            byte[] inkey = new byte[iface.InPipe.MaximumPacketSize];
            byte[] processed = new byte[iface.InPipe.MaximumPacketSize];
            byte[][] buff = new byte[2][] { new byte[6], new byte[6] };
            List<ushort> keycode = new List<ushort>();

            byte modpress;
            byte modleave;

            iface.InPipe.Flush();
            while (!cts.IsCancellationRequested) {
                keycode.Clear();

                try {
                    iface.InPipe.Read(inkey);
                } catch (USBException) {
                    break;
                }

                modleave = (byte)((processed[0] ^ inkey[0]) & processed[0]);
                if (modleave > 0x00) {
                    keycode.Add((ushort)(0x0200 | modleave));
                }

                modpress = (byte)((processed[0] ^ inkey[0]) & inkey[0]);
                if (modpress > 0x00) {
                    keycode.Add((ushort)(0x0300 | modpress));
                }

                Array.Copy(processed, 2, buff[0], 0, 6);
                Array.Copy(inkey, 2, buff[1], 0, 6);

                for (int i = 0; i < buff[0].Length; i++) {
                    if (buff[0][i] == 0x00) {
                        break;
                    }
                    for (int j = 0; j <= i; j++) {
                        if (buff[0][i] == buff[1][j]) {
                            buff[0][i] = 0x00;
                            buff[1][j] = 0x00;
                            break;
                        }
                    }
                }

                for (int i = 0; i < buff[0].Length; i++) {
                    if (buff[0][i] != 0x00) {
                        keycode.Add(buff[0][i]);
                    }
                }

                for (int j = 0; j < buff[1].Length; j++) {
                    if (buff[1][j] != 0x00) {
                        keycode.Add((ushort)(0x0100 | buff[1][j]));
                    }
                }
                if (keycode.Count > 0 && KeyEvent != null) {
                    KeyEvent(keycode);
                }

                inkey.CopyTo(processed, 0);
            }
            cts.Dispose();
            cts = null;
        }

    }
}
