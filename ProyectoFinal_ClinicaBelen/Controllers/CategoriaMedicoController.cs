using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal_ClinicaBelen.Context;
using ProyectoFinal_ClinicaBelen.Models;

namespace ProyectoFinal_ClinicaBelen.Controllers
{
    public class CategoriaMedicoController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: CategoriaMedico
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
            return View(db.CategoriaMedicoes.ToList());
        }

        [HttpGet]
        public ActionResult GetData()
        {
            using (ClinicaContext db = new ClinicaContext())
            {
                try
                {
                    var listaCategoria = db.CategoriaMedicoes.OrderBy(categoria => categoria.NombreCategoria)
                            .Select(categoria => new { categoria.Id_Categoria, categoria.NombreCategoria })
                            .ToList();
                    return Json(new { data = listaCategoria }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        // GET: CategoriaMedico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaMedico categoriaMedico = db.CategoriaMedicoes.Find(id);
            if (categoriaMedico == null)
            {
                return HttpNotFound();
            }
            return View(categoriaMedico);
        }

        // GET: CategoriaMedico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaMedico/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Categoria,NombreCategoria")] CategoriaMedico categoriaMedico)
        {
            if (ModelState.IsValid)
            {
                db.CategoriaMedicoes.Add(categoriaMedico);
                db.SaveChanges();
                TempData["Accion"] = "Insertado";
                return RedirectToAction("Index");
            }

            return View(categoriaMedico);
        }

        // GET: CategoriaMedico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaMedico categoriaMedico = db.CategoriaMedicoes.Find(id);
            if (categoriaMedico == null)
            {
                return HttpNotFound();
            }
            return View(categoriaMedico);
        }

        // POST: CategoriaMedico/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Categoria,NombreCategoria")] CategoriaMedico categoriaMedico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoriaMedico).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Accion"] = "Editado";
                return RedirectToAction("Index");
            }
            return View(categoriaMedico);
        }

        // GET: CategoriaMedico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaMedico categoriaMedico = db.CategoriaMedicoes.Find(id);
            if (categoriaMedico == null)
            {
                return HttpNotFound();
            }
            return View(categoriaMedico);
        }

        // POST: CategoriaMedico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoriaMedico categoriaMedico = db.CategoriaMedicoes.Find(id);
            db.CategoriaMedicoes.Remove(categoriaMedico);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EliminarCategoria(int id)
        {
            CategoriaMedico categoriaMedico = db.CategoriaMedicoes.Find(id);
            db.CategoriaMedicoes.Remove(categoriaMedico);
            TempData["Accion"] = "Eliminado";
            db.SaveChanges();
            return RedirectToAction("Index");
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
