using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CustomTenkey {
    partial class Form3 : Form {

        private class CmbItem {
            public string NAME { get; }
            public uint VALUE { get; }
            public CmbItem(string name, uint value) {
                NAME = name;
                VALUE = value;
            }
            public override string ToString() {
                //return "[0x" + VALUE.ToString("X2") + "] " + NAME;
                return NAME;
            }
        }

        List<CmbItem> items = new List<CmbItem>();

        public Form3(IKeyProcess keyproc) {
            InitializeComponent();
            initCmbVkey();

            if (keyproc.GetType() == typeof(GenericKeyProcess)) {
                GenericKeyProcess proc = (GenericKeyProcess)keyproc;
                radioGeneric.Checked = true;
                tbName.Text = proc.Name;
                cbxCtrl.Checked = proc.UseCtrl;
                cbxAlt.Checked = proc.UseAlt;
                cbxShift.Checked = proc.UseShift;
                cmbVKey.SelectedItem = items.Find(x => x.VALUE == proc.KeyCode);
            } else if (keyproc.GetType() == typeof(KeyStrokeProcess)) {
                KeyStrokeProcess proc = (KeyStrokeProcess)keyproc;
                radioKeyStroke.Checked = true;
                tbName.Text = proc.Name;
                tbKeyStroke.Text = proc.GetStrokeString();
            } else if (keyproc.GetType() == typeof(LaunchProcess)) {
                LaunchProcess proc = (LaunchProcess)keyproc;
                radioLaunch.Checked = true;
                tbName.Text = proc.Name;
                tbLaunchFile.Text = proc.File;
                tbLaunchArgs.Text = proc.Args;
            } else if (keyproc.GetType() == typeof(NullKeyProcess)) {
                radioNull.Checked = true;
            }
        }

        private IKeyProcess generateGenericKeyProc() {
            CmbItem item = (CmbItem)cmbVKey.SelectedItem;
            ushort code = (ushort)item.VALUE;
            string name = tbName.Text;
            if (name.Equals("")) {
                //name = "[0x" + code.ToString("X2") + "]" + item.NAME;
                name = item.NAME;
            }

            return new GenericKeyProcess(name, (byte)code, cbxCtrl.Checked, cbxShift.Checked, cbxAlt.Checked);
        }

        private IKeyProcess generateKeyStrokeProc() {
            string name = tbName.Text;
            string str = tbKeyStroke.Text;
            if (name.Equals("")) {
                name = str;
            }

            return new KeyStrokeProcess(name, str);
        }

        private IKeyProcess generateLaunchProc() {
            string name = tbName.Text;
            string file = tbLaunchFile.Text;
            string args = tbLaunchArgs.Text;
            if (name.Equals("")) {
                name = "Launch";
            }

            return new LaunchProcess(name, file, args);
        }

        public IKeyProcess GetKeyProcess() {
            IKeyProcess proc = null;

            if (radioGeneric.Checked) {
                proc = generateGenericKeyProc();
            } else if (radioKeyStroke.Checked) {
                proc = generateKeyStrokeProc();
            } else if (radioLaunch.Checked) {
                proc = generateLaunchProc();
            } else if (radioNull.Checked) {
                proc = new NullKeyProcess();
            }

            return proc;
        }

        private void initCmbVkey() {
            string[] vkNames = Enum.GetNames(typeof(Keys));
            Array vkValues = Enum.GetValues(typeof(Keys));

            foreach (Keys val in vkValues) {
                if ((uint)val < 0xFFFF) {
                    items.Add(new CmbItem(Enum.GetName(typeof(Keys), val), (ushort)val));
                }
            }
            cmbVKey.Items.AddRange(items.ToArray());
        }

        private void changeRegionEnabled() {
            gbGeneric.Enabled = radioGeneric.Checked;
            gbKeyStroke.Enabled = radioKeyStroke.Checked;
            gbLaunch.Enabled = radioLaunch.Checked;
        }

        private void CheckedChanged(object sender, EventArgs e) {
            changeRegionEnabled();
        }

        private void tbKeySelector_KeyDown(object sender, KeyEventArgs e) {
            cbxCtrl.Checked = e.Control;
            cbxAlt.Checked = e.Alt;
            cbxShift.Checked = e.Shift;
            
            tbName.Text = (e.Control ? "Ctrl + " : "") + (e.Shift ? "Shift + " : "") + (e.Alt ? "Alt + " : "");
            if (e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.Menu && e.KeyCode != Keys.ControlKey) {
                CmbItem item = items.Find(x => x.VALUE == (uint)e.KeyCode);
                cmbVKey.SelectedItem = item;
                tbName.Text += item.NAME;
            }
        }

        private void tbKeySelector_TextChanged(object sender, EventArgs e) {
            tbKeySelector.Clear();
        }

        private void btnLaunchFileSelect_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\";

            if (ofd.ShowDialog() == DialogResult.OK) {
                tbLaunchFile.Text = ofd.FileName;
            }
        }
    }
}
