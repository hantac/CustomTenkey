using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace CustomTenkey {
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public class GenericKeyProcess : IKeyProcess {
        [System.Xml.Serialization.XmlAttribute("pkey")]
        public string PhysicalKey { get; set; }

        [System.Xml.Serialization.XmlAttribute("name")]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlAttribute("lkey")]
        public string LogicalKey {
            get {
                return string.Format("0x{0:X2}", KeyCode);
            }
            set {
                KeyCode = Convert.ToByte(value, 16);
            }
        }

        [System.Xml.Serialization.XmlAttribute("ctrl")]
        public bool UseCtrl { get; set; }

        [System.Xml.Serialization.XmlAttribute("alt")]
        public bool UseAlt { get; set; }

        [System.Xml.Serialization.XmlAttribute("shift")]
        public bool UseShift { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public byte KeyCode { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        private CancellationTokenSource cts = null;

        [System.Xml.Serialization.XmlIgnore]
        public Color InterfaceColor { get { return Color.NavajoWhite; } }

        public GenericKeyProcess(string Name, byte KeyCode, bool UseCtrl, bool UseShift, bool UseAlt) {
            this.Name = Name;
            this.KeyCode = KeyCode;
            this.UseCtrl = UseCtrl;
            this.UseShift = UseShift;
            this.UseAlt = UseAlt;
        }

        public GenericKeyProcess(string Name, byte KeyCode) : this(Name, KeyCode, false, false, false) {
        }

        public GenericKeyProcess() {
        }

        public void Leave() {
            if (cts != null) {
                cts.Cancel();
            }
            InputIssuer.sendKeyUp(KeyCode, UseCtrl, UseShift, UseAlt);
        }

        public void Press() {
            press(true);
            if (cts == null) {
                cts = new CancellationTokenSource();
                Task.Run(() => loop());
            }
        }

        private void press(bool modify) {
            if (modify) {
                InputIssuer.sendKeyDown(KeyCode, UseCtrl, UseShift, UseAlt);
            } else {
                InputIssuer.sendKeyDown(KeyCode, false, false, false);
            }
        }

        private void loop() {
            Thread.Sleep(400);
            while (!cts.IsCancellationRequested) {
                Thread.Sleep(10);
                press(false);
            }
            cts.Dispose();
            cts = null;
        }
    }
}
