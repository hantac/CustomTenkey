using System.Drawing;
using System.Diagnostics;

namespace CustomTenkey {

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class LaunchProcess : IKeyProcess {

        [System.Xml.Serialization.XmlAttribute("pkey")]
        public string PhysicalKey { get; set; }

        [System.Xml.Serialization.XmlAttribute("name")]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlElement("File")]
        public string File { get; set; }

        [System.Xml.Serialization.XmlElement("Args")]
        public string Args { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public Color InterfaceColor { get { return Color.LavenderBlush; } }

        public LaunchProcess() {
            this.Name = "Launch";
        }

        public LaunchProcess(string name,  string file, string args) {
            Name = name;
            File = file;
            Args = args;
        }

        public void Leave() {
        }

        public void Press() {
            ProcessStartInfo psi = new ProcessStartInfo(File, Args);
            Process p = Process.Start(psi);
        }
    }
}
