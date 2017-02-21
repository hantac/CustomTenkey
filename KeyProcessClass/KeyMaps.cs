
namespace CustomTenkey {
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public class KeyMaps {
        [System.Xml.Serialization.XmlElement("KeyMap")]
        public KeyMap[] KeyMap { get; set; }
    }
}
