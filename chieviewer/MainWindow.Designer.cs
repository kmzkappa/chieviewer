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
            this.colAnsCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCategoryA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCategoryB = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCategoryC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUpdatedDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.categoryTree = new System.Windows.Forms.TreeView();
            this.imageListCategory = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.brsArticle = new System.Windows.Forms.WebBrowser();
            this.contextMenuStripBrowser = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextBrowserCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.statusStripMainText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ヘルプHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemVersionInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnGetNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBoxQuestionId = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonMoveQuestionId = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripComboBoxLevel1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBoxLevel2 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBoxLevel3 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBoxSearchQuery = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonSearch = new System.Windows.Forms.ToolStripButton();
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
            this.colAnsCount,
            this.colCategoryA,
            this.colCategoryB,
            this.colCategoryC,
            this.colUpdatedDate,
            this.colStatus});
            this.listViewArticles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewArticles.Location = new System.Drawing.Point(0, 0);
            this.listViewArticles.MultiSelect = false;
            this.listViewArticles.Name = "listViewArticles";
            this.listViewArticles.Size = new System.Drawing.Size(1034, 356);
            this.listViewArticles.TabIndex = 1;
            this.listViewArticles.UseCompatibleStateImageBehavior = false;
            this.listViewArticles.View = System.Windows.Forms.View.Details;
            this.listViewArticles.Click += new System.EventHandler(this.listViewArticles_Click);
            // 
            // colId
            // 
            this.colId.Text = "ID";
            this.colId.Width = 5;
            // 
            // colTitle
            // 
            this.colTitle.Text = "質問";
            this.colTitle.Width = 440;
            // 
            // colCoin
            // 
            this.colCoin.Text = "知恵コイン";
            this.colCoin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colCoin.Width = 50;
            // 
            // colAnsCount
            // 
            this.colAnsCount.Text = "回答数";
            this.colAnsCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colAnsCount.Width = 43;
            // 
            // colCategoryA
            // 
            this.colCategoryA.Text = "大分類";
            this.colCategoryA.Width = 130;
            // 
            // colCategoryB
            // 
            this.colCategoryB.Text = "中分類";
            this.colCategoryB.Width = 100;
            // 
            // colCategoryC
            // 
            this.colCategoryC.Text = "小分類";
            this.colCategoryC.Width = 90;
            // 
            // colUpdatedDate
            // 
            this.colUpdatedDate.Text = "更新日時";
            this.colUpdatedDate.Width = 102;
            // 
            // colStatus
            // 
            this.colStatus.Text = "状態";
            this.colStatus.Width = 48;
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
            this.splitContainer1.Size = new System.Drawing.Size(1224, 720);
            this.splitContainer1.SplitterDistance = 182;
            this.splitContainer1.TabIndex = 2;
            // 
            // categoryTree
            // 
            this.categoryTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryTree.ImageKey = "folder-c-small.png";
            this.categoryTree.ImageList = this.imageListCategory;
            this.categoryTree.Location = new System.Drawing.Point(0, 0);
            this.categoryTree.Name = "categoryTree";
            this.categoryTree.SelectedImageIndex = 0;
            this.categoryTree.ShowPlusMinus = false;
            this.categoryTree.Size = new System.Drawing.Size(178, 716);
            this.categoryTree.TabIndex = 0;
            this.categoryTree.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.categoryTree_BeforeCollapse);
            this.categoryTree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.categoryTree_AfterCollapse);
            this.categoryTree.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.categoryTree_AfterExpand);
            this.categoryTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.categoryTree_AfterSelect);
            this.categoryTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.categoryTree_NodeMouseClick);
            // 
            // imageListCategory
            // 
            this.imageListCategory.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCategory.ImageStream")));
            this.imageListCategory.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListCategory.Images.SetKeyName(0, "folder-c-small.png");
            this.imageListCategory.Images.SetKeyName(1, "folder-o.png");
            this.imageListCategory.Images.SetKeyName(2, "comment.png");
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
            this.splitContainer2.Size = new System.Drawing.Size(1038, 720);
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
            this.brsArticle.Size = new System.Drawing.Size(1034, 352);
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
            this.statusStripMain.Size = new System.Drawing.Size(1224, 22);
            this.statusStripMain.SizingGrip = false;
            this.statusStripMain.TabIndex = 3;
            this.statusStripMain.Text = "a";
            // 
            // statusStripMainText
            // 
            this.statusStripMainText.Name = "statusStripMainText";
            this.statusStripMainText.Size = new System.Drawing.Size(1107, 17);
            this.statusStripMainText.Spring = true;
            this.statusStripMainText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ヘルプHToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1224, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ヘルプHToolStripMenuItem
            // 
            this.ヘルプHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemVersionInfo});
            this.ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
            this.ヘルプHToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)";
            // 
            // toolStripMenuItemVersionInfo
            // 
            this.toolStripMenuItemVersionInfo.Name = "toolStripMenuItemVersionInfo";
            this.toolStripMenuItemVersionInfo.Size = new System.Drawing.Size(175, 22);
            this.toolStripMenuItemVersionInfo.Text = "Chie Viewer について";
            this.toolStripMenuItemVersionInfo.Click += new System.EventHandler(this.toolStripMenuItemVersionInfo_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGetNew,
            this.toolStripSeparator2,
            this.toolStripTextBoxQuestionId,
            this.toolStripButtonMoveQuestionId,
            this.toolStripSeparator1,
            this.toolStripComboBoxLevel1,
            this.toolStripComboBoxLevel2,
            this.toolStripComboBoxLevel3,
            this.toolStripTextBoxSearchQuery,
            this.toolStripButtonSearch});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1224, 25);
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
            this.btnGetNew.ToolTipText = "選択したカテゴリの新着質問を取得します。";
            this.btnGetNew.Click += new System.EventHandler(this.btnGetNew_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripTextBoxQuestionId
            // 
            this.toolStripTextBoxQuestionId.Name = "toolStripTextBoxQuestionId";
            this.toolStripTextBoxQuestionId.Size = new System.Drawing.Size(385, 25);
            this.toolStripTextBoxQuestionId.Enter += new System.EventHandler(this.toolStripTextBoxQuestionId_Enter);
            this.toolStripTextBoxQuestionId.Leave += new System.EventHandler(this.toolStripTextBoxQuestionId_Leave);
            // 
            // toolStripButtonMoveQuestionId
            // 
            this.toolStripButtonMoveQuestionId.AutoToolTip = false;
            this.toolStripButtonMoveQuestionId.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonMoveQuestionId.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMoveQuestionId.Image")));
            this.toolStripButtonMoveQuestionId.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMoveQuestionId.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.toolStripButtonMoveQuestionId.Name = "toolStripButtonMoveQuestionId";
            this.toolStripButtonMoveQuestionId.Size = new System.Drawing.Size(35, 22);
            this.toolStripButtonMoveQuestionId.Text = "表示";
            this.toolStripButtonMoveQuestionId.Click += new System.EventHandler(this.toolStripButtonMoveQuestionId_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(2, 0, 3, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripComboBoxLevel1
            // 
            this.toolStripComboBoxLevel1.BackColor = System.Drawing.SystemColors.Window;
            this.toolStripComboBoxLevel1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxLevel1.DropDownWidth = 170;
            this.toolStripComboBoxLevel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toolStripComboBoxLevel1.Margin = new System.Windows.Forms.Padding(3, 0, 1, 0);
            this.toolStripComboBoxLevel1.MaxDropDownItems = 100;
            this.toolStripComboBoxLevel1.Name = "toolStripComboBoxLevel1";
            this.toolStripComboBoxLevel1.Size = new System.Drawing.Size(150, 25);
            this.toolStripComboBoxLevel1.ToolTipText = "大分類";
            this.toolStripComboBoxLevel1.DropDownClosed += new System.EventHandler(this.toolStripComboBoxLevel1_DropDownClosed);
            // 
            // toolStripComboBoxLevel2
            // 
            this.toolStripComboBoxLevel2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxLevel2.DropDownWidth = 150;
            this.toolStripComboBoxLevel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toolStripComboBoxLevel2.Margin = new System.Windows.Forms.Padding(3, 0, 1, 0);
            this.toolStripComboBoxLevel2.Name = "toolStripComboBoxLevel2";
            this.toolStripComboBoxLevel2.Size = new System.Drawing.Size(140, 25);
            this.toolStripComboBoxLevel2.ToolTipText = "中分類";
            this.toolStripComboBoxLevel2.DropDownClosed += new System.EventHandler(this.toolStripComboBoxLevel2_DropDownClosed);
            // 
            // toolStripComboBoxLevel3
            // 
            this.toolStripComboBoxLevel3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxLevel3.DropDownWidth = 140;
            this.toolStripComboBoxLevel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toolStripComboBoxLevel3.Margin = new System.Windows.Forms.Padding(3, 0, 1, 0);
            this.toolStripComboBoxLevel3.Name = "toolStripComboBoxLevel3";
            this.toolStripComboBoxLevel3.Size = new System.Drawing.Size(130, 25);
            this.toolStripComboBoxLevel3.ToolTipText = "小分類";
            this.toolStripComboBoxLevel3.DropDownClosed += new System.EventHandler(this.toolStripComboBoxLevel3_DropDownClosed);
            // 
            // toolStripTextBoxSearchQuery
            // 
            this.toolStripTextBoxSearchQuery.ForeColor = System.Drawing.SystemColors.WindowText;
            this.toolStripTextBoxSearchQuery.Margin = new System.Windows.Forms.Padding(3, 0, 1, 0);
            this.toolStripTextBoxSearchQuery.Name = "toolStripTextBoxSearchQuery";
            this.toolStripTextBoxSearchQuery.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBoxSearchQuery.ToolTipText = "検索キーワード(必須)";
            this.toolStripTextBoxSearchQuery.Enter += new System.EventHandler(this.toolStripTextBoxSearchQuery_Enter);
            this.toolStripTextBoxSearchQuery.Leave += new System.EventHandler(this.toolStripTextBoxSearchQuery_Leave);
            this.toolStripTextBoxSearchQuery.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxSearchQuery_KeyPress);
            // 
            // toolStripButtonSearch
            // 
            this.toolStripButtonSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSearch.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSearch.Image")));
            this.toolStripButtonSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSearch.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.toolStripButtonSearch.Name = "toolStripButtonSearch";
            this.toolStripButtonSearch.Size = new System.Drawing.Size(35, 22);
            this.toolStripButtonSearch.Text = "検索";
            this.toolStripButtonSearch.ToolTipText = "指定されたカテゴリ(大分類～小分類)およびキーワードに合致する質問を検索します。";
            this.toolStripButtonSearch.Click += new System.EventHandler(this.toolStripButtonSearch_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 791);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Chie Viewer";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
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
        private System.Windows.Forms.ColumnHeader colUpdatedDate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser brsArticle;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel statusStripMainText;
        private System.Windows.Forms.TreeView categoryTree;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ヘルプHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemVersionInfo;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnGetNew;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripBrowser;
        private System.Windows.Forms.ToolStripMenuItem contextBrowserCopy;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxQuestionId;
        private System.Windows.Forms.ToolStripButton toolStripButtonMoveQuestionId;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colAnsCount;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxLevel1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxLevel2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxLevel3;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSearchQuery;
        private System.Windows.Forms.ToolStripButton toolStripButtonSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ImageList imageListCategory;
    }
}

