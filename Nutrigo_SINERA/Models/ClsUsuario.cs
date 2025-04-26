using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nutrigo_SINERA.Models
{
    public class ClsUsuario
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
    }
}