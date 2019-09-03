using AnzamtechWS.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnzamtechWS.Areas.Admin.Controllers
{
    public class DanhMucController : Controller
    {
        public ActionResult Index()
        {
            return View(new DanhMucModel());
        }

        [HttpPost]
        public ActionResult Index(DanhMucModel model)
        {
            ViewData["Result"] = true;
            return View(model);
        }

        public ActionResult ThemDM()
        {
            if (TempData["ModelDM"] != null)
            {
                return View((GroupDanhMuc)TempData["ModelDM"]);
            }
            else
            {
                return View(new GroupDanhMuc());
            }
        }

        public ActionResult ChinhSuaDMC(int MaDMC)
        {

            DanhMucConModel model = new DanhMucConModel();
            model.MaDMC = MaDMC;
            model.GetDAnhMucConByID();
            return View(model);
        }

        [HttpPost]
        public ActionResult ChinhSuaDMC(DanhMucConModel model)
        {
            try
            {
                model.Update();
                model.Error = ErrorStatus.Message;
                model.Message = "Sửa danh mục con thành công";
            }
            catch (Exception)
            {
                model.Error = ErrorStatus.DatabaseError;
                model.Message = "Sửa danh mục con thất bại";
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ThemDM(GroupDanhMuc model)
        {
            if (ModelState.IsValid)
            {
                model.DanhMucModel.ThemDM();
                TempData["ModelDM"] = model;
                return RedirectToAction("ThemDM");
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult ThemDMC(GroupDanhMuc model)
        {
            if (ModelState.IsValid)
            {
                model.DanhMucConModel.ThemDMC();
                TempData["ModelDM"] = model;
                return RedirectToAction("ThemDM");
            }
            else
            {
                return View("ThemDM", model);
            }
        }
        

        public JsonResult GetDanhMuc()
        {
            DanhMucModel model = new DanhMucModel();
            return Json(model.GetJSONDSDanhMuc(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDanhMucCon(int id)
        {
            DanhMucConModel model = new DanhMucConModel() {
                MaDM = id
            };
            return Json(model.GetDanhMucCons(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChinhSuaDM(DanhMucModel model)
        {
            try
            {
                model.UpdateDanhMuc();
                model.Error = ErrorStatus.Message;
                model.Message = "Sửa danh mục thành công";
            }
            catch (Exception)
            {
                model.Error = ErrorStatus.DatabaseError;
                model.Message = "Sửa danh mục thất bại";
            }
            model.GetDAnhMucByID();
            return View(model);
        }
        public ActionResult ChinhSuaDM(int MaDM)
        {
            DanhMucModel model = new DanhMucModel()
            {
                MaDM = MaDM
            };
            model.GetDAnhMucByID();
            return View(model);
        }
    }
}