using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Nutrigo_SINERA.Models;
using System.IO;
using System.Configuration;

namespace Nutrigo_SINERA.Controllers
{
    public class LoginController : Controller
    {
        private const string recaptchaSecretKey = null;

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ClsLogin model)
        {
            // Validar reCAPTCHA
            //if (!IsCaptchaValid())
            //{
            //    ViewBag.Error = "Por favor valida el reCAPTCHA.";
            //    return View();
            //}

            var usuario = AutenticarUsuario(model.Usuario, model.Password);

            if (usuario != null)
            {
                Session["Usuario"] = usuario.Usuario;
                Session["Rol"] = usuario.Rol;

                switch (usuario.Rol)
                {
                    case "Colegio":
                        return RedirectToAction("Index", "Colegio");
                    case "Proveedor":
                        return RedirectToAction("Index", "Proveedor");
                    case "Alumno":
                        return RedirectToAction("Index", "Alumno");
                    case "Administrador":
                        return RedirectToAction("Index", "Administrador");
                }
            }

            ViewBag.Error = "Credenciales incorrectas";
            return View();
        }

        private bool IsCaptchaValid()
        {
            var response = Request.Form["g-recaptcha-response"];
            var secret = ConfigurationManager.AppSettings["RecaptchaSecretKey"];
            var apiUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={response}";

            using (var client = new WebClient())
            {
                var jsonResult = client.DownloadString(apiUrl);
                var js = new JavaScriptSerializer();
                dynamic result = js.Deserialize<object>(jsonResult);
                return Convert.ToBoolean(result["success"]);
            }
        }


        private ClsUsuario AutenticarUsuario(string user, string pass)
        {
            var lista = new List<ClsUsuario>
            {
                new ClsUsuario { Usuario = "admin", Password = "admin123", Rol = "Administrador" },
                new ClsUsuario { Usuario = "colegio1", Password = "123", Rol = "Colegio" },
                new ClsUsuario { Usuario = "proveedor1", Password = "123", Rol = "Proveedor" },
                new ClsUsuario { Usuario = "alumno1", Password = "123", Rol = "Alumno" }
            };

            return lista.FirstOrDefault(u => u.Usuario == user && u.Password == pass);
        }

    }
}
