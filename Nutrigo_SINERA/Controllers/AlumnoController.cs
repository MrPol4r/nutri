using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nutrigo_SINERA.Models;

namespace Nutrigo_SINERA.Controllers
{
    public class AlumnoController : Controller
    {

        // 📊 Gráficas de progreso
        public ActionResult Index()
        {
            // más adelante: cargar datos para Chart.js
            return View();
        }

        // 📦 Recomendaciones Personalizadas IA
        public ActionResult RecomendacionesPersonalizadas()
            => View();

        // 📋 Evaluación de Hábitos
        public ActionResult EvaluacionHabitos()
            => View();

        // ⚖️ Monitoreo de IMC
        public ActionResult MonitoreoIMC()
            => View();

        // 🗓 Plan semanal del Sistema
        public ActionResult PlanSemanalSistema()
            => View();

        // 🗓 Plan semanal de Proveedores
        public ActionResult PlanSemanalProveedores()
            => View();

        // 👤 Editar Perfil
        public ActionResult EditarPerfil()
            => View();

        // 🔔 Notificaciones
        public ActionResult Notificaciones()
            => View();

    }
}