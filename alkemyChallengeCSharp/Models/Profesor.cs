using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace alkemyChallengeCSharp.Models
{
    public class Profesor
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(200, ErrorMessage = "La longitud máxima del campo es de 200 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(100, ErrorMessage = "La longitud máxima del campo es de 100 caracteres")]
        [RegularExpression(@"[a-zA-Z áéíóú]*", ErrorMessage = "El campo admite sólo caracteres alfabéticos")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }
        
        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression(@"[0-9]{2}\.[0-9]{3}\.[0-9]{3}", ErrorMessage = "El campo debe ser del formato NN.NNN.NNN")]
        [MaxLength(20, ErrorMessage = "El campo {0} tiene un máximo de {1} caracteres")]
        [Display(Name = "DNI")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Range(typeof(bool), "true", "false")]
        [Display(Name = "Is Active")]
        public Boolean Activo { get; set; }
    }
}
