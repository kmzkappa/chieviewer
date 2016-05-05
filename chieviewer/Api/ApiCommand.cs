using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;


namespace chieviewer.Api
{
    public abstract class ApiCommand
    {
        public static readonly string YahooApplicationId = "dj0zaiZpPURtV2Z4bWZ2ZExLMiZzPWNvbnN1bWVyc2VjcmV0Jng9Mjk-";

        protected Dictionary<string, string> urlParams;
        protected bool isValidParam = false;
        public Stopwatch Timer { get; set; }

        public ApiCommand()
        {
            urlParams = new Dictionary<string, string>();

            Timer = new System.Diagnostics.Stopwatch();
        }

        // URLパラメータ不正チェック
        public abstract void CheckUrlParams();


        public abstract Task<string> Send();

        public abstract object LoadResultSet(string xml);

        // URLパラメータ追加
        public void SetParam(string key, string value)
        {
            urlParams.Add(key, value);
        }


        // URLパラメータ組み立て
        public string BuildUrlParams(string baseUrl)
        {
            if (!isValidParam)
            {
                // TODO: 例外にすべき
                return null;
            }
            string url = baseUrl + "?appid=" + YahooApplicationId;
            foreach(var param in urlParams)
            {
                string p = "&" + param.Key + "=" + param.Value;
                url += p;
            }
            return url;
        }


        // 参考
        // http://www.atmarkit.co.jp/ait/articles/1501/06/news086.html
        // http://www.slideshare.net/neuecc/httpclient
        public async Task<string> GetApiAsync(Uri uri)
        {
            using (HttpClient client = new HttpClient())
            {
                
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "YahooAppID: " + YahooApplicationId);
                
                client.DefaultRequestHeaders.Add("Accept-Language", "ja-JP");
                client.Timeout = TimeSpan.FromSeconds(5.0);

                try
                {
                    return await client.GetStringAsync(uri).ConfigureAwait(false);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nError");
                    Exception ex = e;
                    while(ex != null)
                    {
                        Console.WriteLine("Exception message: {0}", ex.Message);
                        ex = ex.InnerException;
                    }

                }
                catch (TaskCanceledException e)
                {
                    Console.WriteLine("\nTimeout");
                    Console.WriteLine("Exception message: {0}", e.Message);
                }
                return null;
            }
        }

    }
}
