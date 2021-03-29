using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace alkemyChallengeCSharp.Models
{
    public class Materia
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(200, ErrorMessage = "La longitud máxima del campo es de 200 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression(@"[0-9]{2}\-[0-9]{2}", ErrorMessage = "El campo debe ser del formato NN-NN")]
        [Display(Name = "Horario")]
        public string Horario { get; set; }

        [ForeignKey(nameof(Profesor))]
        [Display(Name = "Profesor")]
        public Guid ProfesorId { get; set; }
        public Profesor Profesor  { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Range(1D, 32D, ErrorMessage = "El maximo de alumnos debe estar comprendido entre 1 y 32")]
        [Display(Name = "Stock")]
        public int MaxAlumnos{ get; set; }
    }
}
