using System;
using System.Windows.Forms;

namespace CustomTenkey {
    static class Program {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TenkeyController controlelr = new TenkeyController();

            Form1 form = new Form1(controlelr);

            HIDKBDPseudoDriver driver = new HIDKBDPseudoDriver();
            driver.SetupUSBNotifier(form);
            driver.InitDevice();

            controlelr.SetupPseudoDriver(driver);

            Application.Run();
        }
    }
}
