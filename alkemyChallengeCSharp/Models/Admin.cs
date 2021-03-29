using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace alkemyChallengeCSharp.Models
{
    public class Admin : Usuario
    {
        [ScaffoldColumn(false)]
        public Guid Legajo { get; set; }
    }
}
