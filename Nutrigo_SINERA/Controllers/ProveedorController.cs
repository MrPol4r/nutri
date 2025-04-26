using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nutrigo_SINERA.Controllers
{
    public class ProveedorController : Controller
    {

        /// <summary>
        /// GET: Proveedor/Index
        /// Muestra el formulario para registrar el plan semanal (3 menús).
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        // GET: Proveedor/Perfil
        public ActionResult Perfil()
        {
            return View();
        }

        // GET: Proveedor/Notificaciones
        public ActionResult Notificaciones()
        {
            return View();
        }



    }
}