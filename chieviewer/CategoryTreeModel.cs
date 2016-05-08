using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chieviewer
{
    public class CategoryTreeModel
    {
        public int Id { get; set; }
        public string CategoryId { get; set; }
        public string CategoryPath { get; set; }
        public string Title { get; set; }
        public string TitlePath { get; set; }
        public string ParentId { get; set; }
        public int Level { get; set; }

        public CategoryTreeModel(Api.categoryTreeResponse.ResultSetCategory category, int level)
        {
            this.CategoryId = category.Id;
            this.CategoryPath = category.IdPath;
            this.Title = category.Title;
            this.TitlePath = category.Path;
            this.Level = level;
        }
        public CategoryTreeModel() { }
        public CategoryTreeModel(string title, string categoryId = null)
        {
            Title = title;
            CategoryId = categoryId;
        }

        public override string ToString()
        {
            return Title;
        }

    }
}
