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
    public class UsersController : Controller
    {
        //Siempre hay que agregar un modificador pricate/public
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Users
        public ActionResult Index()
        {
            if (TempData["Accion"] != null)
            {
                var accion = Convert.ToString(TempData["Accion"]);
                if (accion == "Insertado")
                {
                    ViewBag.Accion = "Insertado";
                }
                else if (accion == "Editado")
                {
                    ViewBag.Accion = "Editado";
                }
                else if (accion == "Eliminado")
                {
                    ViewBag.Accion = "Eliminado";
                }
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();

            //valores que agregaremos para trabajar en la vista con nuestro modelo, tomando los usuarios del identity
            var usersView = new List<UserView>();
            foreach (var user in users)
            {
                var userView = new UserView
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    UserID = user.Id
                };
                usersView.Add(userView);
            }
            //Enviamos la lista de los usuarios a la vista
            return View(usersView);
        }

        [HttpGet]
        public ActionResult GetData()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                try
                {
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                    var users = userManager.Users.ToList();

                    //valores que agregaremos para trabajar en la vista con nuestro modelo, tomando los usuarios del identity
                    var usersView = new List<UserView>();
                    foreach (var user in users)
                    {
                        var userView = new UserView
                        {
                            Email = user.Email,
                            UserName = user.UserName,
                            UserID = user.Id
                        };
                        usersView.Add(userView);
                    }
                    var listaUsers = usersView.OrderBy(u => u.UserName).Select(u => new { u.UserName, u.Email, u.UserID }).ToList();

                    return Json(new { data = listaUsers }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {

                    throw;
                }

            }
        }

        [HttpGet]
        public ActionResult Roles(string UserID)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles.ToList();

            //Buscamos el id que estamos recibiendo dentro de la lista de usuarios dentro del identity
            var user = users.Find(u => u.Id == UserID);

            var rolesView = new List<RoleView>();

            //verficamos que el rol no vaya nulo
            if (user.Roles != null)
            {
                foreach (var item in user.Roles)
                {
                    var role = roles.Find(r => r.Id == item.RoleId);
                    var roleView = new RoleView
                    {
                        RoleID = role.Id,
                        Name = role.Name
                    };
                    rolesView.Add(roleView);
                }
            }
            
            var userView = new UserView
            {
                Email = user.Email,
                UserName = user.UserName,
                UserID = user.Id,
                Roles = rolesView
            };

           
            return View(userView);
        }

        
        public ActionResult InndexRoles(string UserID)
        {
            var userId = Request["UserID"];
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles.ToList();

            //Buscamos el id que estamos recibiendo dentro de la lista de usuarios dentro del identity
            var user = users.Find(u => u.Id == UserID);

            var rolesView = new List<RoleView>();

            //verficamos que el rol no vaya nulo
            if (user.Roles != null)
            {
                foreach (var item in user.Roles)
                {
                    var role = roles.Find(r => r.Id == item.RoleId);
                    var roleView = new RoleView
                    {
                        RoleID = role.Id,
                        Name = role.Name
                    };
                    rolesView.Add(roleView);
                }
            }

            var userView = new UserView
            {
                Email = user.Email,
                UserName = user.UserName,
                UserID = user.Id,
                Roles = rolesView
            };


            return View(userView);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}