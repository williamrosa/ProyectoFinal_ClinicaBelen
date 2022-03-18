using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal_ClinicaBelen.Models
{
    public class CategoriaMedico
    {
        [Key]
        public int Id_Categoria { get; set; }

        [Required(ErrorMessage = "El campo {0}, no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El campo {0} necesita entre {2} y {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Nombre de la categoria")]
        public string NombreCategoria { get; set; }

        public ICollection<Medico> Medicos { get; set; }
    }
}