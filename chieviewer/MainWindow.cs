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
            

            // その他初期化
            using (StreamReader stream = new StreamReader("../../../template/default/article.html", Encoding.GetEncoding("UTF-8")))
            {
                brsArticle.DocumentText = await stream.ReadToEndAsync();
            }
            //await GetNewQuestionList(sender);
            
            
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
            toolStripProgressBar.Value = 21;
            toolStripProgressBar.Value = 20;
            string categoryId = categoryTree.SelectedNode.Name;
            await GetNewQuestionList(sender, categoryId);

            await Task.Delay(500);
            toolStripProgressBar.Value = 0;
        }

        /***********************************************************/
        /* 画面更新処理 */
        /***********************************************************/

        // 一覧部分の更新
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

        private void UpdateBrowserDetail(Api.detailSearchResponse.ResultSet resultSet)
        {
            brsArticle.Document.GetElementById("contributor-content").InnerText
                = resultSet.Result.Content;

            string nickName = resultSet.Result.NickName;
            brsArticle.Document.GetElementById("contributor-name").InnerText
                = ChieUtil.DecryptNickName(nickName);
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


    }
}
