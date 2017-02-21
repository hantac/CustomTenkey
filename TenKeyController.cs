using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CustomTenkey {

    public class TenkeyController {
        List<KeyMap> keylist = new List<KeyMap>();

        private HIDKBDPseudoDriver PseudoDriver { get; set; }
        private int nowMap = 0;
        public static Form1 UIForm { get; set; }

        public TenkeyController() {
            LoadMapList();
        }

        public void SetupPseudoDriver(HIDKBDPseudoDriver driver) {
            PseudoDriver = driver;
            PseudoDriver.KeyEvent += KeyEventHandler;
            NumLockKeyProcess.ChangeMapEvent += ChangeMap;
        }

        private string GetSettingFilePath() {
            Environment.SpecialFolder sf = Environment.SpecialFolder.ApplicationData;
            string path = string.Format(
                    @"{0}\{1}", Environment.GetFolderPath(sf), Application.ProductName);

            lock (path) {
                if (!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }
            }

            return Path.Combine(path, "settings.xml");
        }

        private void LoadMapList() {
            string path = GetSettingFilePath();
            System.Xml.Serialization.XmlSerializer serializer = 
                    new System.Xml.Serialization.XmlSerializer(typeof(KeyMaps));
            KeyMaps maps = null;

            if (File.Exists(path)) {
                FileStream fs = new FileStream(path, FileMode.Open);
                maps = (KeyMaps)serializer.Deserialize(fs);
                fs.Close();
            } else {
                StringReader sr = new StringReader(Properties.Resources._default);
                maps = (KeyMaps)serializer.Deserialize(sr);
                sr.Close();
            }

            keylist = new List<KeyMap>(maps.KeyMap);
            keylist[0].InUse = true;
        }

        public void LoadDefault() {
            System.Xml.Serialization.XmlSerializer serializer = 
                    new System.Xml.Serialization.XmlSerializer(typeof(KeyMaps));
            StringReader sr = new StringReader(Properties.Resources._default);
            KeyMaps maps = (KeyMaps)serializer.Deserialize(sr);
            sr.Close();

            keylist = new List<KeyMap>(maps.KeyMap);
            keylist[0].InUse = true;
        }

        public void SaveMapList() {
            KeyMaps maps = new KeyMaps();
            maps.KeyMap = keylist.ToArray();

            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(KeyMaps));

            System.IO.StreamWriter sw = new System.IO.StreamWriter(
                    GetSettingFilePath(), false, new System.Text.UTF8Encoding(false));
            serializer.Serialize(sw, maps);
            sw.Close();
        }

        private byte[] convertModCode(byte code) {
            List<byte> list = new List<byte>();

            if ((code & 0x01) != 0x00) {
                list.Add(0xA2);
            }
            if ((code & 0x02) != 0x00) {
                list.Add(0xA0);
            }
            if ((code & 0x04) != 0x00) {
                list.Add(0xA4);
            }
            if ((code & 0x10) != 0x00) {
                list.Add(0xA3);
            }
            if ((code & 0x20) != 0x00) {
                list.Add(0xA1);
            }
            if ((code & 0x40) != 0x00) {
                list.Add(0xA5);
            }

            return list.ToArray();
        }

        private void KeyEventHandler(List<ushort> keys) {
            foreach (ushort i in keys) {
                if (((i >> 8) & 0x02) != 0x00) { // modify
                    foreach (byte j in convertModCode((byte)(i & 0xFF))) {
                        if ((i & 0x0100) > 0) {
                            KeyPress(j);
                        } else {
                            KeyLeave(j);
                        }
                    }
                } else {
                    if ((i & 0x0100) > 0) {
                        KeyPress((byte)(i & 0xFF));
                    } else {
                        KeyLeave((byte)(i & 0xFF));
                    }
                }
            }
        }

        public void SelectMap(int index) {
            keylist[nowMap].InUse = false;
            nowMap = index;
            PseudoDriver.ChangeBlinkPattern(nowMap);
            UIForm.ChangeIcon(nowMap);
            keylist[nowMap].InUse = true;
        }

        public void ChangeMap() {
            int index = nowMap + 1;
            if (index == keylist.Count) {
                index = 0;
            }
            SelectMap(index);
        }

        public List<KeyMap> GetKeyMapList() {
            return keylist;
        }

        public void SetKeyMapList(List<KeyMap> list) {
            keylist = list;
        }

        private void KeyPress(ushort code) {
            keylist[nowMap].Map[code].Press();
        }

        private void KeyLeave(ushort code) {
            keylist[nowMap].Map[code].Leave();
        }

        public Dictionary<ushort, IKeyProcess> addNewMap() {
            Dictionary<ushort, IKeyProcess> dict;
            dict = new Dictionary<ushort, IKeyProcess>();
            dict.Add(0x62, new GenericKeyProcess("Num0", 0x60)); // Num0
            dict.Add(0x59, new GenericKeyProcess("Num1", 0x61)); // Num1
            dict.Add(0x5A, new GenericKeyProcess("Num2", 0x62)); // Num2
            dict.Add(0x5B, new GenericKeyProcess("Num3", 0x63)); // Num3
            dict.Add(0x5C, new GenericKeyProcess("Num4", 0x64)); // Num4
            dict.Add(0x5D, new GenericKeyProcess("Num5", 0x65)); // Num5
            dict.Add(0x5E, new GenericKeyProcess("Num6", 0x66)); // Num6
            dict.Add(0x5F, new GenericKeyProcess("Num7", 0x67)); // Num7
            dict.Add(0x60, new GenericKeyProcess("Num8", 0x68)); // Num8
            dict.Add(0x61, new GenericKeyProcess("Num9", 0x69)); // Num9
            dict.Add(0x63, new GenericKeyProcess(".", 0x6E)); // Num.
            dict.Add(0x54, new GenericKeyProcess("/", 0x6F)); // Num/
            dict.Add(0x55, new GenericKeyProcess("*", 0x6A)); // Num*
            dict.Add(0x2A, new GenericKeyProcess("=", 0xBB)); // Num=
            dict.Add(0x56, new GenericKeyProcess("-", 0x6D)); // Num-
            dict.Add(0x57, new GenericKeyProcess("+", 0x6B)); // Num+
            dict.Add(0x58, new GenericKeyProcess("Enter", 0x0D)); // NumEnter
            dict.Add(0x53, new NumLockKeyProcess()); // NumLock
            dict.Add(0x29, new GenericKeyProcess("ESC", 0x1B)); // ESC
            dict.Add(0x1B, new GenericKeyProcess("Cut", 0x58, true, false, false)); // Ctrl+X
            dict.Add(0x06, new GenericKeyProcess("Copy", 0x43, true, false, false)); // Ctrl+C
            dict.Add(0x19, new GenericKeyProcess("Paste", 0x56, true, false, false)); // Ctrl+V
            dict.Add(0xA0, new NullKeyProcess()); // Left SHIFT key
            dict.Add(0xA1, new NullKeyProcess()); // Right SHIFT key
            dict.Add(0xA2, new NullKeyProcess()); // Left CONTROL key
            dict.Add(0xA3, new NullKeyProcess()); // Right CONTROL key
            dict.Add(0xA4, new NullKeyProcess()); // Left ALT key
            dict.Add(0xA5, new NullKeyProcess()); // Right ALT key
            return dict;
        }
    }
}
