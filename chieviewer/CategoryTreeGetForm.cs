using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chieviewer
{
    public partial class CategoryTreeGetForm : Form
    {
        private MainWindow parent;
        public CategoryTreeGetForm(MainWindow form)
        {
            parent = form;
            InitializeComponent();
        }
        public CategoryTreeGetForm()
        {
            InitializeComponent();
        }

        private  void CategoryTreeGetForm_Load(object sender, EventArgs e)
        {

        }

        private async void CategoryTreeGetForm_Shown(object sender, EventArgs e)
        {
            try
            {
                // DB初期化
                DataBase db = new DataBase();
                if (!db.CheckDb())
                {
                    progressBar1.Value = 1;
                    labelProgress1.Text = "データベースを作成しています。";
                    db.InitDb();
                    progressBar1.Value = 3;
                }
                if (db.IsEmptyCategoryTree())
                {
                    labelProgress1.Text = "カテゴリを取得しています。";

                    await db.InitCategoryTree(labelProgress2, progressBar1);
                    progressBar1.Value = 100;
                }

                // カテゴリツリー表示
                parent.UpdateCategoryTree();

                // 検索用のカテゴリ項目初期化
                parent.UpdateCategoryComboBox(1, null);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                parent.GetStatusLabel().Text = "カテゴリの取得に失敗しました。";
            }
            finally
            {
                this.Close();
            }
        }
    }
}
