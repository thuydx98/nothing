using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AnzamtechWS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Helper.Helper.ListTest = new List<string>();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            Task.Run(() =>
            {
                int i = 0;
                while (true)
                {
                    Helper.Helper.Count += 1;
                    Task wait = Task.Delay(2000);
                    wait.Wait();
                }
            });
        }
    }
}
