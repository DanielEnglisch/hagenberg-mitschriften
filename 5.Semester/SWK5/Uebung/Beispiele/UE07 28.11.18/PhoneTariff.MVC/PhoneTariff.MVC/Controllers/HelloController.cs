using PhoneTariff.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneTariff.MVC.Controllers
{
    public class HelloController : Controller
    {
        // GET: Hello
        public ActionResult Index()
        {
            ViewData["Message"] = "Hello World";

            // ViewData["Date"] = DateTime.Now;
            ViewBag.Date = DateTime.Now;

            return View();
        }

        public ActionResult TypedIndex()
        {
            var model = new HelloModel()
            {
                Message = "HElllooo World",
                Date = DateTime.Now
            };

            //return View("viewName", model);
            return View(model);
        }
    }
}