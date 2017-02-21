using System.Drawing;

namespace CustomTenkey {
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public class NullKeyProcess : IKeyProcess {
        [System.Xml.Serialization.XmlAttribute("pkey")]
        public string PhysicalKey { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public string Name { get { return "None"; } set { } }

        [System.Xml.Serialization.XmlIgnore]
        public Color InterfaceColor { get { return Color.DarkGray; } }

        public NullKeyProcess() {
        }

        public void Leave() {
        }

        public void Press() {
        }
    }
}
