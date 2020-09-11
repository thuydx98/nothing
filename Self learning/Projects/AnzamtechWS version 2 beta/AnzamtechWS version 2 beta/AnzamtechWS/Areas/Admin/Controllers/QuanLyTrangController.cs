using AnzamtechWS.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnzamtechWS.Areas.Admin.Controllers
{
    public class QuanLyTrangController : Controller
    {
        // GET: Admin/QuanLyTrang
        public ActionResult Index()
        {
            if (TempData["ModelQLTrang"] == null)
            {
                return View(new QuanLyTrangModel());
            }
            else
            {
                return View((QuanLyTrangModel)TempData["ModelQLTrang"]);
            }
        }

        [HttpPost]
        public ActionResult Index(QuanLyTrangModel model)
        {
            model.Update();
            TempData["ModelQLTrang"] = model;
            return RedirectToAction("Index");
        }
    }
}