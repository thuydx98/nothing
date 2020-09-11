using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnzamtechWS.Areas.User.Controllers
{
    public class DMSanPhamController : Controller
    {
        // GET: User/DMSanPham
        public ActionResult Index(string CatName, int? madm, string SubCat, int? madmc, int? id)
        {
            ViewBag.ID = (id ==null)?1:(int)id;
            ViewBag.CatName = CatName;
            ViewBag.SubCat = SubCat;
            ViewBag.MaDM = madm;
            ViewBag.MaDMC = madmc;
            return View();
        }
        public ActionResult GiaiPhap()
        {
            return View();
        }
        public ActionResult PhanMem()
        {
            return View();
        }
    }
}