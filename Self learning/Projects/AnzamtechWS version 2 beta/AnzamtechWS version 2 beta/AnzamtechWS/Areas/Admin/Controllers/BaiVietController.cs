using AnzamtechWS.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AnzamtechWS.Areas.Admin.Controllers
{
    public class BaiVietController : Controller
    {
        public ActionResult XoaBV(int MaBV)
        {
            if (Session["TKPaging"] != null)
            {
                BaiVietModel model = (BaiVietModel)Session["TKPaging"];
                model.MaBV = MaBV;
                model.XoaBV();
                
            }
            return RedirectToAction("Index");
        }
        public ActionResult ChinhSuaBV(int MaBV)
        {
            BaiVietModel model = new BaiVietModel()
            {
                MaBV = MaBV
            };
            model.GetBaiViet();
            Session["ListTag"] = model.ListTags.ToList<Tag>();
            if (TempData["ModelBV"] == null)
            {
                return View(model);
            }
            else
            {
                return View((BaiVietModel)TempData["ModelBV"]);
            }
        }
        
        [HttpPost]
        public ActionResult ChinhSuaBV(BaiVietModel model)
        {
            if (ModelState.IsValid)
            {
                model.ListTags = (List<Tag>)Session["ListTag"];
                model.Update();
                TempData["ModelBV"] = model;
                Session["ListTag"] = null;
                return RedirectToAction("ChinhSuaBV", new { @MaBV = model.MaBV });
            }
            else
            {
                return View(model);
            }
        }

        // GET: Admin/BaiViet
        public ActionResult Index(int? pageid)
        {
            if (Session["TKPaging"] == null)
            {
                return View(new BaiVietModel());
            }
            else
            {
                BaiVietModel model = (BaiVietModel)Session["TKPaging"];
                model.PageNumber = (pageid == null) ? 1 : (int)pageid;
                return View(model);
            }          
        }
        [HttpPost]
        public ActionResult Index(BaiVietModel model)
        {
            Session["TKPaging"] = model;
            return View(model);
        }
        public ActionResult ThemBV()
        {
            Session["ListTag"] = new List<Tag>();
            if (TempData["BaiVietModel"] == null)
            {
                return View(new BaiVietModel());
            }
            else
            {
                return View((BaiVietModel)TempData["BaiVietModel"]);
            }           
        }

        [HttpPost]
        public ActionResult ThemBV(BaiVietModel model)
        {

            List<Tag> list = (List<Tag>)Session["ListTag"];
            model.MaNV = 2;
            if (list != null && list.Count > 0)
            {
                model.ListTags = list;
            }
            model.Add();
            TempData["BaiVietModel"] = model;
            return RedirectToAction("ThemBV");
        }

        public JsonResult ThemTags(int MaTag)
        {
            ANZEntities context = new ANZEntities();
            if (Session["ListTag"] != null)
            {

                List<Tag> list = (List<Tag>)Session["ListTag"];
                Tag t = list.FirstOrDefault<Tag>(u => u.MaTag == MaTag);
                if (t == null || t.MaTag == 0)
                {
                    list.Add(context.Tags.FirstOrDefault<Tag>(u => u.MaTag == MaTag));
                    Session["ListTag"] = list;
                }
                else 
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
               
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public JsonResult XoaTags(int MaTag)
        {
            ANZEntities context = new ANZEntities();
            if (Session["ListTag"] != null)
            {
                List<Tag> list = (List<Tag>)Session["ListTag"];
                int i = list.FindIndex(u => u.MaTag == MaTag);
                if (i != -1)
                {
                    list.RemoveAt(i);
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
                
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ThemTag()
        {
            return View(new TagModel());
        }

        [HttpPost]
        public ActionResult ThemTag(TagModel model)
        {
            model.AddTag();
            return View(model);
        }

        public ActionResult ThemDanhMucBV()
        {
            return View(new DanhMucBaiVietModel());
        }

        [HttpPost]
        public ActionResult ThemDanhMucBV(DanhMucBaiVietModel model)
        {
            model.Add();
            return View(model);
        }

    }
}