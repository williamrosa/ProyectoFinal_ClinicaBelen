using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal_ClinicaBelen.Models
{
	public class Recetas
	{
        [Key]
        public int IdRecetas { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string PrescripcionMedica { get; set; }

        public string MedicamentosRecetados { get; set; }

        public string Dosis { get; set; }

        public int Id_Paciente { get; set; }
        public virtual Paciente oPaciente { get; set; }

        public int Id_Medico { get; set; }
        public virtual Medico oMedico { get; set; }
    }
}