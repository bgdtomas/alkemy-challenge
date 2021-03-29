using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace alkemyChallengeCSharp.Models
{
    public class Alumno : Usuario
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression(@"[0-9]{2}\.[0-9]{3}\.[0-9]{3}", ErrorMessage = "El campo debe ser del formato NN.NNN.NNN")]
        [MaxLength(20, ErrorMessage = "El campo {0} tiene un máximo de {1} caracteres")]
        [Display(Name = "DNI")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(20, ErrorMessage = "El campo {0} tiene un máximo de {1} caracteres")]
        [Display(Name = "Legajo")]
        public string Legajo { get; set; }
        public List<Materia> Materias { get; set; }

    }
}
