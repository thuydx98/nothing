using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;


namespace MekongSite.Controllers
{
    public class AdminController : Controller
    {
        MekongCanThoEntities db = new MekongCanThoEntities();

        #region Xem bài đăng
        /// <summary>
        /// Trang xem bai viet
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? page)
        {
            int checkRole = CheckRole();

            if (checkRole == -1)
            {
                return RedirectToAction("Index", "Login");
            }

            if (TempData["403"] != null)
            {
                ViewBag.Error403 = TempData["403"];
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(db.Posts.OrderByDescending(n => n.Post_ID).ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Load phân trang mới
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult TablePostsDetailsPartial(int? page)
        {
            int checkRole = CheckRole();

            if (checkRole == -1)
            {
                return RedirectToAction("Index", "Login");
            }

            if (TempData["403"] != null)
            {
                ViewBag.Error403 = TempData["403"];
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (page <= 0)
                pageNumber = 1;
            return PartialView("TablePostsDetailsPartial", db.Posts.OrderByDescending(n => n.Post_ID).ToPagedList(pageNumber, pageSize));
        }
        #endregion

        #region Xóa bài đăng
        /// <summary>
        /// Trang xoa bai viet cua Admin
        /// </summary>
        /// <returns></returns>
        public ActionResult XoaBaiViet(int? page)
        {
            int checkRole = CheckRole();

            if (checkRole == -1)
            {
                return RedirectToAction("Index", "Login");
            }

            if (checkRole != 1)
            {
                TempData["403"] = "403";
                return RedirectToAction("Index");
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(db.Posts.OrderByDescending(n => n.Post_ID).ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Hàm xóa bài đăng
        /// </summary>
        /// <param name="post_id"></param>
        /// <returns></returns>
        public String DeletePost(int post_id)
        {
            int checkRole = CheckRole();

            if (checkRole == -1)
            {
                return "0";
            }

            if (checkRole != 1)
            {
                return "0";
            }

            Post post = db.Posts.Find(post_id);

            if(post == null)
            {
                Response.StatusCode = 404;
                return "0";
            }
            else
            {
                db.Posts.Remove(post);
                db.SaveChanges();
            }
            return "1";
        }

        /// <summary>
        /// Load phân trang mới
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult TablePostsDeletePartial(int? page)
        {
            int checkRole = CheckRole();

            if (checkRole == -1)
            {
                return RedirectToAction("Index", "Login");
            }

            if (checkRole != 1)
            {
                TempData["403"] = "403";
                return RedirectToAction("Index");
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (page <= 0)
                pageNumber = 1;
            return PartialView("TablePostsDeletePartial", db.Posts.OrderByDescending(n => n.Post_ID).ToPagedList(pageNumber, pageSize));
        }

        #endregion


        /// <summary>
        /// Check quyền và trạng thái đăng nhập khi truy cập trang
        /// </summary>
        /// <returns></returns>
        private int CheckRole()
        {
            User user = Session["Account_Mekong"] as User;

            if (user != null && user.GroupID == "ADMIN")
            {
                return 1;
            }

            if (user != null && user.GroupID == "MOD")
            {
                return 0;
            }

            return -1;
        }
    }
}