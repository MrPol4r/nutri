using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nutrigo_SINERA.Models
{
    public class ClsSalud
    {
        public int AlumnoId { get; set; } // FK
        public int Edad { get; set; }
        public double Peso { get; set; }
        public string Talla { get; set; }
        public string Alergias { get; set; }
        public string Enfermedades { get; set; }
    }
}