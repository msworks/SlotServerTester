using System;
using System.Linq;
using System.IO;
using System.Net;

namespace SlotServerTester
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        void Run()
        {
            var head = "http://web.ee-gaming.net/ps/";

            var _urls = new[]
            {
                head + "open.html?gameId=2&operator=1&token=abc&language=en&mode=1",
                head + "config.html",
                head + "init.html",
                head + "play.html?rate=10.00&betCount=3&power=123",
                head + "collect.html?reelStopLeft=1&reelStopCenter=2&reelStopRight=4&oshijun=123",
            };

            var urls = from gameId in Enumerable.Range(2, 1)
                       from userId in Enumerable.Range(1, 1)
                       from url in _urls
                       select String.Format(url, gameId, userId);

            var msg = String.Join(
                Environment.NewLine,
                urls.Select(url=> url + Environment.NewLine + Url2Json(url))
            );

            Console.WriteLine(msg);
            Console.ReadLine();
        }

        string Url2Json(string url)
        {
            var json = null as string;

            try
            {
                var req = WebRequest.Create(url);
                var res = req.GetResponse();
                var st = res.GetResponseStream();
                var sr = new StreamReader(st);
                json = sr.ReadToEnd();
                sr.Close();
                st.Close();
            }
            catch( Exception ex)
            {
                return ex.ToString();
            }

            return json;
        }
    }
}
