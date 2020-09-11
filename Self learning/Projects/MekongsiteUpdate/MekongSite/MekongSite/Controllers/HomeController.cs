using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MekongSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult MassageBody()
        {
            return View();
        }

        public ActionResult FootMassage()
        {
            return View();
        }
        public ActionResult Image()
        {
            //Layout HoiThao
            //Layout Khach hang
            //Layout NhanVien

            return View();
        }
    }
}