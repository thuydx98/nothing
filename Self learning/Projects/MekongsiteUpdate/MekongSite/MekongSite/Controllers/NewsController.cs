using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MekongSite.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            string[] list = Directory.GetFiles(Server.MapPath("~/Data/"));
            List<string> MyContents = new List<string>();
            foreach(string name in list)
            {
                var fileContents = System.IO.File.ReadAllText(name);
                MyContents.Add(fileContents);
            }


            return View(MyContents);
        }
    }
}