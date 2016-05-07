using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using chieviewer.Api;
using System.Windows.Forms;

namespace chieviewer
{
    public class DataBase
    {
        public static readonly string DbFileName = "viewer.db";
        public bool CheckDb()
        {
            return File.Exists(DbFileName);
        }

        public void InitDb()
        {
            if (File.Exists(DbFileName))
            {
                File.Delete(DbFileName);
            }
            using(SQLiteConnection dbconn = new SQLiteConnection("Data Source=" + DbFileName))
            {
                dbconn.Open();
                using (SQLiteCommand cmd = dbconn.CreateCommand())
                {
                    cmd.CommandText = "create table category_tree(";
                    cmd.CommandText += "id INTEGER PRIMARY KEY, category_id TEXT, category_path TEXT, title TEXT, title_path TEXT, parent_id TEXT, level INTEGER";
                    cmd.CommandText += ");";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool IsEmptyCategoryTree()
        {
            using (SQLiteConnection dbconn = new SQLiteConnection("data Source=" + DbFileName))
            {
                int rowCount = -1;
                dbconn.Open();
                using (SQLiteCommand cmd = dbconn.CreateCommand())
                {
                    cmd.CommandText = "select count(1) from category_tree;";
                    cmd.CommandType = System.Data.CommandType.Text;
                    rowCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
                return rowCount <= 0;
            }
        }

        public void InitCategoryTree(ToolStripStatusLabel statusLabel)
        {
            using (SQLiteConnection dbconn = new SQLiteConnection("data Source=" + DbFileName))
            {
                dbconn.Open();

                using (var transaction = dbconn.BeginTransaction())
                {
                    using(SQLiteCommand cmd = dbconn.CreateCommand())
                    {
                        cmd.CommandText = "delete from category_tree where 1=1;";
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    /*
                     * placeholderを使う方法
                     * cmd.CommandText = "insert into tablename(id, data) values (@ID, @DATA)";
                     * cmd.Parameters.Add(new SQLiteParameter("@ID", 1));
                     * cmd.Parameters.Add(new SQLiteParameter("@DATA", 1));
                     * cmd.ExecuteNonQuery(); 
                     */
                }
            }
            GetCategoryTree(statusLabel);
        }

        public async void GetCategoryTree(ToolStripStatusLabel statusLabel)
        {
            ApiCommand api = new ApiCategoryTreeResponse();
            api.Timer.Start();

            // 大項目の取得
            var result = await api.Send();
            Api.categoryTreeResponse.ResultSet level1Items =
                api.LoadResultSet(result) as Api.categoryTreeResponse.ResultSet;

            List<CategoryTreeModel> categoryTreeList = new List<CategoryTreeModel>();

            foreach(var level1item in level1Items.Result)
            {
                CategoryTreeModel item1 = new CategoryTreeModel(level1item, 1);
                categoryTreeList.Add(item1);

                // レベル1のIDでレベル2を検索する
                ApiCommand api2 = new ApiCategoryTreeResponse();
                api2.SetParam("categoryid", item1.CategoryId);
                var res2 = await api2.Send();
                Api.categoryTreeResponse.ResultSet level2items =
                    api.LoadResultSet(res2) as Api.categoryTreeResponse.ResultSet;

                int cnt = 0;
                foreach(var level2item in level2items.Result)
                {
                    // 1番目はレベル１相当のカテゴリ情報のため除外
                    // TODO: IdPathの階層をみて判断すべき
                    if (cnt++ == 0) continue;
                    CategoryTreeModel item2 = new CategoryTreeModel(level2item, 2);
                    categoryTreeList.Add(item2);

                    // レベル２のIDでレベル３を検索する
                    ApiCommand api3 = new ApiCategoryTreeResponse();
                    api3.SetParam("categoryid", item2.CategoryId);
                    var res3 = await api3.Send();
                    Api.categoryTreeResponse.ResultSet level3items =
                        api.LoadResultSet(res3) as Api.categoryTreeResponse.ResultSet;

                    int cnt2 = 0;
                    foreach(var level3item in level3items.Result)
                    {
                        if (cnt2++ == 0) continue;
                        CategoryTreeModel item3 = new CategoryTreeModel(level3item, 3);
                        categoryTreeList.Add(item3);
                    }
                }
            }

            



            api.Timer.Stop();
            long timeMs = api.Timer.ElapsedMilliseconds;
            statusLabel.Text = $"({timeMs}ms) カテゴリツリーを更新しました。";
        }
    }
}

//namespace chieviewer.Api.categoryTreeResponse
// 