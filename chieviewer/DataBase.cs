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

        public async void GetCategoryTree(ToolStripStatusLabel statusLabel, string categoryId = null)
        {
            ApiCommand api = new ApiCategoryTreeResponse();
            api.Timer.Start();


            var result = await api.Send();
            Api.categoryTreeResponse.ResultSet treeItems =
                api.LoadResultSet(result) as Api.categoryTreeResponse.ResultSet;





            api.Timer.Stop();
            long timeMs = api.Timer.ElapsedMilliseconds;
            statusLabel.Text = $"({timeMs}ms) カテゴリツリーを更新しました。";
        }
    }
}

//namespace chieviewer.Api.categoryTreeResponse
// 