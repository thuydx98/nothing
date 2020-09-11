using AnzamtechWS.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnzamtechWS.Areas.Admin.Controllers
{
    public class HinhAnhController : Controller
    {
        ANZEntities anz = new ANZEntities();
        // GET: Admin/HinhAnh
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetImageByID(int id)
        {
            AnhModel model = new AnhModel()
            {
                MaAnh = id
            };
            return Json(model.GetAnhByID(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTypeImage()
        {
            var list = anz.LoaiAnhs.Select(x => new
            {
                MaLA = x.MaLA,
                TenLoai = x.TenLoai
            });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateAlbum()
        {
            DanhMucAnhModel model = new DanhMucAnhModel()
            {
                Path = "/image/BaiViet",
                TenDM = "BaiViet"
            };
            model.Add();
            model.UpdateID();
            return Json(model.Id, JsonRequestBehavior.AllowGet);
        }
    }
}