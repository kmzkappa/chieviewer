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
            listViewArticles.FullRowSelect = true;

            using (StreamReader stream = new StreamReader("../../template/default/article.html", Encoding.GetEncoding("UTF-8")))
            {
                brsArticle.DocumentText = await stream.ReadToEndAsync();
            }


        }

        // 新着一覧取得
        private async void btnGetNewQuestionList_Click(object sender, EventArgs e)
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

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        // 記事リスト行が選択されたとき
        private async void listViewArticles_Click(object sender, EventArgs e)
        {
            string questionId = ((ListView)sender).SelectedItems[0].Text;
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

        private void UpdateBrowserDetail(Api.detailSearchResponse.ResultSet resultSet)
        {

            brsArticle.Document.GetElementById("contributor-content").InnerText =
                    resultSet.Result.Content;
        }

        private void statusStripMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
