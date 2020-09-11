using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AnzamtechWS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
           "Default1-Paging",
            "{CatName}-{madm}/Trang-{id}",
             new { controller = "DMSanPham", action = "Index" },
             new[] { "AnzamtechWS.Areas.User.Controllers" }
            ).DataTokens["area"] = "User";
            routes.MapRoute(
             "Default1",
              "{CatName}-{madm}",
               new { controller = "DMSanPham", action = "Index" },
               new[] { "AnzamtechWS.Areas.User.Controllers" }
            ).DataTokens["area"] = "User";

            routes.MapRoute(
            "Default2-paging",
             "{CatName}-{madm}/{SubCat}-{madmc}/Trang-{id}",
              new { controller = "DMSanPham", action = "Index" },
              new[] { "AnzamtechWS.Areas.User.Controllers" }
            ).DataTokens["area"] = "User";


            routes.MapRoute(
                "Default2",
                "{CatName}-{madm}/{SubCat}-{madmc}",
                new { controller = "DMSanPham", action = "Index" },
                new[] { "AnzamtechWS.Areas.User.Controllers" }
            ).DataTokens["area"] = "User";

            routes.MapRoute(
                "Route-CTSanPham",
                "{CatName}-{madm}/{SubCat}-{madmc}/{TenSP}-{id}",
                new { controller = "ChiTiet", action = "Index" },
                new[] { "AnzamtechWS.Areas.User.Controllers" }
            ).DataTokens["area"] = "User";

            routes.MapRoute(
                "Route-GiaiPhap",
                "GiaiPhap/PhanCung",
                new { controller = "DMSanPham", action = "GiaiPhap" },
                new[] { "AnzamtechWS.Areas.User.Controllers" }
             ).DataTokens["area"] = "User";

            routes.MapRoute(
                "Route-CTGP",
                "GiaiPhap/PhanCung/{TenGP}-{id}",
                new { controller = "ChiTiet", action = "GiaiPhap" },
                new[] { "AnzamtechWS.Areas.User.Controllers" }
            ).DataTokens["area"] = "User";
            routes.MapRoute(
               "Route-GPPM",
               "GiaiPhap/PhanMem/{TenGP}-{id}",
               new { controller = "ChiTiet", action = "PhanMem" },
               new[] { "AnzamtechWS.Areas.User.Controllers" }
           ).DataTokens["area"] = "User";


            routes.MapRoute(
               "Route-TinTuc-ChiTiet",
                "TinTuc/{tendmbv}-{MaDMBV}/{tentt}-{MaBV}",
                 new { controller = "TinTuc", action = "ChiTietTT" },
                 new[] { "AnzamtechWS.Areas.User.Controllers" }
               ).DataTokens["area"] = "User";
            routes.MapRoute(
               "Route-TinTuc",
                "TinTuc/{tentt}-{MaDMBV}",
                 new { controller = "TinTuc", action = "Index" },
                 new[] { "AnzamtechWS.Areas.User.Controllers" }
               ).DataTokens["area"] = "User";
            routes.MapRoute(
                  "Default",
                   "{controller}/{action}/{id}",
                    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                    new[] { "AnzamtechWS.Areas.User.Controllers" }
                ).DataTokens["area"] = "User";

        }
    }
}
