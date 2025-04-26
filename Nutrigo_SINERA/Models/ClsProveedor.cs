using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nutrigo_SINERA.Models
{
    public class ClsProveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string RUC { get; set; }
        public string Especialidad { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string Contrasena { get; set; }
        public int ColegioId { get; set; } // Asociado al colegio
    }
}