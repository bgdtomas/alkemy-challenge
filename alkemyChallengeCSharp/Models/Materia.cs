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
        [Display(Name = "Horario")]
        public TimeSpan Horario { get; set; }

        [ForeignKey(nameof(Profesor))]
        [Display(Name = "Profesor")]
        public Guid ProfesorId { get; set; }
        public Profesor Profesor  { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Range(1D, 32D, ErrorMessage = "El maximo de alumnos debe estar comprendido entre 1 y 32")]
        [Display(Name = "Maximo de Alumnos")]
        public int MaxAlumnos{ get; set; }
    }
}
