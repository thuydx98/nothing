using AnzamtechWS.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnzamtechWS.Areas.Admin.Controllers
{
    public class GiaiPhapController : Controller
    {
        public ActionResult XoaGP(int MaGP)
        {
            if (Session["GPPaging"] != null)
            {
                GiaiPhapModel model = (GiaiPhapModel)Session["GPPaging"];
                model.ID = MaGP;
                model.XoaGP();

            }
            return RedirectToAction("Index");
        }
        public ActionResult ChinhSuaGP(int MaGP)
        {
            GiaiPhapModel model = new GiaiPhapModel()
            {
                ID = MaGP
            };
            model.GetGiaiPhap();
            if (TempData["GPModel"] == null)
            {
                return View(model);
            }
            else
            {
                return View((GiaiPhapModel)TempData["GPModel"]);
            }
        }

        [HttpPost]
        public ActionResult ChinhSuaGP(GiaiPhapModel model)
        {
            if (ModelState.IsValid)
            {
           
                model.UpdateGP();
                TempData["GPModel"] = model;
                return RedirectToAction("ChinhSuaGP", new { @MaGP = model.ID });
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult Index(int ?pageid)
        {
            if (Session["GPPaging"] == null)
            {
                return View(new GiaiPhapModel());
            }
            else
            {
                GiaiPhapModel model = (GiaiPhapModel)Session["GPPaging"];
                model.PageNumber = (pageid == null) ? 1 : (int)pageid;
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Index(GiaiPhapModel model)
        {
            Session["GPPaging"] = model;
            return View(model);
        }
        // GET: Admin/GiaiPhap
        public ActionResult ThemGP()
        {
            if (TempData["GPModel"] == null)
            {
                return View(new GiaiPhapModel());
            }
            else
            {
                return View((GiaiPhapModel)TempData["GPModel"]);
            }
        }

        [HttpPost]
        public ActionResult ThemGP(GiaiPhapModel model)
        {
            if (ModelState.IsValid)
            {
                model.ThemGP();
                TempData["GPModel"] = model;
                return RedirectToAction("ThemGP");
            }
            else
            {
                return View(model);
            }

        }
    }
}