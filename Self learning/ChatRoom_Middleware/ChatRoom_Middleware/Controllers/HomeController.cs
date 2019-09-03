using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChatRoom_Middleware.Models;
using Microsoft.AspNetCore.Http;

namespace ChatRoom_Middleware.Controllers
{
    public class HomeController : Controller
    {

        PayablesContext db = new PayablesContext();
        

        [HttpGet]
        public IActionResult Index()
        {
            string cookieValueFromReq = Request.Cookies["socketId_XT"];



            return View("InsertUserName");
        }

        [HttpPost]
        public IActionResult Index(string username)
        {
            return View("Index", username);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
