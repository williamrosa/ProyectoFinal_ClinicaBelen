using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ProyectoFinal_ClinicaBelen.Models;

namespace ProyectoFinal_ClinicaBelen.Context
{
    public class ClinicaContext:DbContext 
    {
        public DbSet<Paciente> Pacientes { get; set; }

        public System.Data.Entity.DbSet<ProyectoFinal_ClinicaBelen.Models.Correo> Correos { get; set; }

        public System.Data.Entity.DbSet<ProyectoFinal_ClinicaBelen.Models.Medico> Medicos { get; set; }

        public System.Data.Entity.DbSet<ProyectoFinal_ClinicaBelen.Models.CategoriaMedico> CategoriaMedicoes { get; set; }

        public System.Data.Entity.DbSet<ProyectoFinal_ClinicaBelen.Models.EstadoPago> EstadoPagos { get; set; }

        public System.Data.Entity.DbSet<ProyectoFinal_ClinicaBelen.Models.EstadoReservacion> EstadoReservaciones { get; set; }

        public System.Data.Entity.DbSet<ProyectoFinal_ClinicaBelen.Models.Reservacion> Reservaciones { get; set; }

        public System.Data.Entity.DbSet<ProyectoFinal_ClinicaBelen.Models.Recetas> Recetas { get; set; }
    }
}