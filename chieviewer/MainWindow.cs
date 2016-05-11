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
using System.Net;

namespace chieviewer
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private List<WebBrowser> browsers;
        private string htmlTemplate;

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // 設定
            listViewArticles.FullRowSelect = true;         

            // その他初期化
            //await GetNewQuestionList(sender);
            toolStripTextBoxSearchQuery.Text = "キーワード";
            toolStripTextBoxSearchQuery.ForeColor = Color.DarkGray;
            toolStripTextBoxQuestionId.ForeColor = Color.DarkGray;
            toolStripTextBoxQuestionId.Text = "URL/質問ID";
            //toolStripButtonSearch.Font = new Font("FontAwesome", 9, FontStyle.Regular);

            htmlTemplate = File.ReadAllText("../../../template/default/article.html", Encoding.GetEncoding("UTF-8"));

            // ブラウザタブのオブジェクト生成とHTML代入のタイミングを分けるため
            // 枠を2つ分用意しておく
            // --> UpdateBrowserDetail
            browsers = new List<WebBrowser>(2);
            for(int i = 0; i < 2; i++)
            {
                WebBrowser browser = new WebBrowser();
                browser.DocumentText = htmlTemplate;
                browsers.Add(browser);
            }

        }

        // MainWindow_Load完了後の処理
        private void MainWindow_Shown(object sender, EventArgs e)
        {

            DataBase db = new DataBase();
            if (db.CheckDb() && !db.IsEmptyCategoryTree())
            {
                // カテゴリツリー表示
                UpdateCategoryTree();

                // 検索用のカテゴリ項目初期化
                UpdateCategoryComboBox(1, null);
            }
            else
            {
                // DB更新ダイアログ
                CategoryTreeGetForm form = new CategoryTreeGetForm(this);
                form.ShowDialog();
            }
        }

        /***********************************************************/
        /* フォーム・アクション */
        /***********************************************************/

        // 新着取得ボタン
        private async void btnGetNew_Click(object sender, EventArgs e)
        {
            string categoryId = "";
            categoryId = ((CategoryTreeModel)toolStripComboBoxLevel3.SelectedItem).CategoryId;
            if (string.IsNullOrEmpty(categoryId))
            {
                categoryId = ((CategoryTreeModel)toolStripComboBoxLevel2.SelectedItem).CategoryId;
            }
            if (string.IsNullOrEmpty(categoryId))
            {
                categoryId = ((CategoryTreeModel)toolStripComboBoxLevel1.SelectedItem).CategoryId;
            }

            await GetNewQuestionList(sender, categoryId);
        }

        // 記事リスト行が選択されたとき
        private async void listViewArticles_Click(object sender, EventArgs e)
        {
            string questionId = ((ListView)sender).SelectedItems[0].Text;

            // 既にタブに存在する記事の場合、タブを追加せずそれを表示する
            for (int i = 0; i < tabBrowser.TabCount; i++)
            {
                if(((Dictionary<string, string>)tabBrowser.TabPages[i].Tag)["QuestionId"] == questionId)
                {
                    tabBrowser.SelectTab(i);
                    toolStripTextBoxQuestionId.ForeColor = Color.Black;
                    toolStripTextBoxQuestionId.Text =
                        ((Dictionary<string, string>)tabBrowser.TabPages[i].Tag)["PcQuestionUrl"];
                    return;
                }
            }

            await GetArticleDetail(questionId);

            await Task.Delay(500);
            toolStripProgressBar.Value = 0;
        }



        // 移動ボタン
        private async void toolStripButtonMoveQuestionId_Click(object sender, EventArgs e)
        {

            //string ymd = Regex.Match(result.UpdatedDate, "^\\d{4}-\\d{2}-\\d{2}").Value;
            //http://detail.chiebukuro.yahoo.co.jp/qa/question_detail/q10159112045

            string questionId = "";
            string regexUrl = "^http://detail.chiebukuro.yahoo.co.jp/qa/question_detail/q\\d+$";
            string regexQid = "^q?\\d+$";

            if(Regex.IsMatch(toolStripTextBoxQuestionId.Text, regexUrl) ||
                Regex.IsMatch(toolStripTextBoxQuestionId.Text, regexQid)){

                questionId = Regex.Match(toolStripTextBoxQuestionId.Text, "\\d+$").Value;
            }
            else
            {
                statusStripMainText.Text = "指定された質問IDまたはURLが不正です。";
                System.Media.SystemSounds.Beep.Play();
                return;
            }

            if (string.IsNullOrEmpty(questionId)) return;

            // 既にタブに存在する記事の場合、タブを追加せずそれを表示する
            for (int i = 0; i < tabBrowser.TabCount; i++)
            {
                if (((Dictionary<string, string>)tabBrowser.TabPages[i].Tag)["QuestionId"] == questionId)
                {
                    tabBrowser.SelectTab(i);
                    return;
                }
            }

            await GetArticleDetail(questionId);
        }

        // カテゴリツリー選択時
        private async void categoryTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // ツリーを展開する
            //e.Node.Toggle();


            // 記事一覧を更新する
            toolStripProgressBar.Value = 21;
            toolStripProgressBar.Value = 20;

            string categoryId = categoryTree.SelectedNode.Name;

            try
            {
                await GetNewQuestionList(sender, categoryId);
            }
            // TODO: 不要
            catch (NullReferenceException ex)
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

        // URLボックス(enter)
        private void toolStripTextBoxQuestionId_Enter(object sender, EventArgs e)
        {
            if (toolStripTextBoxQuestionId.ForeColor == Color.DarkGray)
            {
                toolStripTextBoxQuestionId.Text = "";
                toolStripTextBoxQuestionId.ForeColor = Color.Black;
            }
        }

        // URLボックス(leave)
        private void toolStripTextBoxQuestionId_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(toolStripTextBoxQuestionId.Text))
            {
                toolStripTextBoxQuestionId.ForeColor = Color.DarkGray;
                toolStripTextBoxQuestionId.Text = "URL/質問ID";
            }
        }

        // 検索ボタン
        private async void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = toolStripTextBoxSearchQuery.Text;

            if (toolStripTextBoxSearchQuery.ForeColor != Color.DarkGray
                && !string.IsNullOrEmpty(toolStripTextBoxSearchQuery.Text))
            {
                string categoryId = "";
                categoryId = ((CategoryTreeModel)toolStripComboBoxLevel3.SelectedItem).CategoryId;
                if (string.IsNullOrEmpty(categoryId))
                {
                    categoryId = ((CategoryTreeModel)toolStripComboBoxLevel2.SelectedItem).CategoryId;
                }
                if (string.IsNullOrEmpty(categoryId))
                {
                    categoryId = ((CategoryTreeModel)toolStripComboBoxLevel1.SelectedItem).CategoryId;
                }

                await GetSearchResultList(searchQuery, categoryId);
            }
            // 空白の場合
            else
            {
                statusStripMainText.Text = "質問の検索にはキーワードの指定が必要です。";
                System.Media.SystemSounds.Beep.Play();
            }
        }

        // 検索ボックスのEnterキー処理
        private async void toolStripTextBoxSearchQuery_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // 検索実行
                string searchQuery = toolStripTextBoxSearchQuery.Text;
                if (toolStripTextBoxSearchQuery.ForeColor != Color.DarkGray
                    && !string.IsNullOrEmpty(toolStripTextBoxSearchQuery.Text))
                {
                    e.Handled = true;
                    string categoryId = "";
                    categoryId = ((CategoryTreeModel)toolStripComboBoxLevel3.SelectedItem).CategoryId;
                    if (string.IsNullOrEmpty(categoryId))
                    {
                        categoryId = ((CategoryTreeModel)toolStripComboBoxLevel2.SelectedItem).CategoryId;
                    }
                    if (string.IsNullOrEmpty(categoryId))
                    {
                        categoryId = ((CategoryTreeModel)toolStripComboBoxLevel1.SelectedItem).CategoryId;
                    }

                    await GetSearchResultList(searchQuery, categoryId);
                }
                // 空白の場合
                else
                {
                    statusStripMainText.Text = "質問の検索にはキーワードの指定が必要です。";
                    System.Media.SystemSounds.Beep.Play();
                }
            }
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

        // カテゴリツリー（閉）をダブルクリックしたとき、開く→閉じる が発生しないよう抑制する
        System.Diagnostics.Stopwatch expandTimer = new System.Diagnostics.Stopwatch();
        // 展開時に（開）アイコンに変更
        private void categoryTree_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 1;
            e.Node.SelectedImageIndex = 1;
            expandTimer.Restart();
        }
        // 閉じる際に（閉）アイコンに変更
        private void categoryTree_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 0;
            e.Node.SelectedImageIndex = 0;
        }
        // カテゴリをシングルクリックで開く
        private void categoryTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!e.Node.IsExpanded)
            {
                e.Node.Expand();
            }
        }

        // 一度オープンしたら、一定時間内はクリックしても閉じない
        private void categoryTree_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.IsExpanded && expandTimer.ElapsedMilliseconds > 400)
            {
                // noop
            }
            else
            {
                e.Cancel = true;
            }
        }

        // タブ部分のマウス処理
        private void tabBrowser_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Middle)
            {
                for (int i = 0; i < tabBrowser.TabCount; i++)
                {
                    // タブとマウス位置を比較し、クリックしたタブを除去
                    if (tabBrowser.GetTabRect(i).Contains(e.X, e.Y))
                    {
                        tabBrowser.TabPages.RemoveAt(i);
                        break;
                    }
                }
            }
            else if(e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < tabBrowser.TabCount; i++)
                {
                    // 選択したタブのURLを取り出してURLボックスに反映する
                    if (tabBrowser.GetTabRect(i).Contains(e.X, e.Y))
                    {
                        toolStripTextBoxQuestionId.ForeColor = Color.Black;
                        toolStripTextBoxQuestionId.Text =
                            ((Dictionary<string, string>)tabBrowser.TabPages[i].Tag)["PcQuestionUrl"];
                        break;
                    }
                }
            }
        }

        /***********************************************************/
        /* メニューバー */
        /***********************************************************/

        // 「バージョン情報」
        private void toolStripMenuItemVersionInfo_Click(object sender, EventArgs e)
        {
            VersionInfoForm form = new VersionInfoForm();
            form.ShowDialog();
        }

        // 「オプション」
        private void toolStripMenuItemOption_Click(object sender, EventArgs e)
        {
            OptionForm form = new OptionForm();
            form.ShowDialog();
        }

        /***********************************************************/
        /* 画面更新処理 */
        /***********************************************************/

        // 一覧部分の更新（新着記事用）
        private void UpdateListViewArticles(Api.getNewQuestionList.ResultSet resultSet)
        {
            DataBase db = new DataBase();
            var ngWords = db.GetNgWordList(DataBase.NgType.Word);

            listViewArticles.Items.Clear();
            foreach(var result in resultSet.Result)
            {
                bool skip = false;

                string contentText = WebUtility.HtmlDecode(result.Content);

                // NGワードチェック
                foreach (var ng in ngWords)
                {
                    if (ng.Regex)
                    {
                        // NGワード（正規表現）に引っかかった場合
                        Regex regex = new Regex(ng.Word);
                        if (regex.IsMatch(contentText))
                        {
                            skip = true;
                            break;
                        }

                    }
                    else
                    {
                        // NGワードに引っかかった場合
                        if (contentText.Contains(ng.Word))
                        {
                            skip = true;
                            break;
                        }
                    }
                }
                // NGワードに引っかかった
                if (skip) continue;

                ListViewItem item = new ListViewItem(result.QuestionId);

                item.SubItems.Add(contentText);
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
            DataBase db = new DataBase();
            var ngWords = db.GetNgWordList(DataBase.NgType.Word);

            listViewArticles.Items.Clear();
            foreach (var result in resultSet.Result)
            {
                bool skip = false;

                string contentText = WebUtility.HtmlDecode(result.Content);

                // NGワードチェック
                foreach (var ng in ngWords)
                {
                    if (ng.Regex)
                    {
                        // NGワード（正規表現）に引っかかった場合
                        Regex regex = new Regex(ng.Word);
                        if (regex.IsMatch(contentText))
                        {
                            skip = true;
                            break;
                        }

                    }
                    else
                    {
                        // NGワードに引っかかった場合
                        if (contentText.Contains(ng.Word))
                        {
                            skip = true;
                            break;
                        }
                    }
                }
                // NGワードに引っかかった
                if (skip) continue;

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
            // タブ用
            // WebBrowserの生成直後にDocumentTextの代入ができないため、
            // new のタイミングと代入のタイミングを分ける
            browsers[1].DocumentText = htmlTemplate;
            WebBrowser browser = browsers[0];
            browsers.RemoveAt(0);
            browsers.Add(new WebBrowser());

            browser.Document.GetElementById("contributor-content").InnerText
                = resultSet.Result.Content;

            string nickName = resultSet.Result.NickName;
            if (string.IsNullOrEmpty(nickName))
            {
                browser.Document.GetElementById("contributor-name").InnerText
                    = "ID非公開";
            }
            else
            {
                browser.Document.GetElementById("contributor-name").InnerText
                    = ChieUtil.DecryptNickName(nickName);
            }

            browser.Dock = DockStyle.Fill;
            //browser.AllowWebBrowserDrop = false;
            browser.IsWebBrowserContextMenuEnabled = false;
            browser.ContextMenuStrip = contextMenuStripBrowser;

            // ブラウザ部分の右クリックメニューを設定
            browser.ContextMenuStrip.Opened += new EventHandler(contextMenuStripBrowser_Opened);

            TabPage tab = new TabPage();
            // 質問ID, URL
            tab.Tag = new Dictionary<string, string>()
            {
                {"QuestionId", resultSet.Result.QuestionId},
                {"PcQuestionUrl", resultSet.Result.PcQuestionUrl}
            };

            if(resultSet.Result.Title.Length > 8)
            {
                tab.Text = resultSet.Result.Title.Substring(0, 8);
            }
            else
            {
                tab.Text = resultSet.Result.Title;
            }
            tab.Controls.Add(browser);
            tabBrowser.TabPages.Add(tab);
            // 追加したタブを表示
            tabBrowser.SelectedIndex = tabBrowser.TabCount - 1;
            
        }

        // ブラウザの右クリックメニュー
        // UpdateBrowserDetail内でイベントを追加している
        private void contextMenuStripBrowser_Opened(object sender, EventArgs e)
        {
            WebBrowser browser = ((ContextMenuStrip)sender).SourceControl as WebBrowser;

            // 文字列が選択されていなければ「コピー」を無効化する
            IHTMLDocument2 htmlDocument = browser.Document.DomDocument as IHTMLDocument2;
            IHTMLSelectionObject currentSelection = htmlDocument.selection;
            if (currentSelection != null)
            {
                IHTMLTxtRange range = currentSelection.createRange() as IHTMLTxtRange;
                if (range.text == null)
                {
                    contextBrowserCopy.Enabled = false;
                    contextBrowserAddNgName.Enabled = false;
                    contextBrowserAddNgWord.Enabled = false;
                }
                else
                {
                    contextBrowserCopy.Enabled = true;
                    contextBrowserAddNgName.Enabled = true;
                    contextBrowserAddNgWord.Enabled = true;
                }
            }
        }

        // ブラウザの右クリックメニュー「コピー」処理
        private void contextBrowserCopy_Click(object sender, EventArgs e)
        {
            ContextMenuStrip strip = ((ToolStripMenuItem)sender).Owner as ContextMenuStrip;
            WebBrowser browser = strip.SourceControl as WebBrowser;
            browser.Document.ExecCommand("Copy", false, null);
        }

        // ブラウザの右クリックメニュー「NGワードに追加」
        private void contextTbowserAddNgWord_Click(object sender, EventArgs e)
        {
            ContextMenuStrip strip = ((ToolStripMenuItem)sender).Owner as ContextMenuStrip;
            WebBrowser browser = strip.SourceControl as WebBrowser;
            IHTMLDocument2 htmlDocument = browser.Document.DomDocument as IHTMLDocument2;
            IHTMLSelectionObject currentSelection = htmlDocument.selection;
            IHTMLTxtRange range = currentSelection.createRange() as IHTMLTxtRange;

            // 確認ダイアログ表示
            DialogResult result = MessageBox.Show(
                $"「{range.text}」をNGワードに追加しますか？",
                "NGワード追加",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if(result == DialogResult.Yes)
            {
                DataBase db = new DataBase();
                db.AddNgWord(DataBase.NgType.Word, range.text);
            }
        }

        // ブラウザの右クリックメニュー「NGネームに追加」
        private void contextBrowserAddNgName_Click(object sender, EventArgs e)
        {
            ContextMenuStrip strip = ((ToolStripMenuItem)sender).Owner as ContextMenuStrip;
            WebBrowser browser = strip.SourceControl as WebBrowser;
            IHTMLDocument2 htmlDocument = browser.Document.DomDocument as IHTMLDocument2;
            IHTMLSelectionObject currentSelection = htmlDocument.selection;
            IHTMLTxtRange range = currentSelection.createRange() as IHTMLTxtRange;

            // 確認ダイアログ表示
            DialogResult result = MessageBox.Show(
                $"「{range.text}」をNGネームに追加しますか？",
                "NGネーム追加",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DataBase db = new DataBase();
                db.AddNgWord(DataBase.NgType.Word, range.text);
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

            // 子ノードが存在しない要素のアイコンを変更する
            foreach(TreeNode lv1 in categoryTree.Nodes)
            {
                if(lv1.GetNodeCount(false) == 0)
                {
                    lv1.ImageIndex = 2;
                    lv1.SelectedImageIndex = 2;
                }
                foreach(TreeNode lv2 in lv1.Nodes)
                {
                    if (lv2.GetNodeCount(false) == 0)
                    {
                        lv2.ImageIndex = 2;
                        lv2.SelectedImageIndex = 2;
                    }
                    foreach(TreeNode lv3 in lv2.Nodes)
                    {
                        lv3.ImageIndex = 2;
                        lv3.SelectedImageIndex = 2;
                    }
                }
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

            toolStripTextBoxQuestionId.ForeColor = Color.Black;
            toolStripTextBoxQuestionId.Text = detail.Result.PcQuestionUrl;

            UpdateBrowserDetail(detail);

            toolStripProgressBar.Value = 81;
            toolStripProgressBar.Value = 80;

            api.Timer.Stop();
            long timeMs = api.Timer.ElapsedMilliseconds;
            statusStripMainText.Text = $"({timeMs}ms) 質問詳細を取得しました。";
            toolStripProgressBar.Value = 100;
        }

        // 新着質問取得
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

            if(newQuestions.totalResultsReturned != "0")
            { 
                UpdateListViewArticles(newQuestions);
            }
            toolStripProgressBar.Value = 100;
            api.Timer.Stop();
            long timeMs = api.Timer.ElapsedMilliseconds;
            if (newQuestions.totalResultsReturned == "0")
            {
                statusStripMainText.Text = $"({timeMs}ms) 新着の質問はありません。";
            }
            else
            {
                statusStripMainText.Text = $"({timeMs}ms) 新着質問リストを取得しました。";
            }
        }

        // 検索結果取得
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

        /***********************************************************/
        /* アクセサ */
        /***********************************************************/
        public ToolStripStatusLabel GetStatusLabel()
        {
            return statusStripMainText;
        }

    }
}
