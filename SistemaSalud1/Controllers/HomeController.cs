using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaSalud1.Models;

namespace SistemaSalud1.Controllers
{
    public class HomeController : Controller
    {   
        ListasPacientes listapaciente = new ListasPacientes();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult CambiarEps()
        {
            return View();
        }
        public ActionResult CambiarHistoria()
        {
            return View();
        }
        public ActionResult CambioDatos()
        {
            return View();
        }
        public ActionResult EncontrarPaciente()
        {
            return View();
        }

        public ActionResult MostrarPacienteBuscado()
        {
            string id = (Request.Form["id"].ToString());
            if (listapaciente.ValidarPaciente(id))
            {
                Paciente paciente = listapaciente.BuscarPaciente(id);
                return View(paciente);
            }
            return null;
        }

        public ActionResult ActualizarDatos()
        {
            string id = (Request.Form["id"]).ToString();
            if (listapaciente.ValidarPaciente(id))
            {
                Paciente paciente = listapaciente.BuscarPaciente(id);

                string nuevoId = id;
                TempData["nuevoId"] = nuevoId;
                TempData.Keep("nuevoId");
                return View(paciente);

            }
            return View();

        }
        public ActionResult ActualizarHistoria()
        {
            string id = (Request.Form["id"]).ToString();
            if (listapaciente.ValidarPaciente(id))
            {
                Paciente paciente = listapaciente.BuscarPaciente(id);

                string nuevoId = id;
                TempData["nuevoId"] = nuevoId;
                TempData.Keep("nuevoId");
                return View(paciente);

            }
            return View();

        }
        public ActionResult MostrarNuevaHistoria()
        {
            string nuevoId = (string)TempData["nuevoId"];
            TempData.Keep("nuevoId");
            string id = nuevoId;
            Paciente paciente = listapaciente.BuscarPaciente(id);
            string new_Historia = Request.Form["Historia"].ToString();
            listapaciente.ActualizarHistoria(paciente, new_Historia);
            return View(paciente);
        }
        
        public ActionResult MostrarDatos()
        {
            string nuevoId = (string)TempData["nuevoId"];
            TempData.Keep("nuevoId");
            string id = nuevoId;
            Paciente paciente = listapaciente.BuscarPaciente(id);
            string new_Historia = Request.Form["Historia"].ToString();
            string new_eps = Request.Form["eps"].ToString();
            int new_costos = Convert.ToInt32(Request.Form["Costos"]);
            string new_afiliacion = Request.Form["tipo_afiliacion"].ToString();
            string new_EnfermedadRelevante = Request.Form["enfermedad_relevante"].ToString();
            listapaciente.ActualizacionDatos(paciente,new_eps,new_Historia,new_costos,new_afiliacion,new_EnfermedadRelevante);
            return View(paciente);

        }
        public ActionResult EstadisticasPacientes()
        {
            List<double> porcentajes = ListasPacientes.Mostrar_porcentaje_general(listapaciente.Pacientes);
            List<double> total_costos_por_eps = ListasPacientes.Mostrar_costos_general(listapaciente.Pacientes);
            double total_sin_enfermedades = ListasPacientes.Mostrar_porcentaje_sin_enfermedades(listapaciente.Pacientes);
            List<Paciente>PacienteMayorCosto_eps = ListasPacientes.costo_mayor(listapaciente.Pacientes);
            List<double> porcentaje_edades = ListasPacientes.Mostrar_porcentajes_edades_general(listapaciente.Pacientes);
            List<double> porcentajes_regimen = ListasPacientes.Mostrar_porcentaje_regimen(listapaciente.Pacientes);
            List<double> porcentajes_afiliacion = ListasPacientes.Mostrar_porcentaje_Afilacion(listapaciente.Pacientes);
            double porcentajes_cancer = ListasPacientes.Enfermedad_Relevante(listapaciente.Pacientes);

            if(PacienteMayorCosto_eps == null)
            {
                return View();
            }
        
            EstadisticasPacientes estadisticas = new EstadisticasPacientes(porcentajes, total_costos_por_eps, total_sin_enfermedades, PacienteMayorCosto_eps[0], porcentaje_edades, porcentajes_regimen, porcentajes_afiliacion, porcentajes_cancer);
            return View(estadisticas);
        }
        public ActionResult EscogerEps()
        {
            string id = (Request.Form["id"]).ToString();
            if (listapaciente.ValidarPaciente(id))
            {
                Paciente paciente = listapaciente.BuscarPaciente(id);

                string nuevoId = id;
                TempData["nuevoId"] = nuevoId;
                TempData.Keep("nuevoId"); 
                return View(paciente);

            }
            return View();
        }

        public ActionResult MostrarCambioEps()
        {
            string nuevoId = (string)TempData["nuevoId"];
            TempData.Keep("nuevoId");
            string id = nuevoId;
            Paciente paciente = listapaciente.BuscarPaciente(id);
            string new_eps = Request.Form["eps"].ToString();
            listapaciente.ActualizarEps(paciente, new_eps);
            return View(paciente);
        }

        public ActionResult RegistrarPaciente()
        {
            return View();
        }

        public ActionResult MostrarPaciente() 
        {
            string id, nombre, apellido, eps, historia_clinica, enfermedad_relevante, tipo_regimen, tipo_afiliacion;
            int cantidad_enfermedades,costos;
            double semana_cotizada;
            DateTime fecha_nacimiento, fecha_ingreso_eps;

            id = Request.Form["id"].ToString();
            nombre = Request.Form["nombre"].ToString();
            apellido = Request.Form["apellido"].ToString();
            fecha_nacimiento = Convert.ToDateTime(Request.Form["fecha_nacimiento"]);
            eps = Request.Form["eps"].ToString();
            historia_clinica = Request.Form["historia_clinica"].ToString();
            cantidad_enfermedades = Convert.ToInt32(Request.Form["cantidad_enfermedades"]);
            enfermedad_relevante = (Request.Form["enfermedad_relevante"].ToLower()).Trim();
            fecha_ingreso_eps = Convert.ToDateTime(Request.Form["fecha_nacimiento"]);
            tipo_regimen = Request.Form["tipo_regimen"].ToString();
            tipo_afiliacion = Request.Form["tipo_afiliacion"].ToString();
            semana_cotizada = Convert.ToDouble(Request.Form["semana_cotizada"]);
            costos = Convert.ToInt32(Request.Form["costos"]);

            Fechas fechas = new Fechas(fecha_ingreso_eps);
            SistemaSalud sistemaSalud = new SistemaSalud(tipo_regimen, tipo_afiliacion, semana_cotizada, costos);
            Persona persona = new Persona(id, nombre, apellido, fecha_nacimiento);
            Paciente paciente = new Paciente(persona, eps, historia_clinica, cantidad_enfermedades, enfermedad_relevante, fechas, sistemaSalud);

            listapaciente.ingresarPaciente(paciente);

            return View(paciente);
        }

    }
}