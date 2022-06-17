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
    [Authorize(Roles =  "Administrador")]
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
        [ActionName("Roles")]
        public ActionResult Roles(string UserID)
        {
            //Si el userID viene vacio le mandamos un badRequest
            if (string.IsNullOrEmpty(UserID))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles.ToList();

            //Buscamos el id que estamos recibiendo dentro de la lista de usuarios dentro del identity
            var user = users.Find(u => u.Id == UserID);

            if (user == null)
            {
                return HttpNotFound();
            }
            var rolesView = new List<RoleView>();

            var usersView = new List<UserView>();
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

        [HttpGet]
       public ActionResult AddRole(string UserID)
       {
            //Si el userID viene vacio le mandamos un badRequest
            if (string.IsNullOrEmpty(UserID))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();

            //Buscamos el id que estamos recibiendo dentro de la lista de usuarios dentro del identity
            var user = users.Find(u => u.Id == UserID);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userView = new UserView
            {
                Email = user.Email,
                UserName = user.UserName,
                UserID = user.Id
            };

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var list = roleManager.Roles.ToList();
            list.OrderBy(r => r.Name).ToList();
            ViewBag.RoleID = new SelectList(list, "Id", "Name");


            return View(userView);
        }

        [HttpPost]
        public ActionResult AddRole(string UserID, FormCollection form)
        {
            var roleID = Request["RoleID"];

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();

            //Buscamos el id que estamos recibiendo dentro de la lista de usuarios dentro del identity
            var user = users.Find(u => u.Id == UserID);

           

            var userView = new UserView
            {
                Email = user.Email,
                UserName = user.UserName,
                UserID = user.Id
            };
            if (string.IsNullOrEmpty(roleID))
            {
                ViewBag.error = "No se selecciono ningun rol";

                var list = roleManager.Roles.ToList();
                list.OrderBy(r => r.Name).ToList();
                ViewBag.RoleID = new SelectList(list, "Id", "Name");

                return View(userView);
            }

            var role = roles.Find(r => r.Id == roleID);
            if (!userManager.IsInRole(UserID, role.Name))
            {
                userManager.AddToRole(UserID, role.Name);
            }
            else
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var rolesView = new List<RoleView>();

            var usersView = new List<UserView>();
            //verficamos que el rol no vaya nulo
            if (user.Roles != null)
            {
                foreach (var item in user.Roles)
                {
                    role = roles.Find(r => r.Id == item.RoleId);
                    var roleView = new RoleView
                    {
                        RoleID = role.Id,
                        Name = role.Name
                    };
                    rolesView.Add(roleView);
                }
            }

            userView = new UserView
            {
                Email = user.Email,
                UserName = user.UserName,
                UserID = user.Id,
                Roles = rolesView
            };
            return View("Roles", userView);
        }

        public ActionResult Delete(string UserID, string RoleID)
        {
            if (string.IsNullOrEmpty(UserID) || string.IsNullOrEmpty(RoleID))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();

            var user = users.Find(u => u.Id == UserID);
            var role = roles.Find(r => r.Id == RoleID);

            if (userManager.IsInRole(user.Id, role.Name))
            {
                userManager.RemoveFromRole(user.Id, role.Name);
            }

            var rolesView = new List<RoleView>();

            var usersView = new List<UserView>();
            //verficamos que el rol no vaya nulo
            if (user.Roles != null)
            {
                foreach (var item in user.Roles)
                {
                    role = roles.Find(r => r.Id == item.RoleId);
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
            return View("Roles", userView);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult DeleteUser(string UserID)
        {
            if (string.IsNullOrEmpty(UserID))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();

            var user = users.Find(u => u.Id == UserID);

            db.Users.Remove(user);
            TempData["Accion"] = "Eliminado";
            db.SaveChanges();

            return Redirect("Index");
        }
    }
}