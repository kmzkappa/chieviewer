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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.listViewArticles = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCoin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCategoryA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCategoryB = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCategoryC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUpdatedTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.categoryTree = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.brsArticle = new System.Windows.Forms.WebBrowser();
            this.contextMenuStripBrowser = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextBrowserCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.statusStripMainText = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ヘルプHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chieViewerについてToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnGetNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBoxQuestionId = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonMoveQuestionId = new System.Windows.Forms.ToolStripButton();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.contextMenuStripBrowser.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.listViewArticles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewArticles.Location = new System.Drawing.Point(0, 0);
            this.listViewArticles.MultiSelect = false;
            this.listViewArticles.Name = "listViewArticles";
            this.listViewArticles.Size = new System.Drawing.Size(1054, 356);
            this.listViewArticles.TabIndex = 1;
            this.listViewArticles.UseCompatibleStateImageBehavior = false;
            this.listViewArticles.View = System.Windows.Forms.View.Details;
            this.listViewArticles.Click += new System.EventHandler(this.listViewArticles_Click);
            // 
            // colId
            // 
            this.colId.Text = "ID";
            // 
            // colTitle
            // 
            this.colTitle.Text = "記事";
            this.colTitle.Width = 300;
            // 
            // colCoin
            // 
            this.colCoin.Text = "知恵コイン";
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
            // colUpdatedTime
            // 
            this.colUpdatedTime.Text = "更新日時";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer1.Panel1.Controls.Add(this.categoryTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1270, 720);
            this.splitContainer1.SplitterDistance = 208;
            this.splitContainer1.TabIndex = 2;
            // 
            // categoryTree
            // 
            this.categoryTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryTree.Location = new System.Drawing.Point(0, 0);
            this.categoryTree.Name = "categoryTree";
            this.categoryTree.Size = new System.Drawing.Size(204, 716);
            this.categoryTree.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listViewArticles);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.brsArticle);
            this.splitContainer2.Size = new System.Drawing.Size(1058, 720);
            this.splitContainer2.SplitterDistance = 360;
            this.splitContainer2.TabIndex = 3;
            // 
            // brsArticle
            // 
            this.brsArticle.ContextMenuStrip = this.contextMenuStripBrowser;
            this.brsArticle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brsArticle.IsWebBrowserContextMenuEnabled = false;
            this.brsArticle.Location = new System.Drawing.Point(0, 0);
            this.brsArticle.MinimumSize = new System.Drawing.Size(20, 20);
            this.brsArticle.Name = "brsArticle";
            this.brsArticle.Size = new System.Drawing.Size(1054, 352);
            this.brsArticle.TabIndex = 2;
            // 
            // contextMenuStripBrowser
            // 
            this.contextMenuStripBrowser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextBrowserCopy});
            this.contextMenuStripBrowser.Name = "contextMenuStripBrowser";
            this.contextMenuStripBrowser.Size = new System.Drawing.Size(114, 26);
            this.contextMenuStripBrowser.Opened += new System.EventHandler(this.contextMenuStripBrowser_Opened);
            // 
            // contextBrowserCopy
            // 
            this.contextBrowserCopy.Name = "contextBrowserCopy";
            this.contextBrowserCopy.Size = new System.Drawing.Size(113, 22);
            this.contextBrowserCopy.Text = "コピー(&C)";
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripMainText,
            this.toolStripProgressBar});
            this.statusStripMain.Location = new System.Drawing.Point(0, 769);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(1270, 22);
            this.statusStripMain.SizingGrip = false;
            this.statusStripMain.TabIndex = 3;
            this.statusStripMain.Text = "a";
            // 
            // statusStripMainText
            // 
            this.statusStripMainText.Name = "statusStripMainText";
            this.statusStripMainText.Size = new System.Drawing.Size(1153, 17);
            this.statusStripMainText.Spring = true;
            this.statusStripMainText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ヘルプHToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1270, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ヘルプHToolStripMenuItem
            // 
            this.ヘルプHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chieViewerについてToolStripMenuItem});
            this.ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
            this.ヘルプHToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)";
            // 
            // chieViewerについてToolStripMenuItem
            // 
            this.chieViewerについてToolStripMenuItem.Name = "chieViewerについてToolStripMenuItem";
            this.chieViewerについてToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.chieViewerについてToolStripMenuItem.Text = "ChieViewerについて";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGetNew,
            this.toolStripTextBoxQuestionId,
            this.toolStripButtonMoveQuestionId});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1270, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnGetNew
            // 
            this.btnGetNew.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGetNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGetNew.Image = ((System.Drawing.Image)(resources.GetObject("btnGetNew.Image")));
            this.btnGetNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGetNew.Name = "btnGetNew";
            this.btnGetNew.Size = new System.Drawing.Size(59, 22);
            this.btnGetNew.Text = "新着取得";
            this.btnGetNew.Click += new System.EventHandler(this.btnGetNew_Click);
            // 
            // toolStripTextBoxQuestionId
            // 
            this.toolStripTextBoxQuestionId.Name = "toolStripTextBoxQuestionId";
            this.toolStripTextBoxQuestionId.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripButtonMoveQuestionId
            // 
            this.toolStripButtonMoveQuestionId.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonMoveQuestionId.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMoveQuestionId.Image")));
            this.toolStripButtonMoveQuestionId.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMoveQuestionId.Name = "toolStripButtonMoveQuestionId";
            this.toolStripButtonMoveQuestionId.Size = new System.Drawing.Size(35, 22);
            this.toolStripButtonMoveQuestionId.Text = "移動";
            this.toolStripButtonMoveQuestionId.Click += new System.EventHandler(this.toolStripButtonMoveQuestionId_Click);
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 791);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Chie Viewer";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.contextMenuStripBrowser.ResumeLayout(false);
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.TreeView categoryTree;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ヘルプHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chieViewerについてToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnGetNew;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripBrowser;
        private System.Windows.Forms.ToolStripMenuItem contextBrowserCopy;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxQuestionId;
        private System.Windows.Forms.ToolStripButton toolStripButtonMoveQuestionId;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
    }
}

