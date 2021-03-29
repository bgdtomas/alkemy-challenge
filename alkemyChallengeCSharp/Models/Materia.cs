using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alkemyChallengeCSharp.Models
{
    public class Materia
    {
        public string Nombre { get; set; }
        public string Horario { get; set; }
        public Profesor Profesor  { get; set; }
        public int MaxAlumnos{ get; set; }
    }
}
