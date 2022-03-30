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
    public class EstadoReservacionesController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: EstadoReservaciones
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
            return View(db.EstadoReservaciones.ToList());
        }

        [HttpGet]
        public ActionResult GetData()
        {
            using (ClinicaContext db = new ClinicaContext())
            {
                try
                {
                    var listaEstadoReservaciones = db.EstadoReservaciones.OrderBy(categoria => categoria.EstadoDeCita)
                            .Select(categoria => new { categoria.Id_EstadoReservacion, categoria.EstadoDeCita })
                            .ToList();
                    return Json(new { data = listaEstadoReservaciones }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        // GET: EstadoReservaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoReservacion estadoReservacion = db.EstadoReservaciones.Find(id);
            if (estadoReservacion == null)
            {
                return HttpNotFound();
            }
            return View(estadoReservacion);
        }

        // GET: EstadoReservaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoReservaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_EstadoReservacion,EstadoDeCita")] EstadoReservacion estadoReservacion)
        {
            if (ModelState.IsValid)
            {
                db.EstadoReservaciones.Add(estadoReservacion);
                db.SaveChanges();
                TempData["Accion"] = "Insertado";
                return RedirectToAction("Index");
            }

            return View(estadoReservacion);
        }

        // GET: EstadoReservaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoReservacion estadoReservacion = db.EstadoReservaciones.Find(id);
            if (estadoReservacion == null)
            {
                return HttpNotFound();
            }
            return View(estadoReservacion);
        }

        // POST: EstadoReservaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_EstadoReservacion,EstadoDeCita")] EstadoReservacion estadoReservacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoReservacion).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Accion"] = "Editado";
                return RedirectToAction("Index");
            }
            return View(estadoReservacion);
        }

        // GET: EstadoReservaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoReservacion estadoReservacion = db.EstadoReservaciones.Find(id);
            if (estadoReservacion == null)
            {
                return HttpNotFound();
            }
            return View(estadoReservacion);
        }

        // POST: EstadoReservaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoReservacion estadoReservacion = db.EstadoReservaciones.Find(id);
            db.EstadoReservaciones.Remove(estadoReservacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult EliminarReservacion(int id)
        {
            EstadoReservacion estadoReservacion = db.EstadoReservaciones.Find(id);
            db.EstadoReservaciones.Remove(estadoReservacion);
            db.SaveChanges();
            TempData["Accion"] = "Eliminado";
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
