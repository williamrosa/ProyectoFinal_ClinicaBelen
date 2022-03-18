using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal_ClinicaBelen.Models
{
    public class EstadoReservacion
    {
        [Key]
        public int Id_EstadoReservacion { get; set; }

        [Required(ErrorMessage = "El campo {0}, no puede estar vacio")]
        [StringLength(20, ErrorMessage = "El campo {0} necesita entre {2} y {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Estado de la cita")]
        public string EstadoDeCita { get; set; }

        public ICollection<Reservacion> Reservaciones { get; set; }
    }
}