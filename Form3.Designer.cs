namespace CustomTenkey {
    partial class Form3 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name=disposing>true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.cmbVKey = new System.Windows.Forms.ComboBox();
            this.gbGeneric = new System.Windows.Forms.GroupBox();
            this.tbKeySelector = new System.Windows.Forms.TextBox();
            this.cbxShift = new System.Windows.Forms.CheckBox();
            this.cbxAlt = new System.Windows.Forms.CheckBox();
            this.cbxCtrl = new System.Windows.Forms.CheckBox();
            this.gbKeyStroke = new System.Windows.Forms.GroupBox();
            this.tbKeyStroke = new System.Windows.Forms.TextBox();
            this.radioGeneric = new System.Windows.Forms.RadioButton();
            this.radioKeyStroke = new System.Windows.Forms.RadioButton();
            this.radioLaunch = new System.Windows.Forms.RadioButton();
            this.gbLaunch = new System.Windows.Forms.GroupBox();
            this.tbLaunchArgs = new System.Windows.Forms.TextBox();
            this.btnLaunchFileSelect = new System.Windows.Forms.Button();
            this.tbLaunchFile = new System.Windows.Forms.TextBox();
            this.radioNull = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.gbGeneric.SuspendLayout();
            this.gbKeyStroke.SuspendLayout();
            this.gbLaunch.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbVKey
            // 
            this.cmbVKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVKey.FormattingEnabled = true;
            this.cmbVKey.Location = new System.Drawing.Point(6, 40);
            this.cmbVKey.Name = "cmbVKey";
            this.cmbVKey.Size = new System.Drawing.Size(198, 20);
            this.cmbVKey.TabIndex = 2;
            // 
            // gbGeneric
            // 
            this.gbGeneric.Controls.Add(this.tbKeySelector);
            this.gbGeneric.Controls.Add(this.cbxShift);
            this.gbGeneric.Controls.Add(this.cbxAlt);
            this.gbGeneric.Controls.Add(this.cbxCtrl);
            this.gbGeneric.Controls.Add(this.cmbVKey);
            this.gbGeneric.Location = new System.Drawing.Point(27, 38);
            this.gbGeneric.Name = "gbGeneric";
            this.gbGeneric.Size = new System.Drawing.Size(245, 70);
            this.gbGeneric.TabIndex = 5;
            this.gbGeneric.TabStop = false;
            this.gbGeneric.Text = "Generic";
            // 
            // tbKeySelector
            // 
            this.tbKeySelector.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbKeySelector.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tbKeySelector.Location = new System.Drawing.Point(210, 40);
            this.tbKeySelector.MaxLength = 1;
            this.tbKeySelector.Name = "tbKeySelector";
            this.tbKeySelector.Size = new System.Drawing.Size(23, 19);
            this.tbKeySelector.TabIndex = 4;
            this.tbKeySelector.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbKeySelector.TextChanged += new System.EventHandler(this.tbKeySelector_TextChanged);
            this.tbKeySelector.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbKeySelector_KeyDown);
            // 
            // cbxShift
            // 
            this.cbxShift.AutoSize = true;
            this.cbxShift.Location = new System.Drawing.Point(100, 18);
            this.cbxShift.Name = "cbxShift";
            this.cbxShift.Size = new System.Drawing.Size(48, 16);
            this.cbxShift.TabIndex = 3;
            this.cbxShift.Text = "Shift";
            this.cbxShift.UseVisualStyleBackColor = true;
            // 
            // cbxAlt
            // 
            this.cbxAlt.AutoSize = true;
            this.cbxAlt.Location = new System.Drawing.Point(55, 18);
            this.cbxAlt.Name = "cbxAlt";
            this.cbxAlt.Size = new System.Drawing.Size(39, 16);
            this.cbxAlt.TabIndex = 3;
            this.cbxAlt.Text = "Alt";
            this.cbxAlt.UseVisualStyleBackColor = true;
            // 
            // cbxCtrl
            // 
            this.cbxCtrl.AutoSize = true;
            this.cbxCtrl.Location = new System.Drawing.Point(6, 18);
            this.cbxCtrl.Name = "cbxCtrl";
            this.cbxCtrl.Size = new System.Drawing.Size(43, 16);
            this.cbxCtrl.TabIndex = 3;
            this.cbxCtrl.Text = "Ctrl";
            this.cbxCtrl.UseVisualStyleBackColor = true;
            // 
            // gbKeyStroke
            // 
            this.gbKeyStroke.Controls.Add(this.tbKeyStroke);
            this.gbKeyStroke.Location = new System.Drawing.Point(27, 114);
            this.gbKeyStroke.Name = "gbKeyStroke";
            this.gbKeyStroke.Size = new System.Drawing.Size(245, 93);
            this.gbKeyStroke.TabIndex = 5;
            this.gbKeyStroke.TabStop = false;
            this.gbKeyStroke.Text = "KeyStroke";
            // 
            // tbKeyStroke
            // 
            this.tbKeyStroke.Location = new System.Drawing.Point(6, 18);
            this.tbKeyStroke.Multiline = true;
            this.tbKeyStroke.Name = "tbKeyStroke";
            this.tbKeyStroke.Size = new System.Drawing.Size(233, 66);
            this.tbKeyStroke.TabIndex = 4;
            // 
            // radioGeneric
            // 
            this.radioGeneric.AutoSize = true;
            this.radioGeneric.Location = new System.Drawing.Point(7, 68);
            this.radioGeneric.Name = "radioGeneric";
            this.radioGeneric.Size = new System.Drawing.Size(14, 13);
            this.radioGeneric.TabIndex = 7;
            this.radioGeneric.TabStop = true;
            this.radioGeneric.UseVisualStyleBackColor = true;
            this.radioGeneric.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // radioKeyStroke
            // 
            this.radioKeyStroke.AutoSize = true;
            this.radioKeyStroke.Location = new System.Drawing.Point(7, 158);
            this.radioKeyStroke.Name = "radioKeyStroke";
            this.radioKeyStroke.Size = new System.Drawing.Size(14, 13);
            this.radioKeyStroke.TabIndex = 7;
            this.radioKeyStroke.TabStop = true;
            this.radioKeyStroke.UseVisualStyleBackColor = true;
            this.radioKeyStroke.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // radioLaunch
            // 
            this.radioLaunch.AutoSize = true;
            this.radioLaunch.Location = new System.Drawing.Point(7, 246);
            this.radioLaunch.Name = "radioLaunch";
            this.radioLaunch.Size = new System.Drawing.Size(14, 13);
            this.radioLaunch.TabIndex = 7;
            this.radioLaunch.TabStop = true;
            this.radioLaunch.UseVisualStyleBackColor = true;
            this.radioLaunch.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // gbLaunch
            // 
            this.gbLaunch.Controls.Add(this.tbLaunchArgs);
            this.gbLaunch.Controls.Add(this.btnLaunchFileSelect);
            this.gbLaunch.Controls.Add(this.tbLaunchFile);
            this.gbLaunch.Location = new System.Drawing.Point(27, 213);
            this.gbLaunch.Name = "gbLaunch";
            this.gbLaunch.Size = new System.Drawing.Size(239, 74);
            this.gbLaunch.TabIndex = 5;
            this.gbLaunch.TabStop = false;
            this.gbLaunch.Text = "Launch";
            // 
            // tbLaunchArgs
            // 
            this.tbLaunchArgs.Location = new System.Drawing.Point(6, 47);
            this.tbLaunchArgs.Name = "tbLaunchArgs";
            this.tbLaunchArgs.Size = new System.Drawing.Size(169, 19);
            this.tbLaunchArgs.TabIndex = 6;
            // 
            // btnLaunchFileSelect
            // 
            this.btnLaunchFileSelect.Location = new System.Drawing.Point(181, 43);
            this.btnLaunchFileSelect.Name = "btnLaunchFileSelect";
            this.btnLaunchFileSelect.Size = new System.Drawing.Size(52, 23);
            this.btnLaunchFileSelect.TabIndex = 5;
            this.btnLaunchFileSelect.Text = "Select";
            this.btnLaunchFileSelect.UseVisualStyleBackColor = true;
            this.btnLaunchFileSelect.Click += new System.EventHandler(this.btnLaunchFileSelect_Click);
            // 
            // tbLaunchFile
            // 
            this.tbLaunchFile.Location = new System.Drawing.Point(6, 18);
            this.tbLaunchFile.Name = "tbLaunchFile";
            this.tbLaunchFile.Size = new System.Drawing.Size(227, 19);
            this.tbLaunchFile.TabIndex = 4;
            // 
            // radioNull
            // 
            this.radioNull.AutoSize = true;
            this.radioNull.Location = new System.Drawing.Point(7, 309);
            this.radioNull.Name = "radioNull";
            this.radioNull.Size = new System.Drawing.Size(14, 13);
            this.radioNull.TabIndex = 7;
            this.radioNull.TabStop = true;
            this.radioNull.UseVisualStyleBackColor = true;
            this.radioNull.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(197, 336);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(54, 12);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(218, 19);
            this.tbName.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "Not set";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(5, 15);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(34, 12);
            this.labelName.TabIndex = 9;
            this.labelName.Text = "Name";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 371);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.radioNull);
            this.Controls.Add(this.radioLaunch);
            this.Controls.Add(this.radioKeyStroke);
            this.Controls.Add(this.radioGeneric);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gbLaunch);
            this.Controls.Add(this.gbKeyStroke);
            this.Controls.Add(this.gbGeneric);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form3";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.gbGeneric.ResumeLayout(false);
            this.gbGeneric.PerformLayout();
            this.gbKeyStroke.ResumeLayout(false);
            this.gbKeyStroke.PerformLayout();
            this.gbLaunch.ResumeLayout(false);
            this.gbLaunch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbVKey;
        private System.Windows.Forms.GroupBox gbGeneric;
        private System.Windows.Forms.GroupBox gbKeyStroke;
        private System.Windows.Forms.TextBox tbKeyStroke;
        private System.Windows.Forms.CheckBox cbxShift;
        private System.Windows.Forms.CheckBox cbxAlt;
        private System.Windows.Forms.CheckBox cbxCtrl;
        private System.Windows.Forms.RadioButton radioGeneric;
        private System.Windows.Forms.RadioButton radioKeyStroke;
        private System.Windows.Forms.RadioButton radioLaunch;
        private System.Windows.Forms.GroupBox gbLaunch;
        private System.Windows.Forms.Button btnLaunchFileSelect;
        private System.Windows.Forms.TextBox tbLaunchFile;
        private System.Windows.Forms.RadioButton radioNull;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbKeySelector;
        private System.Windows.Forms.TextBox tbLaunchArgs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelName;
    }
}