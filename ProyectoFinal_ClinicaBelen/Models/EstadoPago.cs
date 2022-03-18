using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal_ClinicaBelen.Models
{
    public class EstadoPago
    {
        [Key]
        public int Id_TipoDePago { get; set; }

        [Required(ErrorMessage = "El campo {0}, no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El campo {0} necesita entre {2} y {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Estado del pago")]
        public string EstadoDePago { get; set; }

        public ICollection<Reservacion> Reservaciones { get; set; }
    }
}