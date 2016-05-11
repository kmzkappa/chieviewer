using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chieviewer
{
    public class NgListModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Word { get; set; }
        public bool Regex { get; set; }


        public NgListModel(int type, string word, bool useRegex)
        {
            Type = type;
            Word = word;
            Regex = useRegex;
        }
        public NgListModel() { }

        public override string ToString()
        {
            return Word;
        }

    }
}
