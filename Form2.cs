using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CustomTenkey {
    partial class Form2 : Form {

        public List<KeyMap> MapList { get; }
        private Dictionary<ushort, Button> buttons = new Dictionary<ushort, Button>();
        private TenkeyController controller;

        private static readonly ushort[] codelist = new ushort[] {
            0x29, 0x1B, 0x06, 0x19,
            0x53, 0x54, 0x55, 0x2A,
            0x5F, 0x60, 0x61, 0x56,
            0x5C, 0x5D, 0x5E, 0x57,
            0x59, 0x5A, 0x5B, 0x58,
            0x62, 0x6262, 0x63,
        };

        public Form2(TenkeyController controller) {
            InitializeComponent();
            InitButton();

            this.controller = controller;

            MapList = new List<KeyMap>();
            foreach (KeyMap map in controller.GetKeyMapList()) {
                MapList.Add((KeyMap)map.Clone());
            }

            cmbKeyMapList.Items.AddRange(MapList.ToArray());
            cmbKeyMapList.SelectedIndex = 0;
            tbMapName.Text = ((KeyMap)cmbKeyMapList.SelectedItem).Name;
        }

        private void AddKeyList() {
            if (MapList.Count >= 7) {
                return;
            }
            KeyMap map = new KeyMap("New Map", false, controller.addNewMap());
            MapList.Add(map);
            cmbKeyMapList.Items.Add(map);
            cmbKeyMapList.SelectedIndex = cmbKeyMapList.Items.Count - 1;
        }

        private void DelKeyList() {
            int index = cmbKeyMapList.SelectedIndex;
            cmbKeyMapList.Items.RemoveAt(index);
            MapList.RemoveAt(index);
            if (cmbKeyMapList.Items.Count == index) {
                index--;
            }
            cmbKeyMapList.SelectedIndex = index;
        }
        
        private void InitButton() {
            foreach (ushort key in codelist) {
                Button btn = new Button();
                btn.Dock = DockStyle.Fill;
                btn.Click += ButtonClick;
                btn.Text = "None";
                buttons.Add(key, btn);
                Control[] ctrls = this.Controls.Find("pnl0x" + key.ToString("X2"), true);
                foreach(Control ctrl in ctrls) {
                    ctrl.Controls.Add(btn);
                }
            }
        }

        private void UpdateButtons() {
            KeyMap maps = (KeyMap)cmbKeyMapList.SelectedItem;
            foreach (KeyValuePair<ushort, IKeyProcess> map in maps.Map) {
                if (!buttons.ContainsKey(map.Key)) {
                    continue;
                }
                buttons[map.Key].Text = map.Value.Name;
                buttons[map.Key].BackColor = map.Value.InterfaceColor;
            }
        }

        Form3 form = null;
        private void ButtonClick(object sender, EventArgs e) {
            if (form == null || form.IsDisposed) {
                ushort key = buttons.First(x => x.Value.Equals(sender)).Key;
                IKeyProcess keyproc = MapList[cmbKeyMapList.SelectedIndex].Map[key];
                form = new Form3(keyproc);
                if (form.ShowDialog() == DialogResult.OK) {
                    IKeyProcess proc = form.GetKeyProcess();
                    MapList[cmbKeyMapList.SelectedIndex].Map[key] = proc;
                    cmbKeyMapList.Refresh();
                }
                form.Dispose();
                form = null;
                UpdateButtons();
            }
        }

        private void cmbKeyMapList_SelectedValueChanged(object sender, EventArgs e) {
            UpdateButtons();
            KeyMap km = (KeyMap)cmbKeyMapList.SelectedItem;
            tbMapName.Text = km.Name;
            btnDel.Enabled = !km.IsPermanent;
        }

        private void tbMapName_TextChanged(object sender, EventArgs e) {
            KeyMap map = (KeyMap)cmbKeyMapList.Items[cmbKeyMapList.SelectedIndex];
            map.Name = tbMapName.Text;
            cmbKeyMapList.Items[cmbKeyMapList.SelectedIndex] = map;
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            AddKeyList();
        }

        private void btnDel_Click(object sender, EventArgs e) {
            DelKeyList();
        }
    }
}
