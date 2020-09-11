using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MekongSite.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            using (var db = new MekongCanThoEntities())
            {
                string pass = CreateMD5(password);

                User user = db.Users.FirstOrDefault(x => x.UserName == username && x.Password == pass);
                if (user != null)
                {
                    Session["Account_Mekong"] = user;

                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.result = "Tài khoản hoặc mật khẩu sai.";
                    return View("Index");
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

    }
}