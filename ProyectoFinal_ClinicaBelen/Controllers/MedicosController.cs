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
    [Authorize(Roles = "Administrador")]
    public class MedicosController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: Medicos
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
            var medicos = db.Medicos.Include(m => m.CategoriaMedico);
            return View(medicos.ToList());
        }

        [HttpGet]
        public ActionResult GetData()
        {
            using (ClinicaContext db = new ClinicaContext())
            {
                try
                {
                    var listaMedicos = db.Medicos.OrderBy(a => a.Nombres)
                            .Select(m => new { m.Nombres, m.Apellidos,  m.Genero, m.Direccion, m.Email, m.Numero_documento, m.Id_Medico })
                            .ToList();
                    
                    return Json(new { data = listaMedicos }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {

                    throw;
                }
                
            }
        }
        

        // GET: Medicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medicos.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // GET: Medicos/Create
        public ActionResult Create()
        {
            ViewBag.Id_Categoria = new SelectList(db.CategoriaMedicoes, "Id_Categoria", "NombreCategoria");
            return View();
        }

        // POST: Medicos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Medico,Nombres,Apellidos,Genero,FechaNacimiento,Email,Direccion,Movil,Fecha_Creacion,Numero_documento,IsActive,Id_Categoria")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Medicos.Add(medico);
                db.SaveChanges();
                TempData["Accion"] = "Insertado";
                return RedirectToAction("Index");
            }

            ViewBag.Id_Categoria = new SelectList(db.CategoriaMedicoes, "Id_Categoria", "NombreCategoria", medico.Id_Categoria);
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medicos.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Categoria = new SelectList(db.CategoriaMedicoes, "Id_Categoria", "NombreCategoria", medico.Id_Categoria);
            ViewBag.FechaNacimiento = string.Format("{0:dd/MM/yyyy}", medico.FechaNacimiento);
            ViewBag.Fecha_Creacion = string.Format("{0:dd/MM/yyyy}", medico.Fecha_Creacion);
            ViewBag.Movil = medico.Movil;
            ViewBag.Dui = medico.Numero_documento;
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Medico,Nombres,Apellidos,Genero,FechaNacimiento,Email,Direccion,Movil,Fecha_Creacion,Numero_documento,IsActive,Id_Categoria")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medico).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Accion"] = "Editado";
                return RedirectToAction("Index");
            }
            ViewBag.Id_Categoria = new SelectList(db.CategoriaMedicoes, "Id_Categoria", "NombreCategoria", medico.Id_Categoria);
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medicos.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medico medico = db.Medicos.Find(id);
            db.Medicos.Remove(medico);
            db.SaveChanges();
            TempData["Accion"] = "Eliminado";
            return RedirectToAction("Index");
        }
        public ActionResult EliminarMedico(int id)
        {
            Medico pac = db.Medicos.Find(id);
            db.Medicos.Remove(pac);
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
