using System.Collections.Generic;
using System.Linq;

namespace FreeMarket.Models
{
    public class ArticlesViewModel
    {
        public List<Article> Articles { get; set; }

        public ArticlesViewModel()
        {
            List<SiteConfiguration> rawArticles = new List<SiteConfiguration>();
            Articles = new List<Article>();

            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                rawArticles = db.SiteConfigurations.ToList();

                if (rawArticles != null && rawArticles.Count > 0)
                {
                    rawArticles = rawArticles.Where(c => c.Key.StartsWith("Article")).ToList();

                    foreach (SiteConfiguration config in rawArticles)
                    {
                        string heading = getBetween(config.Value, "<h1>", "</h1>");
                        string content = config.Value;
                        Articles.Add(new Article { Title = heading, Content = content, Key = config.Key });
                    }
                }
            }
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
    }
}