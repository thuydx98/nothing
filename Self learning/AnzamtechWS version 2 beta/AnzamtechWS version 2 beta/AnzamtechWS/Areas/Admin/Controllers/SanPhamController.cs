using AnzamtechWS.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnzamtechWS.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        ANZEntities context = new ANZEntities();


        public ActionResult Index()
        {
            return View(new SanPhamModel());
        }

        [HttpPost]
        public ActionResult Index(SanPhamModel model)
        {
            ViewData["Result"] = true;
            return View(model);
        }


        public ActionResult ChinhSuaSP(int MaSP)
        {
            SanPhamModel model = new SanPhamModel() { MaSP = MaSP };
            model.GetSanPhamByID();
           
            return View(model);
        }

        [HttpPost]
        public ActionResult ChinhSuaSP(SanPhamModel sp)
        {
            sp.Update();
            return RedirectToAction("Index");
        }
        public ActionResult ThemSanPham()
        {
            return View(new SanPhamModel());
        }
        public ActionResult ThemSanPham1()
        {
            if (TempData["SPModel"] == null)
            {
                return RedirectToAction("ThemSanPham");
            }
            return View((SanPhamModel)TempData["SPModel"]);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemSanPham1(SanPhamModel model1)
        {

            model1.UpdateChiTiet();
            return RedirectToAction("ThemSanPham", "SanPham");
        }

        [HttpPost]
        public ActionResult ThemSanPham(SanPhamModel model)
        {

            using (var trans = context.Database.BeginTransaction())
            {
                try
                {
                    string tendm = "";
                    if (model.TenSP.Length <= 10)
                    {
                        tendm = model.TenSP;
                    }
                    else
                    {
                        tendm = model.TenSP.Substring(0, 10);
                    }
                    DanhMucAnhModel dmaModel = new DanhMucAnhModel(context)
                    {
                        TenDM = Helper.Helper.ConvertVN(tendm).Replace(' ', '-') 

                    };
                    dmaModel.Path = "/image/Product";
                    dmaModel.Add();
                    dmaModel.UpdateID();
                    model.MaDMA = dmaModel.Id;
                    model.ContextANZ = context;
                    model.Add();

                    trans.Commit();
                    TempData["SPModel"] = model;
                    return RedirectToAction("ThemSanPham1", "SanPham");
                }
                catch (Exception)
                {
                    // Console.WriteLine(ex.Message);
                    trans.Rollback();
                }
            }

            return View("ThemSanPham", new SanPhamModel());

        }
    }
}