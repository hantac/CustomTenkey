using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CustomTenkey {
    public partial class Form1 : Form {

        private Form2 form2 = null;
        private TenkeyController controller = null;

        public Form1(TenkeyController controller) {
            Application.ApplicationExit += new EventHandler(ApplicationExit);
            InitializeComponent();
            this.controller = controller;
            TenkeyController.UIForm = this;
        }

        private void exit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void save_Click(object sender, EventArgs e) {
            controller.SaveMapList();
        }

        private void load_Click(object sender, EventArgs e) {
            controller.LoadDefault();
        }

        private void ApplicationExit(object sender, EventArgs e) {
            rfIcon1.Dispose();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left) {
                return;
            }
            controller.ChangeMap();
        }
        
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (form2 == null || form2.IsDisposed) {
                form2 = new Form2(controller);
                if (form2.ShowDialog(this) == DialogResult.OK) {
                    controller.SetKeyMapList(form2.MapList);
                }
                form2.Dispose();
                form2 = null;
            } else {
                form2.Focus();
            }
        }

        private Icon[] iconList = {
            global::CustomTenkey.Properties.Resources.N,
            global::CustomTenkey.Properties.Resources.U,
            global::CustomTenkey.Properties.Resources._1,
            global::CustomTenkey.Properties.Resources._2,
            global::CustomTenkey.Properties.Resources._3,
            global::CustomTenkey.Properties.Resources._4,
            global::CustomTenkey.Properties.Resources._5,
        };

        public void ChangeIcon(int map) {
            if (map > 6) {
                return;
            }
            this.rfIcon1.Icon = iconList[map];
        }


        private void keyMapStripMenuItem_Click(object sender, EventArgs e) {
            int index = contextMenuStrip1.Items.IndexOf((ToolStripItem)sender);
            controller.SelectMap(index);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {
            List<KeyMap> maplist = controller.GetKeyMapList();
            contextMenuStrip1.Items.Clear();
            foreach (KeyMap map in maplist) {
                ToolStripMenuItem item = new ToolStripMenuItem(    map.Name, null, keyMapStripMenuItem_Click);
                item.Checked = map.InUse;
                contextMenuStrip1.Items.Add(item);
            }
            contextMenuStrip1.Items.Add(new ToolStripSeparator());
            contextMenuStrip1.Items.Add(new ToolStripMenuItem("Save", null, save_Click));
            contextMenuStrip1.Items.Add(new ToolStripMenuItem("Load Default", null, load_Click));
            contextMenuStrip1.Items.Add(new ToolStripSeparator());
            contextMenuStrip1.Items.Add(new ToolStripMenuItem("Settings", null, settingsToolStripMenuItem_Click));
            contextMenuStrip1.Items.Add(new ToolStripSeparator());
            contextMenuStrip1.Items.Add(new ToolStripMenuItem("Exit", null, exit_Click));
        }
    }
}

