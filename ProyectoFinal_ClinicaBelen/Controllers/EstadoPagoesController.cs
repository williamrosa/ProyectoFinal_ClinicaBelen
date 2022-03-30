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
    }
}
