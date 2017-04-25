using FreeMarket.Models;
using System.Linq;
using System.Web.Mvc;

namespace FreeMarket.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            WelcomeViewModel model = new WelcomeViewModel();

            return View(model);
        }

        public ActionResult ArticlesIndex()
        {
            ArticlesViewModel model = new ArticlesViewModel();

            return View(model);
        }

        public ActionResult DeliveryOptionsInfo()
        {
            DeliveryOptionsViewModel model = new DeliveryOptionsViewModel();
            return View(model);
        }

        public ActionResult About()
        {
            AboutViewModel model = new AboutViewModel();

            return View(model);
        }

        public ActionResult Contact()
        {
            Support support = new Support();

            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                support = db.Supports.FirstOrDefault();
            }

            return View(support);
        }

        public ActionResult TermsAndConditionsModal()
        {
            string terms = "";
            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                SiteConfiguration temp = db.SiteConfigurations
                    .Where(c => c.Key == "TermsAndConditions")
                    .FirstOrDefault();

                if (temp != null)
                    terms = temp.Value;
            }

            return PartialView("_TermsAndConditionsModal", terms);
        }

        public ActionResult TermsAndConditions()
        {
            TermsAndConditionsViewModel model = new TermsAndConditionsViewModel();
            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                SiteConfiguration temp = db.SiteConfigurations
                    .Where(c => c.Key == "TermsAndConditions")
                    .FirstOrDefault();

                if (temp != null)
                    model.Content = temp.Value;
            }

            return View("TermsAndConditions", model);
        }

        public ActionResult Privacy()
        {
            return View("Privacy");
        }

        [ChildActionOnly]
        public ActionResult SpecialMessage()
        {
            SpecialMessageViewModel model = new SpecialMessageViewModel();

            return PartialView("_SpecialMessage", model);
        }

        public ActionResult ViewArticleModal(string id)
        {
            Article model = new Article();
            using (FreeMarketEntities db = new FreeMarketEntities())
            {
                SiteConfiguration temp = db.SiteConfigurations
                    .Where(c => c.Key == id)
                    .FirstOrDefault();

                if (temp != null)
                {
                    string heading = getBetween(temp.Value, "<h1>", "</h1>");
                    model = new Article { Content = temp.Value, Key = temp.Key, Title = heading };
                }
            }

            return PartialView("_ViewArticleModal", model);
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