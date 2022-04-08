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
    public class ReservacionsController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: Reservacions
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
            var reservaciones = db.Reservaciones.Include(r => r.EstadoPago).Include(r => r.EstadoReservacion).Include(r => r.Medico).Include(r => r.Paciente);
            return View(reservaciones.ToList());
        }

        //Metodo para el datatable
        [HttpGet]
        public ActionResult GetData()
        {
            using (ClinicaContext db = new ClinicaContext())
            {
                try
                {
                    var listaReservaciones = db.Reservaciones.OrderBy(r => r.Titulo_reservacion)
                            .Select(r => new { r.Titulo_reservacion, Nombre_Completo = r.Medico.Nombres + " " + r.Medico.Apellidos, Nombre_Completo_Paciente =  r.Paciente.Nombres + " " + r.Paciente.Apellidos, r.Medicamentos, r.Precio, r.Fecha_cita, r.Fecha_creado, r.Id_Reservacion })
                            .ToList();

                    return Json(new { data = listaReservaciones }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {

                    throw;
                }

            }
        }
        // GET: Reservacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservaciones.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // GET: Reservacions/Create
        public ActionResult Create()
        {
            ViewBag.Id_TipoDePago = new SelectList(db.EstadoPagos, "Id_TipoDePago", "EstadoDePago");
            ViewBag.Id_EstadoReservacion = new SelectList(db.EstadoReservaciones, "Id_EstadoReservacion", "EstadoDeCita");
            ViewBag.Id_Medico = new SelectList(db.Medicos, "Id_Medico", "Nombres");
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id_Paciente", "Nombres");
            return View();
        }

        // POST: Reservacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Reservacion,Titulo_reservacion,Nota_reservacion,Direccion,Fecha_creado,Fecha_cita,Sintomas,Medicamentos,Precio,Id_Medico,Id_Paciente,Id_EstadoReservacion,Id_TipoDePago")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Reservaciones.Add(reservacion);
                db.SaveChanges();
                TempData["Accion"] = "Insertado";
                return RedirectToAction("Index");
            }

            ViewBag.Id_TipoDePago = new SelectList(db.EstadoPagos, "Id_TipoDePago", "EstadoDePago", reservacion.Id_TipoDePago);
            ViewBag.Id_EstadoReservacion = new SelectList(db.EstadoReservaciones, "Id_EstadoReservacion", "EstadoDeCita", reservacion.Id_EstadoReservacion);
            ViewBag.Id_Medico = new SelectList(db.Medicos, "Id_Medico", "Nombres", reservacion.Id_Medico);
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id_Paciente", "Nombres", reservacion.Id_Paciente);
            
            return View(reservacion);
        }

        // GET: Reservacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservaciones.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_TipoDePago = new SelectList(db.EstadoPagos, "Id_TipoDePago", "EstadoDePago", reservacion.Id_TipoDePago);
            ViewBag.Id_EstadoReservacion = new SelectList(db.EstadoReservaciones, "Id_EstadoReservacion", "EstadoDeCita", reservacion.Id_EstadoReservacion);
            ViewBag.Id_Medico = new SelectList(db.Medicos, "Id_Medico", "Nombres", reservacion.Id_Medico);
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id_Paciente", "Nombres", reservacion.Id_Paciente);
            ViewBag.FechaCita =reservacion.Fecha_cita;
            ViewBag.FechaCreado = string.Format("{0:dd/MM/yyyy}", reservacion.Fecha_creado);
            return View(reservacion);
        }

        // POST: Reservacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Reservacion,Titulo_reservacion,Nota_reservacion,Direccion,Fecha_creado,Fecha_cita,Sintomas,Medicamentos,Precio,Id_Medico,Id_Paciente,Id_EstadoReservacion,Id_TipoDePago")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservacion).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Accion"] = "Editado";
                return RedirectToAction("Index");
            }
            ViewBag.Id_TipoDePago = new SelectList(db.EstadoPagos, "Id_TipoDePago", "EstadoDePago", reservacion.Id_TipoDePago);
            ViewBag.Id_EstadoReservacion = new SelectList(db.EstadoReservaciones, "Id_EstadoReservacion", "EstadoDeCita", reservacion.Id_EstadoReservacion);
            ViewBag.Id_Medico = new SelectList(db.Medicos, "Id_Medico", "Nombres", reservacion.Id_Medico);
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id_Paciente", "Nombres", reservacion.Id_Paciente);
            
            return View(reservacion);
        }

        public ActionResult EliminarReservacion(int id)
        {
            Reservacion reserv = db.Reservaciones.Find(id);
            db.Reservaciones.Remove(reserv);
            TempData["Accion"] = "Eliminado";
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Reservacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservaciones.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // POST: Reservacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservacion reservacion = db.Reservaciones.Find(id);
            db.Reservaciones.Remove(reservacion);
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
