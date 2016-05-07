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
                db.InitDb();
            }
            if (db.IsEmptyCategoryTree())
            {
                db.InitCategoryTree(statusStripMainText);
            }
            

            // その他初期化
            using (StreamReader stream = new StreamReader("../../../template/default/article.html", Encoding.GetEncoding("UTF-8")))
            {
                brsArticle.DocumentText = await stream.ReadToEndAsync();
            }
            await GetNewQuestionList(sender);
            
            
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


        /***********************************************************/
        /* 画面更新処理 */
        /***********************************************************/

        // 一覧部分の更新
        private void UpdateListViewArticles(Api.getNewQuestionList.ResultSet resultSet)
        {
            foreach(var result in resultSet.Result)
            {
                ListViewItem item = new ListViewItem(result.QuestionId);
                item.SubItems.Add(result.Content);
                item.SubItems.Add(result.Coin);
                //item.SubItems.Add(result.)
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


        /***********************************************************/
        /* 通信処理 */
        /***********************************************************/

        public async Task GetArticleDetail(string questionId) { 
            
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

            UpdateBrowserDetail(detail);

            api.Timer.Stop();
            long timeMs = api.Timer.ElapsedMilliseconds;
            statusStripMainText.Text = $"({timeMs}ms) 質問詳細を取得しました。";
        }


        public async Task GetNewQuestionList(object sender)
        {
            ApiCommand api = new ApiGetNewQuestionList();
            api.Timer.Start();
            api.SetParam("output", "xml");
            api.SetParam("condition", "open");
            api.SetParam("start", "1");
            api.SetParam("results", "20");
            var result = await api.Send();
            Api.getNewQuestionList.ResultSet newQuestions =
                api.LoadResultSet(result) as Api.getNewQuestionList.ResultSet;
            //Api.getNewQuestionList.ResultSet newQuestions = ChieArticleManager.LoadNewQuestionList(result);
            UpdateListViewArticles(newQuestions);
            api.Timer.Stop();
            long timeMs = api.Timer.ElapsedMilliseconds;
            statusStripMainText.Text = $"({timeMs}ms) 新着質問リストを取得しました。";
        }
    }
}
