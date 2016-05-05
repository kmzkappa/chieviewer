using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace chieviewer.Api
{
    class ApiGetNewQuestionList : ApiCommand
    {
        private static readonly string BaseUrl = "http://chiebukuro.yahooapis.jp/Chiebukuro/V1/getNewQuestionList";

        public async override Task<string> Send()
        {
            CheckUrlParams();
            string url = BuildUrlParams(BaseUrl);

            string res = null;
            int count = 0;
            do
            {
                res = await GetApiAsync(new Uri(url));
                count++;
            } while (res != null && count < 3);


            //return await GetApiAsync(new Uri(url));
            return res;

        }

        public override void CheckUrlParams()
        {
            // 必須パラメータチェック
            if (!urlParams.ContainsKey("condition")) throw new ArgumentException();

            // このメソッドが実行されたことを記憶
            isValidParam = true;
        }

        public override object LoadResultSet(string xml)
        {
            TextReader reader = new StringReader(xml);
            XmlSerializer serializer = new XmlSerializer(typeof(getNewQuestionList.ResultSet));
            getNewQuestionList.ResultSet articles = serializer.Deserialize(reader) as getNewQuestionList.ResultSet;
            return articles;
        }

    }
}
