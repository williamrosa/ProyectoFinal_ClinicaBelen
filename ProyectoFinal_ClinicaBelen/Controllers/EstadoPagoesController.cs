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
    public class EstadoPagoesController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: EstadoPagoes
        public ActionResult Index()
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
            return View(db.EstadoPagos.ToList());
        }

        // GET: EstadoPagoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoPago estadoPago = db.EstadoPagos.Find(id);
            if (estadoPago == null)
            {
                return HttpNotFound();
            }
            return View(estadoPago);
        }

        // GET: EstadoPagoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoPagoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_TipoDePago,EstadoDePago")] EstadoPago estadoPago)
        {
            if (ModelState.IsValid)
            {
                db.EstadoPagos.Add(estadoPago);
                TempData["Accion"] = "Insertado";
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadoPago);
        }

        // GET: EstadoPagoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoPago estadoPago = db.EstadoPagos.Find(id);
            if (estadoPago == null)
            {
                return HttpNotFound();
            }
            return View(estadoPago);
        }

        // POST: EstadoPagoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_TipoDePago,EstadoDePago")] EstadoPago estadoPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoPago).State = EntityState.Modified;
                TempData["Accion"] = "Editado";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadoPago);
        }

        // GET: EstadoPagoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoPago estadoPago = db.EstadoPagos.Find(id);
            if (estadoPago == null)
            {
                return HttpNotFound();
            }
            return View(estadoPago);
        }

        // POST: EstadoPagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoPago estadoPago = db.EstadoPagos.Find(id);
            db.EstadoPagos.Remove(estadoPago);
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

        [HttpGet]
        public ActionResult GetData()
        {
            using (ClinicaContext db = new ClinicaContext())
            {
                try
                {
                    var listaEstadoPagos = db.EstadoPagos.OrderBy(a => a.EstadoDePago)
                            .Select(m => new { m.Id_TipoDePago,m.EstadoDePago })
                            .ToList();

                    return Json(new { data = listaEstadoPagos }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    throw;
                }

            }
        }

        [HttpPost]
        public ActionResult EliminarEstadoPago(int id)
        {
            EstadoPago estadoPagoe = db.EstadoPagos.Find(id);
            db.EstadoPagos.Remove(estadoPagoe);
            db.SaveChanges();
            TempData["Accion"] = "Eliminado";
            return RedirectToAction("Index");
        }
    }
}
