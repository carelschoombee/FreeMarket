using FreeMarket.Models;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace FreeMarket
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Roles.ApplicationName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            Membership.ApplicationName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new DateTimeModelBinder());

            try
            {
                if (Roles.RoleExists("Administrator") == false)
                    Roles.CreateRole("Administrator");

                if (Roles.RoleExists("Ellumin8") == false)
                    Roles.CreateRole("Ellumin8");

                if (!Roles.IsUserInRole("carelschoombee@gmail.co.za", "Ellumin8"))
                    Roles.AddUserToRole("carelschoombee@gmail.co.za", "Ellumin8");

                if (!Roles.IsUserInRole("carel@schoombeeandson.co.za", "Ellumin8"))
                    Roles.AddUserToRole("carel@schoombeeandson.co.za", "Ellumin8");

                if (!Roles.IsUserInRole("carel@schoombeeandson.co.za", "Administrator"))
                    Roles.AddUserToRole("carel@schoombeeandson.co.za", "Administrator");

                if (!Roles.IsUserInRole("johan@schoombeeandson.co.za", "Administrator"))
                    Roles.AddUserToRole("johan@schoombeeandson.co.za", "Administrator");
            }
            catch (Exception e)
            {
                ExceptionLogging.LogException(e);
            }
        }
    }
}
