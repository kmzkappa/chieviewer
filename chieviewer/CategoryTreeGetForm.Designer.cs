namespace chieviewer
{
    partial class CategoryTreeGetForm
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelProgress1 = new System.Windows.Forms.Label();
            this.labelProgress2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 63);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(373, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // labelProgress1
            // 
            this.labelProgress1.AutoSize = true;
            this.labelProgress1.Location = new System.Drawing.Point(13, 13);
            this.labelProgress1.Name = "labelProgress1";
            this.labelProgress1.Size = new System.Drawing.Size(0, 12);
            this.labelProgress1.TabIndex = 1;
            // 
            // labelProgress2
            // 
            this.labelProgress2.AutoSize = true;
            this.labelProgress2.Location = new System.Drawing.Point(13, 35);
            this.labelProgress2.Name = "labelProgress2";
            this.labelProgress2.Size = new System.Drawing.Size(9, 12);
            this.labelProgress2.TabIndex = 2;
            this.labelProgress2.Text = " ";
            // 
            // CategoryTreeGetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 107);
            this.ControlBox = false;
            this.Controls.Add(this.labelProgress2);
            this.Controls.Add(this.labelProgress1);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CategoryTreeGetForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.CategoryTreeGetForm_Load);
            this.Shown += new System.EventHandler(this.CategoryTreeGetForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelProgress1;
        private System.Windows.Forms.Label labelProgress2;
    }
}