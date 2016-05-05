namespace chieviewer
{
    partial class MainWindow
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGetNewQuestionList = new System.Windows.Forms.Button();
            this.listViewArticles = new System.Windows.Forms.ListView();
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCoin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUpdatedTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCategoryA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCategoryB = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCategoryC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.brsArticle = new System.Windows.Forms.WebBrowser();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.statusStripMainText = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetNewQuestionList
            // 
            this.btnGetNewQuestionList.Location = new System.Drawing.Point(13, 13);
            this.btnGetNewQuestionList.Name = "btnGetNewQuestionList";
            this.btnGetNewQuestionList.Size = new System.Drawing.Size(131, 23);
            this.btnGetNewQuestionList.TabIndex = 0;
            this.btnGetNewQuestionList.Text = "GetNewQuestionList";
            this.btnGetNewQuestionList.UseVisualStyleBackColor = true;
            this.btnGetNewQuestionList.Click += new System.EventHandler(this.btnGetNewQuestionList_Click);
            // 
            // listViewArticles
            // 
            this.listViewArticles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colTitle,
            this.colCoin,
            this.colCategoryA,
            this.colCategoryB,
            this.colCategoryC,
            this.colUpdatedTime});
            this.listViewArticles.Location = new System.Drawing.Point(3, 3);
            this.listViewArticles.MultiSelect = false;
            this.listViewArticles.Name = "listViewArticles";
            this.listViewArticles.Size = new System.Drawing.Size(951, 330);
            this.listViewArticles.TabIndex = 1;
            this.listViewArticles.UseCompatibleStateImageBehavior = false;
            this.listViewArticles.View = System.Windows.Forms.View.Details;
            this.listViewArticles.Click += new System.EventHandler(this.listViewArticles_Click);
            // 
            // colTitle
            // 
            this.colTitle.Text = "記事";
            this.colTitle.Width = 300;
            // 
            // colId
            // 
            this.colId.Text = "ID";
            // 
            // colCoin
            // 
            this.colCoin.Text = "知恵コイン";
            // 
            // colUpdatedTime
            // 
            this.colUpdatedTime.Text = "更新日時";
            // 
            // colCategoryA
            // 
            this.colCategoryA.Text = "大分類";
            // 
            // colCategoryB
            // 
            this.colCategoryB.Text = "中分類";
            // 
            // colCategoryC
            // 
            this.colCategoryC.Text = "小分類";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(13, 42);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.brsArticle);
            this.splitContainer1.Panel2.Controls.Add(this.listViewArticles);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(1222, 662);
            this.splitContainer1.SplitterDistance = 261;
            this.splitContainer1.TabIndex = 2;
            // 
            // brsArticle
            // 
            this.brsArticle.IsWebBrowserContextMenuEnabled = false;
            this.brsArticle.Location = new System.Drawing.Point(3, 339);
            this.brsArticle.MinimumSize = new System.Drawing.Size(20, 20);
            this.brsArticle.Name = "brsArticle";
            this.brsArticle.Size = new System.Drawing.Size(951, 320);
            this.brsArticle.TabIndex = 2;
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripMainText});
            this.statusStripMain.Location = new System.Drawing.Point(0, 769);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(1270, 22);
            this.statusStripMain.SizingGrip = false;
            this.statusStripMain.TabIndex = 3;
            this.statusStripMain.Text = "a";
            this.statusStripMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStripMain_ItemClicked);
            // 
            // statusStripMainText
            // 
            this.statusStripMainText.Name = "statusStripMainText";
            this.statusStripMainText.Size = new System.Drawing.Size(0, 17);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 791);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnGetNewQuestionList);
            this.Name = "MainWindow";
            this.Text = "Chie Viewer";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetNewQuestionList;
        private System.Windows.Forms.ListView listViewArticles;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colCoin;
        private System.Windows.Forms.ColumnHeader colCategoryA;
        private System.Windows.Forms.ColumnHeader colCategoryB;
        private System.Windows.Forms.ColumnHeader colCategoryC;
        private System.Windows.Forms.ColumnHeader colUpdatedTime;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser brsArticle;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel statusStripMainText;
    }
}

