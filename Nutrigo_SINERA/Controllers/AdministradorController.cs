using Nutrigo_SINERA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nutrigo_SINERA.Controllers
{
    public class AdministradorController : Controller
    {
        // Simulación de base de datos en sesión (puedes reemplazar por base real luego)
        public static List<ClsColegio> ListaColegios = new List<ClsColegio>
        {
            new ClsColegio { Nombre = "Colegio A", RepresentanteCorreo = "a@edu.com", Activo = true },
            new ClsColegio { Nombre = "Colegio B", RepresentanteCorreo = "b@edu.com", Activo = false }
        };

        public ActionResult Index()
        {
            ViewBag.Total = ListaColegios.Count;
            ViewBag.Activos = ListaColegios.FindAll(c => c.Activo).Count;
            ViewBag.Inactivos = ListaColegios.FindAll(c => !c.Activo).Count;

            return View(ListaColegios);
        }

        public ActionResult Perfil()
        {
            // Simulado: datos del admin
            var admin = new ClsAdministrador
            {
                Nombre = "Admin General",
                Correo = "admin@nutrigo.com",
                Celular = "999999999"
            };
            return View(admin);
        }
        public ActionResult Notificaciones()
        {
            ViewBag.Mensajes = new List<string>
            {
                "Nuevo colegio registrado: Colegio Tarata",
 
            };
            return View();
        }
        public ActionResult Ver(string correo)
        {
            var colegio = ListaColegios
                .FirstOrDefault(c => c.RepresentanteCorreo == correo);

            if (colegio == null)
                return HttpNotFound();

            return View(colegio);
        }



        public ActionResult Eliminar(string correo)
        {
            // Eliminar colegio y sus relaciones (simulado)
            ListaColegios.RemoveAll(c => c.RepresentanteCorreo == correo);

            // También eliminarías sus padres y proveedores aquí
            // ej: ListaPadres.RemoveAll(p => p.CorreoColegio == correo);

            TempData["Mensaje"] = "Colegio eliminado correctamente.";
            return RedirectToAction("Index");
        }
        public ActionResult Agregar() => View(new ClsColegio());

        [HttpPost]
        public ActionResult Agregar(ClsColegio model)
        {
            model.Activo = true;
            ListaColegios.Add(model);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(string correo)
        {
            var colegio = ListaColegios.FirstOrDefault(c => c.RepresentanteCorreo == correo);
            return View(colegio);
        }

        [HttpPost]
        public ActionResult Editar(ClsColegio model)
        {
            var colegio = ListaColegios.FirstOrDefault(c => c.RepresentanteCorreo == model.RepresentanteCorreo);
            if (colegio != null)
            {
                colegio.Nombre = model.Nombre;
                colegio.Activo = model.Activo;
                // actualizar otros campos...
            }
            return RedirectToAction("Index");
        }
    }
}