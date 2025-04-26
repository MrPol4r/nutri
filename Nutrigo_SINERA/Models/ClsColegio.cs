using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nutrigo_SINERA.Models
{
    public class ClsColegio
    {
        // Información institucional
        public string Nombre { get; set; }
        public string CodigoModular { get; set; }
        public string TipoColegio { get; set; }
        public string NivelEducativo { get; set; }
        public string Direccion { get; set; }
        public string Ubicacion { get; set; } // Dpto/Prov/Distrito
        public string Telefono { get; set; }
        public string CorreoInstitucional { get; set; }
        public int CantidadAlumnos { get; set; }
        public bool Activo { get; set; }

        // Representante
        public string RepresentanteNombre { get; set; }
        public string RepresentanteCargo { get; set; }
        public string RepresentanteDNI { get; set; }
        public string RepresentanteCorreo { get; set; }
        public string RepresentanteCelular { get; set; }
        public string Contrasena { get; set; }
    }
}