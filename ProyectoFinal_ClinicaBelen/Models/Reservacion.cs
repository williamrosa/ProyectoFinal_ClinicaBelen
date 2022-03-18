using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal_ClinicaBelen.Models
{
    public class Reservacion
    {
        [Key]
        public int Id_Reservacion { get; set; }

        [Required(ErrorMessage = "Debes llenar el campo {0}")]
        [Display(Name = "Titulo")]
        public string Titulo_reservacion { get; set; }

        [Required(ErrorMessage = "Debes llenar el campo {0}")]
        [Display(Name = "Nota")]
        [DataType(DataType.MultilineText)]
        public string Nota_reservacion { get; set; }

        [Required(ErrorMessage = "Debes llenar el campo {0}")]
        [StringLength(80, ErrorMessage = "El campo {0} debe constar entre {2} y {1} caracteres", MinimumLength = 5)]
        public string Direccion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Fecha de creacion")]
        public DateTime Fecha_creado { get; set; }
        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Fecha de la cita")]
        public DateTime Fecha_cita { get; set; }

        [Required(ErrorMessage = "El campo {0}, no puede estar vacio")]
        [Display(Name = "Hora de la cita")]
        [DataType(DataType.Time)]
        public DateTime Hora_cita { get; set; }

        [Required(ErrorMessage = "Debes llenar el campo {0}")]
        public string Sintomas { get; set; }

        [Required(ErrorMessage = "Debes llenar el campo {0}")]
        [StringLength(80, ErrorMessage = "El campo {0} debe constar entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Medicamentos { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio")]
        public string Precio { get; set; }

        //Relaciones.
        //Relacion a la tabla Medico
        public int Id_Medico { get; set; }

        public virtual Medico Medico { get; set; }

        //Relacion a la tabla paciente
        public int Id_Paciente { get; set; }

        public virtual Paciente Paciente { get; set; }

        //Relaciones
        /// Relacion a la tabla EstadoReservacion
        public int Id_EstadoReservacion { get; set; }

        public virtual EstadoReservacion EstadoReservacion { get; set; }

        //Relacion a la tabla EstadoPago
        public int Id_TipoDePago { get; set; }

        public virtual EstadoPago EstadoPago { get; set; }
    }
}