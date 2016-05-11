namespace chieviewer
{
    partial class OptionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageNgName = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.buttonNgNameOk = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkBoxNgNameRegex = new System.Windows.Forms.CheckBox();
            this.textBoxNgName = new System.Windows.Forms.TextBox();
            this.buttonDeleteNgName = new System.Windows.Forms.Button();
            this.buttonAddNgName = new System.Windows.Forms.Button();
            this.listBoxNgName = new System.Windows.Forms.ListBox();
            this.tabPageNgWord = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonNgWordOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxNgWordRegex = new System.Windows.Forms.CheckBox();
            this.textBoxNgWord = new System.Windows.Forms.TextBox();
            this.buttonDeleteNgWord = new System.Windows.Forms.Button();
            this.buttonAddNgWord = new System.Windows.Forms.Button();
            this.listBoxNgWord = new System.Windows.Forms.ListBox();
            this.labelNgWordError = new System.Windows.Forms.Label();
            this.labelNgNameError = new System.Windows.Forms.Label();
            this.buttonUpdateNgName = new System.Windows.Forms.Button();
            this.buttonUpdateNgWord = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageNgName.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPageNgWord.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageNgName);
            this.tabControl.Controls.Add(this.tabPageNgWord);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(486, 357);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageNgName
            // 
            this.tabPageNgName.Controls.Add(this.panel4);
            this.tabPageNgName.Controls.Add(this.panel3);
            this.tabPageNgName.Location = new System.Drawing.Point(4, 22);
            this.tabPageNgName.Name = "tabPageNgName";
            this.tabPageNgName.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNgName.Size = new System.Drawing.Size(478, 331);
            this.tabPageNgName.TabIndex = 0;
            this.tabPageNgName.Text = "NGネーム管理";
            this.tabPageNgName.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.buttonNgNameOk);
            this.panel4.Location = new System.Drawing.Point(8, 294);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(462, 29);
            this.panel4.TabIndex = 3;
            // 
            // buttonNgNameOk
            // 
            this.buttonNgNameOk.Location = new System.Drawing.Point(384, 3);
            this.buttonNgNameOk.Name = "buttonNgNameOk";
            this.buttonNgNameOk.Size = new System.Drawing.Size(75, 23);
            this.buttonNgNameOk.TabIndex = 5;
            this.buttonNgNameOk.Text = "OK";
            this.buttonNgNameOk.UseVisualStyleBackColor = true;
            this.buttonNgNameOk.Click += new System.EventHandler(this.buttonNgNameOk_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonUpdateNgName);
            this.panel3.Controls.Add(this.labelNgNameError);
            this.panel3.Controls.Add(this.checkBoxNgNameRegex);
            this.panel3.Controls.Add(this.textBoxNgName);
            this.panel3.Controls.Add(this.buttonDeleteNgName);
            this.panel3.Controls.Add(this.buttonAddNgName);
            this.panel3.Controls.Add(this.listBoxNgName);
            this.panel3.Location = new System.Drawing.Point(8, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(462, 282);
            this.panel3.TabIndex = 2;
            // 
            // checkBoxNgNameRegex
            // 
            this.checkBoxNgNameRegex.AutoSize = true;
            this.checkBoxNgNameRegex.Location = new System.Drawing.Point(310, 233);
            this.checkBoxNgNameRegex.Name = "checkBoxNgNameRegex";
            this.checkBoxNgNameRegex.Size = new System.Drawing.Size(124, 16);
            this.checkBoxNgNameRegex.TabIndex = 4;
            this.checkBoxNgNameRegex.Text = "正規表現を使用する";
            this.checkBoxNgNameRegex.UseVisualStyleBackColor = true;
            // 
            // textBoxNgName
            // 
            this.textBoxNgName.Location = new System.Drawing.Point(4, 231);
            this.textBoxNgName.Name = "textBoxNgName";
            this.textBoxNgName.Size = new System.Drawing.Size(300, 19);
            this.textBoxNgName.TabIndex = 3;
            // 
            // buttonDeleteNgName
            // 
            this.buttonDeleteNgName.Location = new System.Drawing.Point(166, 256);
            this.buttonDeleteNgName.Name = "buttonDeleteNgName";
            this.buttonDeleteNgName.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteNgName.TabIndex = 2;
            this.buttonDeleteNgName.Text = "削除";
            this.buttonDeleteNgName.UseVisualStyleBackColor = true;
            this.buttonDeleteNgName.Click += new System.EventHandler(this.buttonDeleteNgName_Click);
            // 
            // buttonAddNgName
            // 
            this.buttonAddNgName.Location = new System.Drawing.Point(4, 256);
            this.buttonAddNgName.Name = "buttonAddNgName";
            this.buttonAddNgName.Size = new System.Drawing.Size(75, 23);
            this.buttonAddNgName.TabIndex = 1;
            this.buttonAddNgName.Text = "追加";
            this.buttonAddNgName.UseVisualStyleBackColor = true;
            this.buttonAddNgName.Click += new System.EventHandler(this.buttonAddNgName_Click);
            // 
            // listBoxNgName
            // 
            this.listBoxNgName.FormattingEnabled = true;
            this.listBoxNgName.ItemHeight = 12;
            this.listBoxNgName.Location = new System.Drawing.Point(3, 3);
            this.listBoxNgName.Name = "listBoxNgName";
            this.listBoxNgName.Size = new System.Drawing.Size(456, 220);
            this.listBoxNgName.TabIndex = 0;
            this.listBoxNgName.SelectedIndexChanged += new System.EventHandler(this.listBoxNgName_SelectedIndexChanged);
            // 
            // tabPageNgWord
            // 
            this.tabPageNgWord.Controls.Add(this.panel2);
            this.tabPageNgWord.Controls.Add(this.panel1);
            this.tabPageNgWord.Location = new System.Drawing.Point(4, 22);
            this.tabPageNgWord.Name = "tabPageNgWord";
            this.tabPageNgWord.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNgWord.Size = new System.Drawing.Size(478, 331);
            this.tabPageNgWord.TabIndex = 1;
            this.tabPageNgWord.Text = "NGワード管理";
            this.tabPageNgWord.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonNgWordOk);
            this.panel2.Location = new System.Drawing.Point(8, 294);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(462, 29);
            this.panel2.TabIndex = 2;
            // 
            // buttonNgWordOk
            // 
            this.buttonNgWordOk.Location = new System.Drawing.Point(384, 3);
            this.buttonNgWordOk.Name = "buttonNgWordOk";
            this.buttonNgWordOk.Size = new System.Drawing.Size(75, 23);
            this.buttonNgWordOk.TabIndex = 5;
            this.buttonNgWordOk.Text = "OK";
            this.buttonNgWordOk.UseVisualStyleBackColor = true;
            this.buttonNgWordOk.Click += new System.EventHandler(this.buttonNgWordOk_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonUpdateNgWord);
            this.panel1.Controls.Add(this.labelNgWordError);
            this.panel1.Controls.Add(this.checkBoxNgWordRegex);
            this.panel1.Controls.Add(this.textBoxNgWord);
            this.panel1.Controls.Add(this.buttonDeleteNgWord);
            this.panel1.Controls.Add(this.buttonAddNgWord);
            this.panel1.Controls.Add(this.listBoxNgWord);
            this.panel1.Location = new System.Drawing.Point(8, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 282);
            this.panel1.TabIndex = 1;
            // 
            // checkBoxNgWordRegex
            // 
            this.checkBoxNgWordRegex.AutoSize = true;
            this.checkBoxNgWordRegex.Location = new System.Drawing.Point(310, 233);
            this.checkBoxNgWordRegex.Name = "checkBoxNgWordRegex";
            this.checkBoxNgWordRegex.Size = new System.Drawing.Size(124, 16);
            this.checkBoxNgWordRegex.TabIndex = 4;
            this.checkBoxNgWordRegex.Text = "正規表現を使用する";
            this.checkBoxNgWordRegex.UseVisualStyleBackColor = true;
            // 
            // textBoxNgWord
            // 
            this.textBoxNgWord.Location = new System.Drawing.Point(4, 231);
            this.textBoxNgWord.Name = "textBoxNgWord";
            this.textBoxNgWord.Size = new System.Drawing.Size(300, 19);
            this.textBoxNgWord.TabIndex = 3;
            // 
            // buttonDeleteNgWord
            // 
            this.buttonDeleteNgWord.Location = new System.Drawing.Point(166, 256);
            this.buttonDeleteNgWord.Name = "buttonDeleteNgWord";
            this.buttonDeleteNgWord.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteNgWord.TabIndex = 2;
            this.buttonDeleteNgWord.Text = "削除";
            this.buttonDeleteNgWord.UseVisualStyleBackColor = true;
            this.buttonDeleteNgWord.Click += new System.EventHandler(this.buttonDeleteNgWord_Click);
            // 
            // buttonAddNgWord
            // 
            this.buttonAddNgWord.Location = new System.Drawing.Point(4, 256);
            this.buttonAddNgWord.Name = "buttonAddNgWord";
            this.buttonAddNgWord.Size = new System.Drawing.Size(75, 23);
            this.buttonAddNgWord.TabIndex = 1;
            this.buttonAddNgWord.Text = "追加";
            this.buttonAddNgWord.UseVisualStyleBackColor = true;
            this.buttonAddNgWord.Click += new System.EventHandler(this.buttonAddNgWord_Click);
            // 
            // listBoxNgWord
            // 
            this.listBoxNgWord.FormattingEnabled = true;
            this.listBoxNgWord.ItemHeight = 12;
            this.listBoxNgWord.Location = new System.Drawing.Point(3, 3);
            this.listBoxNgWord.Name = "listBoxNgWord";
            this.listBoxNgWord.Size = new System.Drawing.Size(456, 220);
            this.listBoxNgWord.TabIndex = 0;
            this.listBoxNgWord.SelectedIndexChanged += new System.EventHandler(this.listBoxNgWord_SelectedIndexChanged);
            // 
            // labelNgWordError
            // 
            this.labelNgWordError.AutoSize = true;
            this.labelNgWordError.ForeColor = System.Drawing.Color.Red;
            this.labelNgWordError.Location = new System.Drawing.Point(247, 261);
            this.labelNgWordError.Name = "labelNgWordError";
            this.labelNgWordError.Size = new System.Drawing.Size(29, 12);
            this.labelNgWordError.TabIndex = 5;
            this.labelNgWordError.Text = "error";
            // 
            // labelNgNameError
            // 
            this.labelNgNameError.AutoSize = true;
            this.labelNgNameError.ForeColor = System.Drawing.Color.Red;
            this.labelNgNameError.Location = new System.Drawing.Point(247, 261);
            this.labelNgNameError.Name = "labelNgNameError";
            this.labelNgNameError.Size = new System.Drawing.Size(29, 12);
            this.labelNgNameError.TabIndex = 5;
            this.labelNgNameError.Text = "error";
            // 
            // buttonUpdateNgName
            // 
            this.buttonUpdateNgName.Enabled = false;
            this.buttonUpdateNgName.Location = new System.Drawing.Point(85, 256);
            this.buttonUpdateNgName.Name = "buttonUpdateNgName";
            this.buttonUpdateNgName.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdateNgName.TabIndex = 6;
            this.buttonUpdateNgName.Text = "更新";
            this.buttonUpdateNgName.UseVisualStyleBackColor = true;
            this.buttonUpdateNgName.Click += new System.EventHandler(this.buttonUpdateNgName_Click);
            // 
            // buttonUpdateNgWord
            // 
            this.buttonUpdateNgWord.Enabled = false;
            this.buttonUpdateNgWord.Location = new System.Drawing.Point(85, 256);
            this.buttonUpdateNgWord.Name = "buttonUpdateNgWord";
            this.buttonUpdateNgWord.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdateNgWord.TabIndex = 6;
            this.buttonUpdateNgWord.Text = "更新";
            this.buttonUpdateNgWord.UseVisualStyleBackColor = true;
            this.buttonUpdateNgWord.Click += new System.EventHandler(this.buttonUpdateNgWord_Click);
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 357);
            this.Controls.Add(this.tabControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionForm";
            this.ShowIcon = false;
            this.Text = "オプション";
            this.TopMost = true;
            this.tabControl.ResumeLayout(false);
            this.tabPageNgName.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPageNgWord.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageNgName;
        private System.Windows.Forms.TabPage tabPageNgWord;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox listBoxNgWord;
        private System.Windows.Forms.Button buttonNgWordOk;
        private System.Windows.Forms.CheckBox checkBoxNgWordRegex;
        private System.Windows.Forms.TextBox textBoxNgWord;
        private System.Windows.Forms.Button buttonDeleteNgWord;
        private System.Windows.Forms.Button buttonAddNgWord;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button buttonNgNameOk;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox checkBoxNgNameRegex;
        private System.Windows.Forms.TextBox textBoxNgName;
        private System.Windows.Forms.Button buttonDeleteNgName;
        private System.Windows.Forms.Button buttonAddNgName;
        private System.Windows.Forms.ListBox listBoxNgName;
        private System.Windows.Forms.Label labelNgWordError;
        private System.Windows.Forms.Label labelNgNameError;
        private System.Windows.Forms.Button buttonUpdateNgName;
        private System.Windows.Forms.Button buttonUpdateNgWord;
    }
}