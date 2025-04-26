using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nutrigo_SINERA.Models
{
    public class ClsAlumno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Grado { get; set; }
        public string Seccion { get; set; }

        public string Correo { get; set; }
        public string Contrasena { get; set; }

        public string EstadoNutricional { get; set; }
        public DateTime FechaActualizacion { get; set; }

        public int ColegioId { get; set; }

        // Relaciones uno a uno
        public ClsSalud Salud { get; set; }
        public ClsHabitos Habitos { get; set; }
    }
}