using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alkemyChallengeCSharp.Models
{
    public class Alumno : Usuario
    {
        public string Dni { get; set; }
        public string Legajo { get; set; }
        public List<Materia> Materias { get; set; }

    }
}
