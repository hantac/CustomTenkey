using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomTenkey {
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public class KeyMap : ICloneable {

        [System.Xml.Serialization.XmlElement("GenericKeyProcess", typeof(GenericKeyProcess))]
        [System.Xml.Serialization.XmlElement("NullKeyProcess", typeof(NullKeyProcess))]
        [System.Xml.Serialization.XmlElement("NumLockKeyProcess", typeof(NumLockKeyProcess))]
        [System.Xml.Serialization.XmlElement("KeyStrokeProcess", typeof(KeyStrokeProcess))]
        [System.Xml.Serialization.XmlElement("LaunchProcess", typeof(LaunchProcess))]
        public object[] Key {
            get {
                List<object> list = new List<object>();
                foreach (KeyValuePair<ushort, IKeyProcess> p in Map.ToArray()) {
                    p.Value.PhysicalKey = string.Format("0x{0:X2}", p.Key);
                    list.Add(p.Value);
                }

                return list.ToArray();
            }
            set {
                Dictionary<ushort, IKeyProcess> map = new Dictionary<ushort, IKeyProcess>();
                foreach (IKeyProcess k in value) {
                    map.Add(Convert.ToUInt16(k.PhysicalKey, 16), k);
                }
                Map = map;
            }
        }

        [System.Xml.Serialization.XmlAttribute("name")]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlAttribute("permanent")]
        public bool IsPermanent { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public bool InUse { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<ushort, IKeyProcess> Map { get; set; }

        public KeyMap(string name, bool permanent, Dictionary<ushort, IKeyProcess> map) {
            Name = name;
            IsPermanent = permanent;
            Map = map;
        }
        public KeyMap() {
        }

        public override string ToString() {
            return Name;
        }

        public object Clone() {
            return new KeyMap(Name, IsPermanent, new Dictionary<ushort, IKeyProcess>(Map));
        }
    }
}
