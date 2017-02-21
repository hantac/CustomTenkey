using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CustomTenkey {
    class InputIssuer {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 SendInput(UInt32 nInputs, INPUT[] pInputs, Int32 cbSize);

        [DllImport("user32.dll")]
        public static extern UIntPtr GetMessageExtraInfo();

        [DllImport("user32.dll")]
        public static extern UInt32 MapVirtualKey(UInt32 uCode, UInt32 uMapType);

        [DllImport("user32.dll")]
        public static extern Int16 GetKeyState(Int32 vKey);

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT {
            public Int32 dx;
            public Int32 dy;
            public UInt32 mouseData;
            public UInt32 dwFlags;
            public UInt32 time;
            public UIntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT {
            public UInt16 wVk;
            public UInt16 wScan;
            public UInt32 dwFlags;
            public UInt32 time;
            public UIntPtr dwExtraInfo;
            public KEYBDINPUT(UInt16 _wVk, UInt16 _wScan, UInt32 _dwFlags, UInt32 _time) {
                wVk = _wVk;
                wScan = _wScan;
                dwFlags = _dwFlags;
                time = _time;
                dwExtraInfo = GetMessageExtraInfo();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT {
            UInt32 uMsg;
            UInt16 wParamL;
            UInt16 wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT {
            [FieldOffset(0)]
            public UInt32 type;
            [FieldOffset(4)] //*
            public MOUSEINPUT mi;
            [FieldOffset(4)] //*
            public KEYBDINPUT ki;
            [FieldOffset(4)] //*
            public HARDWAREINPUT hi;
            public INPUT(UInt32 _type, UInt16 _wVk, UInt16 _wScan, UInt32 _dwFlags, UInt32 _time) {
                type = _type;
                mi = new MOUSEINPUT();
                hi = new HARDWAREINPUT();
                ki = new KEYBDINPUT(_wVk, _wScan, _dwFlags, _time);
            }
        }

        const uint KEYEVENTIF_KEYDOWN = 0x0000;
        const uint KEYEVENTIF_KEYUP = 0x0002;
        const int LLKHF_EXTENDED = 0x00000001;
        const int LLKHF_INJECTED = 0x00000010;
        const int LLKHF_ALTDOWN = 0x00000020;
        const int LLKHF_UP = 0x00000080;
        const int INPUT_KEYBOARD = 1;
        const int VK_OEM_1 = 0xBA;
        const int VK_OEM_PLUS = 0xBB;
        const int VK_OEM_COMMA = 0xBC;
        const int VK_OEM_MINUS = 0xBD;
        const int VK_OEM_PERIOD = 0xBE;
        const int VK_OEM_2 = 0xBF;
        const int VK_OEM_3 = 0xC0;
        const int VK_OEM_4 = 0xDB;
        const int VK_OEM_5 = 0xDC;
        const int VK_OEM_6 = 0xDD;
        const int VK_OEM_7 = 0xDE;
        const int VK_OEM_8 = 0xDF;
        const int VK_SHIFT = 0x10;
        const int VK_CONTROL = 0x11;
        const int VK_MENU = 0x12;
        const int VK_LWIN = 0x5B;
        const int VK_RWIN = 0x5C;
        const int VK_APPS = 0x5D;
        const int VK_LSHIFT = 0xA0;
        const int VK_RSHIFT = 0xA1;
        const int VK_LCONTROL = 0xA2;
        const int VK_RCONTROL = 0xA3;
        const int VK_LMENU = 0xA4;
        const int VK_RMENU = 0xA5;

        static INPUT[] key = new INPUT[4] {
                new INPUT(INPUT_KEYBOARD, ' ', 0, KEYEVENTIF_KEYDOWN, 0),
                new INPUT(INPUT_KEYBOARD, ' ', 0, KEYEVENTIF_KEYDOWN, 0),
                new INPUT(INPUT_KEYBOARD, ' ', 0, KEYEVENTIF_KEYDOWN, 0),
                new INPUT(INPUT_KEYBOARD, ' ', 0, KEYEVENTIF_KEYDOWN, 0),
        };

        public static void sendKeyDown(byte code, bool ctrl, bool shift, bool alt) {
            uint i = 0;
            if (ctrl) {
                key[i].ki.wVk = VK_LCONTROL;
                key[i].ki.wScan = (ushort)MapVirtualKey(VK_LCONTROL, 0);
                key[i].ki.dwFlags = KEYEVENTIF_KEYDOWN;
                i++;
            }
            if (shift) {
                key[i].ki.wVk = VK_LSHIFT;
                key[i].ki.wScan = (ushort)MapVirtualKey(VK_LSHIFT, 0);
                key[i].ki.dwFlags = KEYEVENTIF_KEYDOWN;
                i++;
            }
            if (alt) {
                key[i].ki.wVk = VK_LMENU;
                key[i].ki.wScan = (ushort)MapVirtualKey(VK_LMENU, 0);
                key[i].ki.dwFlags = KEYEVENTIF_KEYDOWN;
                i++;
            }
            key[i].ki.wVk = code;
            key[i].ki.wScan = (ushort)MapVirtualKey(code, 0);
            key[i].ki.dwFlags = KEYEVENTIF_KEYDOWN;
            SendInput(i + 1, key, Marshal.SizeOf(typeof(INPUT)));
        }
        public static void sendKeyUp(byte code, bool ctrl, bool shift, bool alt) {
            uint i = 0;
            if (ctrl) {
                key[i].ki.wVk = VK_LCONTROL;
                key[i].ki.wScan = (ushort)MapVirtualKey(VK_LCONTROL, 0);
                key[i].ki.dwFlags = KEYEVENTIF_KEYUP;
                i++;
            }
            if (shift) {
                key[i].ki.wVk = VK_LSHIFT;
                key[i].ki.wScan = (ushort)MapVirtualKey(VK_LSHIFT, 0);
                key[i].ki.dwFlags = KEYEVENTIF_KEYUP;
                i++;
            }
            if (alt) {
                key[i].ki.wVk = VK_LMENU;
                key[i].ki.wScan = (ushort)MapVirtualKey(VK_LMENU, 0);
                key[i].ki.dwFlags = KEYEVENTIF_KEYUP;
                i++;
            }
            key[i].ki.wVk = code;
            key[i].ki.wScan = (ushort)MapVirtualKey(code, 0);
            key[i].ki.dwFlags = KEYEVENTIF_KEYUP;
            SendInput(i + 1, key, Marshal.SizeOf(typeof(INPUT)));
        }

        public static INPUT[] createKeyStroke(byte[] keylist) {
            List<INPUT> inputList = new List<INPUT>();
            foreach (byte key in keylist) {
                inputList.Add(new INPUT(INPUT_KEYBOARD, key, (ushort)MapVirtualKey(key, 0), KEYEVENTIF_KEYDOWN, 0));
            }
            foreach (byte key in keylist) {
                inputList.Add(new INPUT(INPUT_KEYBOARD, key, (ushort)MapVirtualKey(key, 0), KEYEVENTIF_KEYUP, 0));
            }
            return inputList.ToArray();
        }

        public static void sendInput(INPUT[] inputlist) {
            SendInput((uint)inputlist.Length, inputlist, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
