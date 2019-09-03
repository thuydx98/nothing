using AnzamtechWS.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnzamtechWS.Areas.User.Controllers
{
    public class TinTucController : Controller
    {
        // GET: User/TinTuc
        public ActionResult Index(int? MaDMBV)
        {
            ViewBag.MaDMBV = MaDMBV;
            return View(new BaiVietModel());
        }

        public ActionResult ChiTietTT(int MaBV)
        {
            BaiVietModel model = new BaiVietModel()
            {
                MaBV = MaBV
            };
            return View(model.GetBaiVietByID());
        }
    }
}