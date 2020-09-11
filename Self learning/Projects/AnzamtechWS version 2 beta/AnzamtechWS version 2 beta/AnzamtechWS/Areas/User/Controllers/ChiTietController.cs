using AnzamtechWS.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnzamtechWS.Areas.User.Controllers
{
    public class ChiTietController : Controller
    {
        // GET: User/ChiTiet
        public ActionResult Index(int id)
        {
            SanPhamModel model = new SanPhamModel() { MaSP = id };

            return View(model.GetSanPhamByMaSP());
        }

        public ActionResult GiaiPhap(int id)
        {
            GiaiPhapModel model = new GiaiPhapModel()
            {
                ID = id
            };
            return View(model.GetGiaiPhaps());
        }
        public ActionResult PhanMem(int id)
        {
            GiaiPhapModel model = new GiaiPhapModel()
            {
                ID = id
            };
            return View(model.GetGiaiPhaps());
        }
    }
}