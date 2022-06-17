using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoFinal_ClinicaBelen.Models
{
    public class Paciente
    {
        [Key]
        public int Id_Paciente { get; set; }

        [Required(ErrorMessage = "El campo {0}, no puede estar vacio")]
        [StringLength(30, ErrorMessage = "El campo {0} necesita entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0}, no puede estar vacio")]
        [StringLength(30, ErrorMessage = "El campo {0} necesita entre {2} y {1} caracteres", MinimumLength = 4)]
        public string Apellidos { get; set; }

        public string Nombre_Completo { get { return string.Format("{0} {1}", Nombres, Apellidos); } }

        [Display(Name = "Género")]
        [StringLength(1, ErrorMessage = "El campo {0} necesita un solo caracter(M/F)", MinimumLength = 1)]
        public string Genero { get; set; }

        [Required(ErrorMessage = "El campo {0}, no puede estar vacio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El campo {0}, no puede estar vacio")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debes llenar el campo {0}")]
        [StringLength(80, ErrorMessage = "El campo {0} debe constar entre {2} y {1} caracteres", MinimumLength = 5)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0}, no puede estar vacio")]
        [Display(Name = "Numero documento")]
        public string Numero_documento { get; set; }

        [Required(ErrorMessage = "Debes llenar el campo {0}")]
        [StringLength(9, ErrorMessage = "El campo {0} debe constar de {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Telefono")]
        public string Movil { get; set; }

        [NotMapped]
        public int Edad { get { return DateTime.Now.Year - FechaNacimiento.Year; } }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        public DateTime Fecha_Creacion { get; set; }

        public ICollection<Reservacion> Reservaciones { get; set; }

        public ICollection<Recetas> Recetas { get; set; }
    }
}