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
    [Authorize(Roles = "Administrador, Index")]
    public class RecetasController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: Recetas
        public ActionResult Index()
        {
            var recetas = db.Recetas.Include(r => r.oMedico).Include(r => r.oPaciente);
            return View(recetas.ToList());
        }

        // GET: Recetas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recetas recetas = db.Recetas.Find(id);
            if (recetas == null)
            {
                return HttpNotFound();
            }
            return View(recetas);
        }

        public ActionResult Print(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recetas recetas = db.Recetas.Find(id);
            if (recetas == null)
            {
                return HttpNotFound();
            }
            return new Rotativa.ViewAsPdf(recetas);
        }
        // GET: Recetas/Create
        public ActionResult Create()
        {
            ViewBag.Id_Medico = new SelectList(db.Medicos, "Id_Medico", "Nombres");
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id_Paciente", "Nombres");
            return View();
        }

        // POST: Recetas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRecetas,Nombre,PrescripcionMedica,MedicamentosRecetados,Dosis,Id_Paciente,Id_Medico")] Recetas recetas)
        {
            if (ModelState.IsValid)
            {
                db.Recetas.Add(recetas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Medico = new SelectList(db.Medicos, "Id_Medico", "Nombres", recetas.Id_Medico);
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id_Paciente", "Nombres", recetas.Id_Paciente);
            return View(recetas);
        }

        [HttpGet]
        public ActionResult GetData()
        {
            using (ClinicaContext db = new ClinicaContext())
            {
                try
                {
                    var listaRecetas = db.Recetas.OrderBy(a => a.Nombre)
                            .Select(m => new { m.oMedico.Nombres, m.oPaciente.Apellidos, m.Nombre, m.PrescripcionMedica, m.MedicamentosRecetados, m.Dosis, m.IdRecetas})
                            .ToList();

                    return Json(new { data = listaRecetas }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {

                    throw;
                }

            }
        }

        public ActionResult EliminarReceta(int id)
        {
            Recetas dato = db.Recetas.Find(id);
            db.Recetas.Remove(dato);
            TempData["Accion"] = "Eliminado";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Recetas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recetas recetas = db.Recetas.Find(id);
            if (recetas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Medico = new SelectList(db.Medicos, "Id_Medico", "Nombres", recetas.Id_Medico);
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id_Paciente", "Nombres", recetas.Id_Paciente);
            return View(recetas);
        }

        // POST: Recetas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRecetas,Nombre,PrescripcionMedica,MedicamentosRecetados,Dosis,Id_Paciente,Id_Medico")] Recetas recetas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recetas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Medico = new SelectList(db.Medicos, "Id_Medico", "Nombres", recetas.Id_Medico);
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id_Paciente", "Nombres", recetas.Id_Paciente);
            return View(recetas);
        }

        // GET: Recetas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recetas recetas = db.Recetas.Find(id);
            if (recetas == null)
            {
                return HttpNotFound();
            }
            return View(recetas);
        }

        // POST: Recetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recetas recetas = db.Recetas.Find(id);
            db.Recetas.Remove(recetas);
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
