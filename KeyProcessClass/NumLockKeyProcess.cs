using System.Drawing;

namespace CustomTenkey {

    public delegate void ChangeMapDelegate();

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class NumLockKeyProcess : IKeyProcess {
        [System.Xml.Serialization.XmlAttributeAttribute("pkey")]
        public string PhysicalKey { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public string Name { get { return "NumLock"; } set { } }

        [System.Xml.Serialization.XmlIgnore]
        public Color InterfaceColor { get { return Color.Plum; } }

        public static event ChangeMapDelegate  ChangeMapEvent = null;

        public NumLockKeyProcess() {
        }

        public void Leave() {
        }
        public void Press() {
            ChangeMapEvent();
        }
    }
}
