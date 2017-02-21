using System.Drawing;

namespace CustomTenkey {
    public interface IKeyProcess {
        string PhysicalKey { get; set; }
        string Name { get; set; }
        Color InterfaceColor { get; }
        void Press();
        void Leave();
    }
}
