﻿using System;
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
                using (SQLiteCommand cmd = dbconn.CreateCommand())
                {
                    cmd.CommandText = "create table ng_list(";
                    cmd.CommandText += "id INTEGER PRIMARY KEY, type INTEGER, word TEXT, regex BOOLEAN";
                    cmd.CommandText += ");";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool IsEmptyCategoryTree()
        {
            try
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
            catch(SQLiteException ex)
            {
                Console.WriteLine(ex.InnerException);
                return true;
            }
        }

        public async Task InitCategoryTree(Label statusLabel, ProgressBar progressBar)
        {
            List<CategoryTreeModel> categoryList = await GetCategoryTreeApi(statusLabel, progressBar);

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

                    progressBar.Value = 95;

                    using(SQLiteCommand cmd = dbconn.CreateCommand())
                    {
                        foreach(var category in categoryList)
                        {
                            cmd.CommandText =
                                "insert into category_tree(category_id, category_path, title, title_path, parent_id, level) ";
                            cmd.CommandText +=
                                "values (@CATEGORY_ID, @CATEGORY_PATH, @TITLE, @TITLE_PATH, @PARENT_ID, @LEVEL);";
                            cmd.Parameters.Add(new SQLiteParameter("@CATEGORY_ID", category.CategoryId));
                            cmd.Parameters.Add(new SQLiteParameter("@CATEGORY_PATH", category.CategoryPath));
                            cmd.Parameters.Add(new SQLiteParameter("@TITLE", category.Title));
                            cmd.Parameters.Add(new SQLiteParameter("@TITLE_PATH", category.TitlePath));
                            cmd.Parameters.Add(new SQLiteParameter("@PARENT_ID", category.ParentId));
                            cmd.Parameters.Add(new SQLiteParameter("@LEVEL", category.Level));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
            }
            progressBar.Value = 100;
        }

        // APIを発行しカテゴリ一覧を取得する
        public async Task<List<CategoryTreeModel>> GetCategoryTreeApi(Label statusLabel, ProgressBar progressBar)
        {
            ApiCommand api = new ApiCategoryTreeResponse();
            api.Timer.Start();

            // 大項目の取得
            var result = await api.Send();
            Api.categoryTreeResponse.ResultSet level1Items =
                api.LoadResultSet(result) as Api.categoryTreeResponse.ResultSet;

            List<CategoryTreeModel> categoryTreeList = new List<CategoryTreeModel>();

            progressBar.Value = 5;
            int step = 90 / level1Items.Result.Length;

            foreach(var level1item in level1Items.Result)
            {
                statusLabel.Text = level1item.Title;

                CategoryTreeModel item1 = new CategoryTreeModel(level1item, 1);
                item1.ParentId = null;
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
                    item2.ParentId = item1.CategoryId;
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
                        item3.ParentId = item2.CategoryId;
                        categoryTreeList.Add(item3);
                    }
                }
                progressBar.Value += step;
            }

            api.Timer.Stop();
            long timeMs = api.Timer.ElapsedMilliseconds;
            statusLabel.Text = $"({timeMs}ms) カテゴリツリーを取得しました。";
            return categoryTreeList;
        }

        // DBからカテゴリ一覧を取得
        public List<CategoryTreeModel> GetCategoryTree()
        {
            // TODO: 戻り値は参照渡しの方が良さそう
            List<CategoryTreeModel> categoryList = new List<CategoryTreeModel>();

            using (SQLiteConnection dbconn = new SQLiteConnection("data Source=" + DbFileName))
            {
                dbconn.Open();
                using(SQLiteCommand cmd = dbconn.CreateCommand())
                {
                    cmd.CommandText = "select * from category_tree order by category_id asc;";
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CategoryTreeModel treeItem = new CategoryTreeModel();
                        treeItem.CategoryId = reader["category_id"].ToString();
                        treeItem.CategoryPath = reader["category_path"].ToString();
                        treeItem.Title = reader["title"].ToString();
                        treeItem.TitlePath = reader["title_path"].ToString();
                        treeItem.ParentId = reader["parent_id"].ToString();
                        treeItem.Level = int.Parse(reader["level"].ToString());
                        categoryList.Add(treeItem);
                    }
                }
            }
            return categoryList;
        }

        // NGワード・ネーム登録
        // type: 1=名前、2=単語
        public Int64 AddNgWord(NgType type, string word, bool useRegex = false)
        {
            Int64 lastInsertRowId = -1;
            using (SQLiteConnection dbconn = new SQLiteConnection("data Source=" + DbFileName))
            {
                dbconn.Open();

                using (var transaction = dbconn.BeginTransaction())
                {
                    using (SQLiteCommand cmd = dbconn.CreateCommand())
                    {
                        cmd.CommandText = "insert into ng_list(type, word, regex) ";
                        cmd.CommandText += "values (@TYPE, @WORD, @REGEX);";
                        cmd.Parameters.Add(new SQLiteParameter("@TYPE", type));
                        cmd.Parameters.Add(new SQLiteParameter("@WORD", word));
                        cmd.Parameters.Add(new SQLiteParameter("@REGEX", useRegex));
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }

                using (SQLiteCommand cmd = dbconn.CreateCommand())
                {
                    cmd.CommandText = @"select last_insert_rowid()";
                    lastInsertRowId = (Int64)cmd.ExecuteScalar();
                }
            }
            return lastInsertRowId;
        }

        public void UpdateNgWord(NgType type, int id, string word, bool useRegex)
        {
            using (SQLiteConnection dbconn = new SQLiteConnection("data Source=" + DbFileName))
            {
                dbconn.Open();
                using (var transaction = dbconn.BeginTransaction())
                {
                    using (SQLiteCommand cmd = dbconn.CreateCommand())
                    {
                        cmd.CommandText = "update ng_list set word=@WORD, regex=@REGEX where id=@ID;";
                        cmd.Parameters.Add(new SQLiteParameter("@WORD", word));
                        cmd.Parameters.Add(new SQLiteParameter("@REGEX", useRegex));
                        cmd.Parameters.Add(new SQLiteParameter("@ID", id));
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
        }

        public List<NgListModel> GetNgWordList(NgType type)
        {
            List<NgListModel> ngList = new List<NgListModel>();

            using (SQLiteConnection dbconn = new SQLiteConnection("data Source=" + DbFileName))
            {
                dbconn.Open();
                using (SQLiteCommand cmd = dbconn.CreateCommand())
                {
                    cmd.CommandText = "select * from ng_list where type=@TYPE;";
                    cmd.Parameters.Add(new SQLiteParameter("@TYPE", (int)type));
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        NgListModel model = new NgListModel();
                        model.Id = int.Parse(reader["id"].ToString());
                        model.Type = int.Parse((reader["type"].ToString()));
                        model.Word = reader["word"].ToString();
                        model.Regex = (bool)reader["regex"];
                        ngList.Add(model);
                    }
                }
            }
            return ngList;
        }

        public void DeleteNgWordList(NgType type, int pk)
        {
            using (SQLiteConnection dbconn = new SQLiteConnection("data Source=" + DbFileName))
            {
                dbconn.Open();
                using (var transaction = dbconn.BeginTransaction())
                {

                    using (SQLiteCommand cmd = dbconn.CreateCommand())
                    {
                        cmd.CommandText = "delete from ng_list where type=@TYPE and id=@ID";
                        cmd.Parameters.Add(new SQLiteParameter("@TYPE", (int)type));
                        cmd.Parameters.Add(new SQLiteParameter("@ID", pk));
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
        }

        public enum NgType
        {
            Name = 1,
            Word = 2
        }
    }
}
