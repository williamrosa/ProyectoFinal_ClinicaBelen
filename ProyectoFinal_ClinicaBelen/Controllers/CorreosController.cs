using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal_ClinicaBelen.Context;
using ProyectoFinal_ClinicaBelen.Models;

namespace ProyectoFinal_ClinicaBelen.Controllers
{
    public class CorreosController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: Correos/Create
        public ActionResult EnviarCorreo()
        {

            return View();
        }

        // POST: Correos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnviarCorreo([Bind(Include = "idCorreo,Nombres,Apellidos,Email,Numero_documento,Movil")] Correo correo)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //HttpPostedFile fichero;
                    //Cuerpo del mensaje.
                    string body = "<body>" +
                        "<h1>Clinica Belen</h1>" +
                        "<h4>Información Básica del paciente:</h4>" +
                        "<span>" + "Nombre : " + correo.Nombres + "<br> Apellido : " + correo.Apellidos + "<br> DUI: " + correo.Numero_documento + "<br> Movil: " + correo.Movil
                        + "</span>" +
                        "</body>";
                    //Se adjunta en el mensaje los datos dentro de nuestro campo.
                    //string mensaje = "Nombre : " + correo.Nombres + "<br> Apellido : " + correo.Apellidos + "<br> DUI: " + correo.Numero_documento + "<br> Movil: " + correo.Movil;
                    MailMessage email = new MailMessage();
                    //correo de la clinica
                    email.From = new MailAddress("r3yes.mauricio@gmail.com");
                    //correo del secretario
                    email.To.Add("maury99900@gmail.com");
                    //Asunto del correo
                    email.Subject = "Datos del paciente";
                    email.Body = body;
                    email.IsBodyHtml = true;
                    email.Priority = MailPriority.Normal;

                    //Para futuro si necesitamos enviar un archivo..
                    //Se almacena el archivo a enviar en una carpeta en especifico que se encuentra dentro del proyecto
                    //string ruta = Server.MapPath(".../Temp");
                    //En este caso recibiriamos el fichero como parametro dentro del metodo
                    //fichero.SaveAs(ruta + "\\" + fichero.FileName);

                    //Attachment adjunto = new Attachment(ruta + "\\" + fichero.FileName);
                    //email.Attachments.Add(adjunto);

                    //Configuracion del servidor smtp
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 25;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    string cuentaCorreo = "r3yes.mauricio@gmail.com";
                    string passwordCorreo = "Patricia9926.";
                    smtp.Credentials = new System.Net.NetworkCredential(cuentaCorreo, passwordCorreo);

                    smtp.Send(email);
                    ViewBag.Mensaje = "Enviado";
                }
                catch (ArgumentException e)
                {
                    ViewBag.Mensaje = "Mensaje no enviado";
                }
                //db.Correos.Add(correo);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return View(correo);
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
