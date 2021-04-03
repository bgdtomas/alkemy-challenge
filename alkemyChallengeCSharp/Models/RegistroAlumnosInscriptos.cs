using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace alkemyChallengeCSharp.Models
{
    public class RegistroAlumnosInscriptos
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Alumno))]
        public Guid AlumnoId { get; set; }
        public Alumno Alumno { get; set; }

        [ForeignKey(nameof(Materia))]
        public Guid MateriaId { get; set; }
        public Materia Materia { get; set; }
    }
}
