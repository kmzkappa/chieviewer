using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using chieviewer.Api;
using System.Xml.Serialization;
using System.IO;
using mshtml;
using System.Text.RegularExpressions;

namespace chieviewer
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MainWindow_Load(object sender, EventArgs e)
        {
            // 設定
            listViewArticles.FullRowSelect = true;

            // DB初期化
            DataBase db = new DataBase();
            if (!db.CheckDb())
            {
                statusStripMainText.Text =
                    "データベースが作成されていません。新規作成します。";
                db.InitDb();
                statusStripMainText.Text =
                    "データベースを新規作成しました。";
            }
            if (db.IsEmptyCategoryTree())
            {
                statusStripMainText.Text = "カテゴリツリーを取得しています";
                db.InitCategoryTree(statusStripMainText, toolStripProgressBar);
                toolStripProgressBar.Value = 0;
            }
            
            // カテゴリツリー表示
            UpdateCategoryTree();

            // 検索用のカテゴリ項目初期化
            UpdateCategoryComboBox(1, null);

            

            // その他初期化
            using (StreamReader stream = new StreamReader("../../../template/default/article.html", Encoding.GetEncoding("UTF-8")))
            {
                brsArticle.DocumentText = await stream.ReadToEndAsync();
            }
            //await GetNewQuestionList(sender);
            toolStripTextBoxSearchQuery.Text = "キーワード";
            toolStripTextBoxSearchQuery.ForeColor = Color.DarkGray;



        }

        /***********************************************************/
        /* フォーム・アクション */
        /***********************************************************/

        // 新着取得
        private async void btnGetNew_Click(object sender, EventArgs e)
        {
            await GetNewQuestionList(sender);
        }

        // 記事リスト行が選択されたとき
        private async void listViewArticles_Click(object sender, EventArgs e)
        {
            string questionId = ((ListView)sender).SelectedItems[0].Text;

            await GetArticleDetail(questionId);

            await Task.Delay(500);
            toolStripProgressBar.Value = 0;
        }

        // ブラウザで右クリックしたとき
        private void contextMenuStripBrowser_Opened(object sender, EventArgs e)
        {
            // すべて選択
            // brsArticle.Document.ExecCommand("SelectAll", false, null);
            // コピー
            // brsArticle.Document.ExecCommand("Copy", false, null);

            // 文字列が選択されていなければ「コピー」を無効化する

            IHTMLDocument2 htmlDocument = brsArticle.Document.DomDocument as IHTMLDocument2;
            IHTMLSelectionObject currentSelection = htmlDocument.selection;
            if (currentSelection != null)
            {
                IHTMLTxtRange range = currentSelection.createRange() as IHTMLTxtRange;
                if (range.text == null)
                {
                    contextBrowserCopy.Enabled = false;
                }
                else
                {
                    contextBrowserCopy.Enabled = true;
                }
            }
        }

        // 移動ボタン
        private async void toolStripButtonMoveQuestionId_Click(object sender, EventArgs e)
        {
            string questionId = toolStripTextBoxQuestionId.Text;
            if (string.IsNullOrEmpty(questionId)) return;

            await GetArticleDetail(questionId);

        }

        // カテゴリツリー選択時
        private async void categoryTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // 記事一覧を更新する
            toolStripProgressBar.Value = 21;
            toolStripProgressBar.Value = 20;

            string categoryId = categoryTree.SelectedNode.Name;

            try
            {
                await GetNewQuestionList(sender, categoryId);
            } catch(NullReferenceException ex)
            {
                statusStripMainText.Text = "記事一覧の取得に失敗しました。";
                Console.WriteLine(ex.InnerException);
            }

            /* 以下、選択結果をコンボボックスに反映する */
            DataBase db = new DataBase();
            CategoryTreeModel selectedCategory =
                db.GetCategoryTree().Where(item => item.CategoryId == categoryId).FirstOrDefault();

            // 全てのカテゴリを選択した場合
            if(selectedCategory == null)
            {
                toolStripComboBoxLevel2.Items.Clear();
                toolStripComboBoxLevel2.Items.Add(new CategoryTreeModel("中分類"));
                toolStripComboBoxLevel3.Items.Clear();
                toolStripComboBoxLevel3.Items.Add(new CategoryTreeModel("小分類"));
                toolStripComboBoxLevel1.SelectedIndex = 0;
                toolStripComboBoxLevel2.SelectedIndex = 0;
                toolStripComboBoxLevel3.SelectedIndex = 0;
            }
            // 有効なカテゴリを選択した場合
            else
            {
                string selectedCategoryPath = selectedCategory.CategoryPath;
                string[] categories = selectedCategoryPath.Split('|');
                // カテゴリツリーから 大・中・小 分類を指定した場合
                if(categories.Length >= 1)
                {
                    // 対応する大分類コンボを選択する
                    var comboItems = toolStripComboBoxLevel1.Items;
                    int cnt = 0;
                    foreach(var item in comboItems)
                    {
                        if(((CategoryTreeModel)item).CategoryId == categories[0])
                        {
                            break;
                        }
                        cnt++;
                    }
                    toolStripComboBoxLevel1.SelectedIndex = cnt;
                    // 中分類コンボを更新して先頭項目を選択
                    UpdateCategoryComboBox(2, categories[0]);
                }
                // カテゴリツリーから 中・小 分類を指定した場合
                if(categories.Length >= 2)
                {
                    // 中分類コンボを選択
                    var comboItems2 = toolStripComboBoxLevel2.Items;
                    int cnt2 = 0;
                    foreach(var item in comboItems2)
                    {
                        if (((CategoryTreeModel)item).CategoryId == categories[1])
                        {
                            break;
                        }
                        cnt2++;
                    }
                    toolStripComboBoxLevel2.SelectedIndex = cnt2;

                    // 小分類コンボを更新
                    UpdateCategoryComboBox(3, categories[1]);
                }
                // カテゴリツリーから小分類を指定した場合
                if(categories.Length >= 3)
                {
                    // 小分類コンボを選択
                    var comboItems3 = toolStripComboBoxLevel3.Items;
                    int cnt3 = 0;
                    foreach (var item in comboItems3)
                    {
                        if (((CategoryTreeModel)item).CategoryId == categories[2])
                        {
                            break;
                        }
                        cnt3++;
                    }
                    toolStripComboBoxLevel3.SelectedIndex = cnt3;
                }
            }

            await Task.Delay(500);
            toolStripProgressBar.Value = 0;
        }

        // 検索キーワードボックス(enter)
        private void toolStripTextBoxSearchQuery_Enter(object sender, EventArgs e)
        {
            if(toolStripTextBoxSearchQuery.ForeColor == Color.DarkGray)
            {
                toolStripTextBoxSearchQuery.Text = "";
                toolStripTextBoxSearchQuery.ForeColor = Color.Black;
            }
        }

        // 検索キーワードボックス(leave)
        private void toolStripTextBoxSearchQuery_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(toolStripTextBoxSearchQuery.Text))
            {
                toolStripTextBoxSearchQuery.ForeColor = Color.DarkGray;                
                toolStripTextBoxSearchQuery.Text = "キーワード";
            }
        }

        // 検索ボタン
        private async void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = toolStripTextBoxSearchQuery.Text;

            await GetSearchResultList(searchQuery);

        }


        // カテゴリ選択コンボ（大分類）選択時
        private async void toolStripComboBoxLevel1_DropDownClosed(object sender, EventArgs e)
        {
            string categoryId = ((CategoryTreeModel)toolStripComboBoxLevel1.SelectedItem).CategoryId;
            UpdateCategoryComboBox(2, categoryId);
            await GetNewQuestionList(sender, categoryId);
        }

        // カテゴリ選択コンボ（中分類）選択時
        private async void toolStripComboBoxLevel2_DropDownClosed(object sender, EventArgs e)
        {
            string categoryId = ((CategoryTreeModel)toolStripComboBoxLevel2.SelectedItem).CategoryId;
            // 「中分類」が選択された場合
            if (string.IsNullOrEmpty(categoryId))
            {
                categoryId = ((CategoryTreeModel)toolStripComboBoxLevel1.SelectedItem).CategoryId;
            }
            UpdateCategoryComboBox(3, categoryId);
            await GetNewQuestionList(sender, categoryId);
        }

        // カテゴリ選択コンボ（小分類）選択時
        private async void toolStripComboBoxLevel3_DropDownClosed(object sender, EventArgs e)
        {
            string categoryId = ((CategoryTreeModel)toolStripComboBoxLevel3.SelectedItem).CategoryId;
            // 「小分類」が選択された場合
            if (string.IsNullOrEmpty(categoryId))
            {
                categoryId = ((CategoryTreeModel)toolStripComboBoxLevel2.SelectedItem).CategoryId;
                if (string.IsNullOrEmpty(categoryId))
                {
                    categoryId = ((CategoryTreeModel)toolStripComboBoxLevel1.SelectedItem).CategoryId;
                }
            }
            await GetNewQuestionList(sender, categoryId);
        }


        /***********************************************************/
        /* 画面更新処理 */
        /***********************************************************/

        // 一覧部分の更新（新着記事用）
        private void UpdateListViewArticles(Api.getNewQuestionList.ResultSet resultSet)
        {
            listViewArticles.Items.Clear();
            foreach(var result in resultSet.Result)
            {
                ListViewItem item = new ListViewItem(result.QuestionId);
                item.SubItems.Add(result.Content);
                // 知恵コイン
                item.SubItems.Add(result.Coin);
                // 回答数
                item.SubItems.Add(result.AnsCount);

                string[] categories = result.CategoryPath.Split('|');
                // 大項目
                if (categories.Length >= 1) {
                    item.SubItems.Add(categories[0]);
                } else {
                    item.SubItems.Add(" ");
                }
                // 中項目
                if(categories.Length >= 2) {
                    item.SubItems.Add(categories[1]);
                } else {
                    item.SubItems.Add(" ");
                }
                // 小項目
                if(categories.Length >= 3) {
                    item.SubItems.Add(categories[2]);
                } else {
                    item.SubItems.Add(" ");
                }

                // 更新日時
                // "2016-05-08T02:06:25+09:00"
                string ymd = Regex.Match(result.UpdatedDate, "^\\d{4}-\\d{2}-\\d{2}").Value;
                ymd = ymd.Replace('-', '/');
                string time = Regex.Match(result.UpdatedDate, "(?<=T)\\d{2}:\\d{2}").Value;
                item.SubItems.Add(ymd + " " + time);

                // 状態
                item.SubItems.Add("受付中");


                listViewArticles.Items.Add(item);
            }
        }

        // 一覧部分の更新（検索時用）
        private void UpdateListViewArticles(Api.questionSearchResponse.ResultSet resultSet)
        {
            listViewArticles.Items.Clear();
            foreach (var result in resultSet.Result)
            {
                ListViewItem item = new ListViewItem(result.Id);
                item.SubItems.Add(result.Content);

                // 知恵コイン
                // TODO: APIで取得できない？
                item.SubItems.Add("-");

                // 回答数
                item.SubItems.Add(result.AnsCount);

                string[] categories = result.CategoryPath.Split('|');
                // 大項目
                if (categories.Length >= 1)
                {
                    item.SubItems.Add(categories[0]);
                }
                else
                {
                    item.SubItems.Add(" ");
                }
                // 中項目
                if (categories.Length >= 2)
                {
                    item.SubItems.Add(categories[1]);
                }
                else
                {
                    item.SubItems.Add(" ");
                }
                // 小項目
                if (categories.Length >= 3)
                {
                    item.SubItems.Add(categories[2]);
                }
                else
                {
                    item.SubItems.Add(" ");
                }

                // 更新日時
                // "2016-05-08T02:06:25+09:00"
                // 未解決の場合
                if (string.IsNullOrEmpty(result.SolvedDate))
                {
                    string ymd = Regex.Match(result.PostedDate, "^\\d{4}-\\d{2}-\\d{2}").Value;
                    ymd = ymd.Replace('-', '/');
                    string time = Regex.Match(result.PostedDate, "(?<=T)\\d{2}:\\d{2}").Value;
                    item.SubItems.Add(ymd + " " + time);
                }
                // 解決済みの場合
                else
                {
                    string ymd = Regex.Match(result.SolvedDate, "^\\d{4}-\\d{2}-\\d{2}").Value;
                    ymd = ymd.Replace('-', '/');
                    string time = Regex.Match(result.SolvedDate, "(?<=T)\\d{2}:\\d{2}").Value;
                    item.SubItems.Add(ymd + " " + time);
                }

                // 状態
                string condition;
                switch (result.Condition)
                {
                    case "open":
                        condition = "受付中"; break;
                    case "vote":
                        condition = "投票中"; break;
                    case "solved":
                        condition = "解決済"; break;
                    default:
                        condition = " "; break;
                }
                item.SubItems.Add(condition);

                listViewArticles.Items.Add(item);
            }
        }

        private void UpdateBrowserDetail(Api.detailSearchResponse.ResultSet resultSet)
        {
            brsArticle.Document.GetElementById("contributor-content").InnerText
                = resultSet.Result.Content;

            string nickName = resultSet.Result.NickName;
            if (string.IsNullOrEmpty(nickName))
            {
                brsArticle.Document.GetElementById("contributor-name").InnerText
                    = "ID非公開";
            }
            else
            {
                brsArticle.Document.GetElementById("contributor-name").InnerText
                    = ChieUtil.DecryptNickName(nickName);
            }

        }

        public void UpdateCategoryTree()
        {
            // 先頭に「全てのカテゴリ」を追加する
            TreeNode allCategory = new TreeNode();
            allCategory.Text = "全てのカテゴリ";
            allCategory.Name = null;
            categoryTree.Nodes.Add(allCategory);

            // 大項目～小項目まで順に追加
            DataBase db = new DataBase();
            List<CategoryTreeModel> categoryList = db.GetCategoryTree();

            var query1 = from e in categoryList
                         where e.Level == 1
                         orderby e.CategoryId
                         select e;

            // level1 loop
            foreach (var item in query1)
            {
                TreeNode level1item = new TreeNode();
                level1item.Text = item.Title;
                level1item.Name = item.CategoryId;


                var queryLevel2items = categoryList
                    .Where(e => e.Level == 2 && e.ParentId == level1item.Name);

                // level2 loop
                foreach(var queryL2item in queryLevel2items)
                {
                    TreeNode level2item = new TreeNode();
                    level2item.Text = queryL2item.Title;
                    level2item.Name = queryL2item.CategoryId;

                    var queryLevel3items = categoryList
                        .Where(e => e.Level == 3 && e.ParentId == level2item.Name);

                    // level3 loop
                    foreach(var queryl3item in queryLevel3items)
                    {
                        TreeNode level3item = new TreeNode();
                        level3item.Text = queryl3item.Title;
                        level3item.Name = queryl3item.CategoryId;

                        level2item.Nodes.Add(level3item);
                    }
                    level1item.Nodes.Add(level2item);
                }
                categoryTree.Nodes.Add(level1item);
            }
        }

        public void UpdateCategoryComboBox(int level = 1, string categoryId = null)
        {
            // TODO: このあたりが雑

            // level: 1, categoryId: null のとき、大項目一覧を更新する
            if(level == 1 && string.IsNullOrEmpty(categoryId))
            {
                DataBase db = new DataBase();

                toolStripComboBoxLevel1.Items.Clear();
                toolStripComboBoxLevel2.Items.Clear();
                toolStripComboBoxLevel3.Items.Clear();

                var treeItems = db.GetCategoryTree().Where(item => item.Level == 1);
                toolStripComboBoxLevel1.Items.Add(new CategoryTreeModel("大分類"));
                toolStripComboBoxLevel1.Items.AddRange(treeItems.ToArray());
                toolStripComboBoxLevel2.Items.Add(new CategoryTreeModel("中分類"));
                toolStripComboBoxLevel3.Items.Add(new CategoryTreeModel("小分類"));


                toolStripComboBoxLevel1.SelectedIndex = 0;
                toolStripComboBoxLevel2.SelectedIndex = 0;
                toolStripComboBoxLevel3.SelectedIndex = 0;
            }
            // 大分類が選択されたとき
            // (「大分類」を選択したとき
            else if(level == 2 && string.IsNullOrEmpty(categoryId))
            {
                toolStripComboBoxLevel2.Items.Clear();
                toolStripComboBoxLevel2.Items.Add(new CategoryTreeModel("中分類"));
                toolStripComboBoxLevel3.Items.Clear();
                toolStripComboBoxLevel3.Items.Add(new CategoryTreeModel("小分類"));
                toolStripComboBoxLevel2.SelectedIndex = 0;
                toolStripComboBoxLevel3.SelectedIndex = 0;
            }
            // 大分類の有効な値を選択したとき
            else if(level == 2)
            {
                DataBase db = new DataBase();
                // 中分類を取得
                var treeItems = db.GetCategoryTree()
                    .Where(item => item.Level == 2 && item.ParentId == categoryId);
                //int size = treeItems.Count();
                toolStripComboBoxLevel2.Items.Clear();
                toolStripComboBoxLevel2.Items.Add(new CategoryTreeModel("中分類"));
                toolStripComboBoxLevel2.Items.AddRange(treeItems.ToArray());
                toolStripComboBoxLevel3.Items.Clear();
                toolStripComboBoxLevel3.Items.Add(new CategoryTreeModel("小分類"));
                toolStripComboBoxLevel2.SelectedIndex = 0;
                toolStripComboBoxLevel3.SelectedIndex = 0;
            }
            // 中分類が選択されたとき
            // (「中分類」を選択したとき)
            else if(level == 3 && string.IsNullOrEmpty(categoryId))
            {
                toolStripComboBoxLevel3.Items.Clear();
                toolStripComboBoxLevel3.Items.Add(new CategoryTreeModel("小分類"));
                toolStripComboBoxLevel3.SelectedIndex = 0;
            }
            // 有効な中分類が選択されたとき
            else if(level == 3)
            {
                toolStripComboBoxLevel3.Items.Clear();
                toolStripComboBoxLevel3.Items.Add(new CategoryTreeModel("小分類"));
                DataBase db = new DataBase();
                // 小分類を取得
                var treeItems = db.GetCategoryTree()
                    .Where(item => item.Level == 3 && item.ParentId == categoryId);
                toolStripComboBoxLevel3.Items.AddRange(treeItems.ToArray());
                toolStripComboBoxLevel3.SelectedIndex = 0;
            }
        }

        /***********************************************************/
        /* 通信処理 */
        /***********************************************************/

        public async Task GetArticleDetail(string questionId) {
            toolStripProgressBar.Value = 20;
            
            ApiCommand api = new ApiDetailSearchResponse();
            api.Timer.Start();
            api.SetParam("question_id", questionId);
            api.SetParam("sort", "-postdate"); // or +postdate
            api.SetParam("results", "100"); // default: 0
            api.SetParam("image_type", "0");
            api.SetParam("output", "xml");
            var result = await api.Send();
            Api.detailSearchResponse.ResultSet detail =
                api.LoadResultSet(result) as Api.detailSearchResponse.ResultSet;

            toolStripProgressBar.Value = 61;
            toolStripProgressBar.Value = 60;


            UpdateBrowserDetail(detail);

            toolStripProgressBar.Value = 81;
            toolStripProgressBar.Value = 80;

            api.Timer.Stop();
            long timeMs = api.Timer.ElapsedMilliseconds;
            statusStripMainText.Text = $"({timeMs}ms) 質問詳細を取得しました。";
            toolStripProgressBar.Value = 100;
        }


        public async Task GetNewQuestionList(object sender, string categoryId = null)
        {
            ApiCommand api = new ApiGetNewQuestionList();
            api.Timer.Start();
            api.SetParam("output", "xml");
            api.SetParam("condition", "open");
            api.SetParam("start", "1");
            api.SetParam("results", "20");
            if (!string.IsNullOrEmpty(categoryId))
            {
                api.SetParam("category_id", categoryId);
            }

            toolStripProgressBar.Value = 40;

            var result = await api.Send();
            Api.getNewQuestionList.ResultSet newQuestions =
                api.LoadResultSet(result) as Api.getNewQuestionList.ResultSet;
            //Api.getNewQuestionList.ResultSet newQuestions = ChieArticleManager.LoadNewQuestionList(result);

            toolStripProgressBar.Value = 80;

            UpdateListViewArticles(newQuestions);
            toolStripProgressBar.Value = 100;
            api.Timer.Stop();
            long timeMs = api.Timer.ElapsedMilliseconds;
            statusStripMainText.Text = $"({timeMs}ms) 新着質問リストを取得しました。";
        }

        public async Task GetSearchResultList(string query, string categoryId = null)
        {
            ApiCommand api = new ApiQuestionSearchResponse();
            api.Timer.Start();
            api.SetParam("query", query);
            api.SetParam("type", "all");
            if (!string.IsNullOrEmpty(categoryId)) api.SetParam("categoryid", categoryId);
            api.SetParam("condition", "all");
            api.SetParam("sort", "-posteddate");
            api.SetParam("posteddevice", "all");
            api.SetParam("results", "100");


            toolStripProgressBar.Value = 40;

            var result = await api.Send();
            Api.questionSearchResponse.ResultSet questions =
                api.LoadResultSet(result) as Api.questionSearchResponse.ResultSet;

            toolStripProgressBar.Value = 80;

            UpdateListViewArticles(questions);

            toolStripProgressBar.Value = 100;
            api.Timer.Stop();
            long timeMs = api.Timer.ElapsedMilliseconds;
            statusStripMainText.Text = $"({timeMs}ms) 検索結果を取得しました。";
        }
    }
}
