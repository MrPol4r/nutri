using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nutrigo_SINERA.Models;

namespace Nutrigo_SINERA.Controllers
{
    public class ColegioController : Controller
    {
        // GET: Colegio
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registro() => View();

        [HttpPost]
        public ActionResult Registro(ClsColegio model)
        {
            Session["RegistroColegio"] = model;
            return RedirectToAction("Pagar", "Colegio");
        }

        //metodo de pagos o pasarela

        public ActionResult Pagar() => View();

        [HttpPost]
        public ActionResult ConfirmarPago()
        {
            var datos = Session["RegistroColegio"] as ClsColegio;

            // Aquí simularías la validación de tarjeta (por ahora, solo avanzamos)
            // Puedes capturar datos del formulario si deseas, por ejemplo:
            // string tarjeta = Request.Form["NumeroTarjeta"];

            Session["Usuario"] = datos.RepresentanteCorreo;
            Session["Rol"] = "Colegio";

            return RedirectToAction("Index", "Colegio");
        }

        //Gestionar proveedores 

        public static List<ClsProveedor> ListaProveedores = new List<ClsProveedor>();


        public ActionResult Proveedores(string search)
        {
            if (Session["Rol"]?.ToString() != "Colegio")
                return RedirectToAction("Index", "Login");

            ViewBag.Search = search;
            var data = string.IsNullOrEmpty(search)
                ? ListaProveedores
                : ListaProveedores.Where(p => p.Nombre.ToLower().Contains(search.ToLower())).ToList();

            return View(data);
        }

        [HttpPost]
        public ActionResult AgregarProveedor(ClsProveedor proveedor)
        {
            proveedor.Id = ListaProveedores.Count > 0 ? ListaProveedores.Max(p => p.Id) + 1 : 1;
            ListaProveedores.Add(proveedor);
            return RedirectToAction("Proveedores");
        }

        public ActionResult EditarProveedor(int id)
        {
            var proveedor = ListaProveedores.FirstOrDefault(p => p.Id == id);
            return View(proveedor);
        }

        [HttpPost]
        public ActionResult EditarProveedor(ClsProveedor model)
        {
            var proveedor = ListaProveedores.FirstOrDefault(p => p.Id == model.Id);
            if (proveedor != null)
            {
                proveedor.Nombre = model.Nombre;
                proveedor.RUC = model.RUC;
                proveedor.Especialidad = model.Especialidad;
                proveedor.Correo = model.Correo;
                proveedor.Celular = model.Celular;
                proveedor.Contrasena = model.Contrasena;
            }
            return RedirectToAction("Proveedores");
        }

        public ActionResult VerProveedor(int id)
        {
            var proveedor = ListaProveedores.FirstOrDefault(p => p.Id == id);
            return View(proveedor);
        }

        public ActionResult EliminarProveedor(int id)
        {
            var proveedor = ListaProveedores.FirstOrDefault(p => p.Id == id);
            if (proveedor != null)
                ListaProveedores.Remove(proveedor);

            return RedirectToAction("Proveedores");
        }

        //ver reportes de salud de los estudiantes

        // Lista simulada de alumnos
        public static List<ClsAlumno> Alumnos = new List<ClsAlumno>
        {
            new ClsAlumno {
                Id = 1,
                Nombre = "Juan Pérez",
                Edad = 11,
                EstadoNutricional = "Peso Normal",
                FechaActualizacion = DateTime.Parse("2025-02-22"),
                ColegioId = 1,
                Salud = new ClsSalud { Peso = 43.5 }
            },
            new ClsAlumno {
                Id = 2,
                Nombre = "Lucía Ramírez",
                Edad = 9,
                EstadoNutricional = "Sobrepeso",
                FechaActualizacion = DateTime.Parse("2025-02-22"),
                ColegioId = 1,
                Salud = new ClsSalud { Peso = 39.2 }
            },
            new ClsAlumno {
                Id = 3,
                Nombre = "Diego Vargas",
                Edad = 10,
                EstadoNutricional = "Bajo Peso",
                FechaActualizacion = DateTime.Parse("2025-02-22"),
                ColegioId = 1,
                Salud = new ClsSalud { Peso = 35.8 }
            },
            new ClsAlumno {
                Id = 4,
                Nombre = "Paola Méndez",
                Edad = 10,
                EstadoNutricional = "Peso Normal",
                FechaActualizacion = DateTime.Parse("2025-02-22"),
                ColegioId = 2,
                Salud = new ClsSalud { Peso = 42.0 }
            }
        };


        public ActionResult Reportes()
        {
            if (Session["Rol"]?.ToString() != "Colegio")
                return RedirectToAction("Index", "Login");

            // Suponiendo que ColegioId está guardado en sesión
            int colegioId = ObtenerColegioIdDesdeUsuario(Session["Usuario"]?.ToString());

            var alumnosDelColegio = Alumnos.Where(a => a.ColegioId == colegioId).ToList();

            return View(alumnosDelColegio);
        }

        // Simulador de búsqueda de ID de colegio (puedes reemplazarlo con BD)
        private int ObtenerColegioIdDesdeUsuario(string correoUsuario)
        {
            // Simulación: si el correo contiene 'admin', colegio ID 1; si no, colegio ID 2
            return correoUsuario != null && correoUsuario.Contains("admin") ? 1 : 2;
        }
        // --- Sección: Lista de estudiantes (simulada) ---
        public static List<ClsAlumno> ListaAlumnos = new List<ClsAlumno>();





