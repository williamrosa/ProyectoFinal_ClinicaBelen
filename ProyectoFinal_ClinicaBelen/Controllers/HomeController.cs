using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProyectoFinal_ClinicaBelen.Models;
using ProyectoFinal_ClinicaBelen.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal_ClinicaBelen.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            //var UserName = TempData["UserName"];
            //UserID = Convert.ToString(UserName);
            ////Si el userID viene vacio le mandamos un badRequest
            //if (string.IsNullOrEmpty(UserID ))
            //{
            //    ViewBag.IsUser = false;
            //    return View();
            //}
            //ViewBag.IsUser = true;
            return View();
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}