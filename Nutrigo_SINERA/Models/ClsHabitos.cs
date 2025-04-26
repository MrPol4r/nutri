using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nutrigo_SINERA.Models
{
    public class ClsHabitos
    {
        public int AlumnoId { get; set; } // FK
        public int ComidasPorDia { get; set; }
        public string TipoAlimentos { get; set; }
        public string DietaEspecial { get; set; }
        public string FrecuenciaFastFood { get; set; }
        public string BebidasAzucaradas { get; set; }
        public string FrutasVerduras { get; set; }
        public string UltimaComidaHora { get; set; }
        public string ImportanciaAlimentacion { get; set; }
    }
}