        // --- Acción GET: Listar estudiantes ---
        public ActionResult Estudiantes(string search)
        {
            if (Session["Rol"]?.ToString() != "Colegio")
                return RedirectToAction("Index", "Login");

            ViewBag.Search = search;
            var data = string.IsNullOrEmpty(search)
                ? ListaAlumnos
                : ListaAlumnos.Where(e => e.Nombre.ToLower().Contains(search.ToLower())).ToList();

            return View(data);
        }

        // --- Acción POST: Agregar estudiante ---
        [HttpPost]
        public ActionResult AgregarAlumno(FormCollection form)
        {
            var alumno = new ClsAlumno
            {
                Id = ListaAlumnos.Count > 0 ? ListaAlumnos.Max(e => e.Id) + 1 : 1,
                Nombre = form["Nombre"],
                Grado = form["Grado"],
                Seccion = form["Seccion"],
                Correo = form["Correo"],
                Contrasena = form["Contrasena"],
                FechaActualizacion = DateTime.Now,
                EstadoNutricional = "Sin Evaluar", // opcional
                ColegioId = ObtenerColegioIdDesdeUsuario(Session["Usuario"]?.ToString())
            };

            ListaAlumnos.Add(alumno);
            TempData["Mensaje"] = "Estudiante registrado correctamente.";
            return RedirectToAction("Estudiantes");
        }
        public ActionResult VerAlumno(int id)
        {
            var alumno = ListaAlumnos.FirstOrDefault(a => a.Id == id);
            if (alumno == null)
                return HttpNotFound();

            return View(alumno);
        }
        // GET: Colegio/EditarAlumno/5
        public ActionResult EditarAlumno(int id)
        {
            // Busca el alumno por Id
            var alumno = ListaAlumnos.FirstOrDefault(a => a.Id == id);
            if (alumno == null)
                return HttpNotFound();

            // Pasa el alumno encontrado a la vista
            return View(alumno);
        }

        // POST: Colegio/EditarAlumno
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarAlumno(ClsAlumno model)
        {
            // Busca el alumno en la lista usando el Id del modelo
            var alumno = ListaAlumnos.FirstOrDefault(a => a.Id == model.Id);
            if (alumno != null)
            {
                // Actualiza propiedades
                alumno.Nombre = model.Nombre;
                alumno.Edad = model.Edad;
                alumno.Sexo = model.Sexo;
                alumno.Grado = model.Grado;
                alumno.Seccion = model.Seccion;
                alumno.Correo = model.Correo;
                alumno.Contrasena = model.Contrasena;
                alumno.FechaActualizacion = DateTime.Now;
                // (Si quieres actualizar Habitos o Salud, hazlo aquí)
            }

            // Regresa al listado
            TempData["Mensaje"] = "Alumno editado correctamente.";
            return RedirectToAction("Estudiantes");
        }
        public ActionResult EliminarAlumno(int id)
        {
            var alumno = ListaAlumnos.FirstOrDefault(a => a.Id == id);
            if (alumno != null)
                ListaAlumnos.Remove(alumno);

            TempData["Mensaje"] = "Estudiante eliminado correctamente.";
            return RedirectToAction("Estudiantes");
        }




        // GET: Colegio/EditarPerfil
        public ActionResult EditarPerfil()
        {
            // 1) Verifico sesión de usuario:
            //if (Session["Rol"]?.ToString() != "Colegio")
            //    return RedirectToAction("Index", "Login");

            // 2) Intento cargar el perfil de sesión
            var model = Session["PerfilColegio"] as ClsColegio;
            if (model == null)
            {
                // 3) Si no existe, creamos uno simulado:
                model = new ClsColegio
                {
                    Nombre = "Colegio San Simulado",
                    CodigoModular = "2025-EDU-001",
                    TipoColegio = "Público",
                    NivelEducativo = "Primaria",
                    Direccion = "Av. Ejemplo 123",
                    Ubicacion = "Tacna/Tacna/Tacna",
                    Telefono = "052-123456",
                    CorreoInstitucional = "contacto@sansimulado.edu.pe",
                    CantidadAlumnos = 120,
                    Activo = true,
                    RepresentanteNombre = "María Pérez",
                    RepresentanteCargo = "Directora",
                    RepresentanteDNI = "87654321",
                    RepresentanteCorreo = "maria.perez@sansimulado.edu.pe",
                    RepresentanteCelular = "987654321",
                    Contrasena = "Simulado@2025"
                };

                // 4) Lo guardo en sesión para usarlo luego
                Session["PerfilColegio"] = model;
            }

            return View(model);
        }



        // POST: Colegio/EditarPerfil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPerfil(ClsColegio model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Aquí guardarías en Base de Datos; de momento lo refloto en sesión:
            Session["PerfilColegio"] = model;
            TempData["PerfilMensaje"] = "Perfil actualizado correctamente.";
            return RedirectToAction("Index", "Colegio");
        }















        // … tus otras acciones …

        // Lista simulada de notificaciones usando Tuple<Fecha, Mensaje, ¿Leída?>
        private static List<Tuple<DateTime, string, bool>> ListaNotificaciones =
            new List<Tuple<DateTime, string, bool>>
            {
            Tuple.Create(DateTime.Now.AddDays(-2), "Reporte semanal listo para descargar.", false),
            Tuple.Create(DateTime.Now.AddDays(-1), "Nuevo alumno registrado en tu institución.", false),
            Tuple.Create(DateTime.Now.AddHours(-3), "Recordatorio: revisa el estado nutricional de tus alumnos.", true)
            };

        // GET: Colegio/Notificaciones
        public ActionResult Notificaciones()
        {
            // Orden descendente por fecha
            var ordenadas = ListaNotificaciones
                .OrderByDescending(n => n.Item1)
                .ToList();
            return View(ordenadas);
        }

    }
}