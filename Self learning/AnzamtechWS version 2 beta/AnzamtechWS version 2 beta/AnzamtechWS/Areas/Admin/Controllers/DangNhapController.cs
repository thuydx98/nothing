using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnzamtechWS.Areas.Admin.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: Admin/DangNhap
        public ActionResult Index()
        {
            if (HttpContext.Session["LoginAdmin"] != null)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }

        [HttpPost]
        public ActionResult Login(Admin.Models.DangNhapModel l)
        {
            if (ModelState.IsValid== true)
            {
                if (l.DangNhapHopLe() == true)
                {
                    HttpContext.Session["LoginAdmin"] = l;
                    if (HttpContext.Session["URLLoginAdmin"] != null)
                        return Redirect(Session["URLLoginAdmin"].ToString());
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Error", "Tên đăng nhập hoặc mật khẩu không chính xác!");
                    return View("Index", l);
                }
            }

            ModelState.AddModelError("Error", "Có lỗi gì đó xảy ra!");
            return View("Index", l);
        }

        public ActionResult Login()
        {
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            HttpContext.Session["LoginAdmin"] = null;
            HttpContext.Session["URLLoginAdmin"] = null;
            return RedirectToAction("Index");
        }

    }
}