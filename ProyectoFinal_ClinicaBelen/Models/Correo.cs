using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal_ClinicaBelen.Models
{
    public class Correo
    {
        [Key]
        public int idCorreo { get; set; }

        [Required(ErrorMessage = "El campo {0}, no puede estar vacio")]
        [StringLength(30, ErrorMessage = "El campo {0} necesita entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0}, no puede estar vacio")]
        [StringLength(30, ErrorMessage = "El campo {0} necesita entre {2} y {1} caracteres", MinimumLength = 4)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo {0}, no puede estar vacio")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0}, no puede estar vacio")]
        [Display(Name = "Numero documento")]
        public string Numero_documento { get; set; }

        [Required(ErrorMessage = "Debes llenar el campo {0}")]
        [StringLength(9, ErrorMessage = "El campo {0} debe constar de {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Telefono")]
        public string Movil { get; set; }
    }
}