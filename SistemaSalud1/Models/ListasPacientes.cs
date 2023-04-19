using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace SistemaSalud1.Models
{
    public class ListasPacientes
    {
        public static List<Paciente> pacientes = new List<Paciente>();

        public List<Paciente> Pacientes { get => pacientes; set => pacientes = value; }

        public void ingresarPaciente(Paciente paciente)
        {
            pacientes.Add(paciente);
        }

        public bool ValidarPaciente(string id)
        {
            foreach (Paciente paciente in pacientes)
            {
                if (paciente.Persona.Id == id)
                {
                    return true;
                }

            }
            return false;
        }

        public Paciente BuscarPaciente(string id)
        {
            foreach (Paciente paciente in pacientes)
            {
                if (paciente.Persona.Id == id)
                {
                    return paciente;
                }

            }
            return null;
        }

        public void ActualizarEps(Paciente paciente, string new_eps)
        {
            if (paciente.Sistema.Semana_cotizada > 3)
            {
                paciente.Eps = new_eps;

            }
        }
        public void ActualizarHistoria(Paciente paciente, string new_Historia)
        {


            paciente.Historia_clinica = new_Historia;


        }

        public void ActualizacionDatos(Paciente paciente, string new_eps, string new_Historia, int new_costos, string new_afiliacion, string new_EnfermedadRelevante)
        {
            if (paciente.Sistema.Semana_cotizada > 3)
            {
                paciente.Eps = new_eps;

            }
            paciente.Historia_clinica = new_Historia;

            paciente.Sistema.Costos = new_costos;

            paciente.Enfermedad_relevante = new_EnfermedadRelevante;

            paciente.Sistema.Tipo_afiliacion = new_afiliacion;
        }

        public static double CalcularProcentaje(double num1, double num2)
        {
            try
            {
                double porcentaje = (num1 * 100) / num2;
                return porcentaje;
            }
            catch (DivideByZeroException)
            {
                return 0;

            }
        }

        public static double Mostrar_porcentaje_sura(List<Paciente> pacientes)
        {
            if(pacientes.Count()==0) return 0;

            double suma_sura = pacientes.Where(eps => eps.Eps == "Sura").Sum(Costos => Costos.Sistema.Costos);
            double total_costos = pacientes.Sum(costo => costo.Sistema.Costos);
            double porcentaje_sura = CalcularProcentaje(suma_sura, total_costos);
            return porcentaje_sura;

        }
        public static double Mostrar_porcentaje_Nueva_eps(List<Paciente> pacientes)
        {
            if (pacientes.Count() == 0) return 0;
            double suma_eps = pacientes.Where(eps => eps.Eps == "Nueva Eps").Sum(Costos => Costos.Sistema.Costos);
            double total_costos = pacientes.Sum(costo => costo.Sistema.Costos);
            double porcentaje_eps = CalcularProcentaje(suma_eps, total_costos);
            return porcentaje_eps;

        }
        public static double Mostrar_porcentaje_SaludTotal(List<Paciente> pacientes)
        {
            if (pacientes.Count() == 0) return 0;


            double suma_saludtotal = pacientes.Where(eps => eps.Eps == "Salud Total").Sum(Costos => Costos.Sistema.Costos);
            double total_costos = pacientes.Sum(costo => costo.Sistema.Costos);
            double porcentaje_saludtotal = CalcularProcentaje(suma_saludtotal, total_costos);
            return porcentaje_saludtotal;

        }
        public static double Mostrar_porcentaje_Sanitas(List<Paciente> pacientes)
        {
            if (pacientes.Count() == 0) return 0;

            double suma_sanitas = pacientes.Where(eps => eps.Eps == "Sanitas").Sum(Costos => Costos.Sistema.Costos);
            double total_costos = pacientes.Sum(costo => costo.Sistema.Costos);
            double porcentaje_sanitas = CalcularProcentaje(suma_sanitas, total_costos);
            return porcentaje_sanitas;

        }
        public static double Mostrar_porcentaje_Savia(List<Paciente> pacientes)
        {
            if (pacientes.Count() == 0) return 0;
            double suma_Savia = pacientes.Where(eps => eps.Eps == "Savia").Sum(Costos => Costos.Sistema.Costos);
            double total_costos = pacientes.Sum(costo => costo.Sistema.Costos);
            double porcentaje_Savia = CalcularProcentaje(suma_Savia, total_costos);
            return porcentaje_Savia;

        }

        public static List<double> Mostrar_porcentaje_general(List<Paciente> pacientes)
        {
            double porcentajesSura       = ListasPacientes.Mostrar_porcentaje_sura(pacientes);
            double porcentajesNuevaEps   = ListasPacientes.Mostrar_porcentaje_Nueva_eps(pacientes);
            double porcentajesSaludtotal = ListasPacientes.Mostrar_porcentaje_SaludTotal(pacientes);
            double porcentajesSanitas    = ListasPacientes.Mostrar_porcentaje_Sanitas(pacientes);
            double porcentajesSavia      = ListasPacientes.Mostrar_porcentaje_Savia(pacientes);

            List<double> porcentajes = new List<double> { porcentajesSura, porcentajesNuevaEps, porcentajesSaludtotal, porcentajesSanitas, porcentajesSavia };

            return porcentajes;
        }

        static double Total_sura(List<Paciente> pacientes)
        {
            double suma_sura = pacientes.Where(eps => eps.Eps == "Sura").Sum(costo => costo.Sistema.Costos);

            return suma_sura;
        }
        static double Total_Nueva_Eps(List<Paciente> pacientes)
        {
            double suma_Nueva_eps = pacientes.Where(eps => eps.Eps == "Nueva Eps").Sum(costo => costo.Sistema.Costos);

            return suma_Nueva_eps;
        }
        static double Total_Salud_Total(List<Paciente> pacientes)
        {
            double suma_saludTotal = pacientes.Where(eps => eps.Eps == "Salud Total").Sum(costo => costo.Sistema.Costos);

            return suma_saludTotal;
        }
        static double Total_Sanitas(List<Paciente> pacientes)
        {
            double suma_Sanitas = pacientes.Where(eps => eps.Eps == "Sanitas").Sum(costo => costo.Sistema.Costos);

            return suma_Sanitas;
        }
        static double Total_Savia(List<Paciente> pacientes)
        {
            double suma_Savia = pacientes.Where(eps => eps.Eps == "Savia").Sum(costo => costo.Sistema.Costos);

            return suma_Savia;
        }

        public static List<double> Mostrar_costos_general(List<Paciente> pacientes)
        {
            double total_sura = ListasPacientes.Total_sura(pacientes);
            double total_nueva_eps = ListasPacientes.Total_Nueva_Eps(pacientes);
            double total_saludtotal = ListasPacientes.Total_Salud_Total(pacientes);
            double total_sanitas = ListasPacientes.Total_Sanitas(pacientes);
            double total_savia = ListasPacientes.Total_Savia(pacientes);

            List<double> total_costos_por_eps = new List<double> { total_sura, total_nueva_eps, total_saludtotal, total_sanitas, total_savia };

            return total_costos_por_eps;

        }

        public static double Mostrar_porcentaje_sin_enfermedades(List<Paciente> pacientes)
        {
            double porcentaje_pacientes_sin_enfermedades;
            var total_sin_enfermedades = pacientes.Where(x => x.Cantidad_enfermedades == 0).ToList();
            double Tsin_enfermedades = total_sin_enfermedades.Count();

            var totalpacientes = pacientes.Count();

            porcentaje_pacientes_sin_enfermedades = CalcularProcentaje(Tsin_enfermedades, totalpacientes);


            return porcentaje_pacientes_sin_enfermedades;

        }

        public static List<double> Mostrar_total_sin_enfermedades(List<Paciente> pacientes)
        {
            double total_sin_enfermedades = ListasPacientes.Mostrar_porcentaje_sin_enfermedades(pacientes);

            List<double> Total = new List<double> { total_sin_enfermedades };

            return Total;
        }

        public static List<Paciente> costo_mayor(List<Paciente> pacientes)
        {
            if (pacientes.Count() == 0)
            {
                return null;
            }

            var Mayorcosto = pacientes.Where(x => x.Sistema.Costos ==(pacientes.Max(y => y.Sistema.Costos))).ToList();

            return Mayorcosto;

        }
     
        static double Mostrar_porcentaje_niños(List<Paciente> pacientes)
        {
            DateTime fecha_actual = DateTime.Today;

            var total_pacientes = pacientes.Count();

            var fechas = pacientes.Where(fecha => (fecha_actual.Year - fecha.Persona.Fecha_nacimiento.Year) > 0 && (fecha_actual.Year - fecha.Persona.Fecha_nacimiento.Year) < 12).ToList();
            double porcentaje = CalcularProcentaje(fechas.Count(), total_pacientes);
            return porcentaje;

        }
        static double Mostrar_porcentaje_adolecentes(List<Paciente> pacientes)
        {
            DateTime fecha_actual = DateTime.Today;

            var total_pacientes = pacientes.Count();

            var fechas = pacientes.Where(fecha => (fecha_actual.Year - fecha.Persona.Fecha_nacimiento.Year) > 12 && (fecha_actual.Year - fecha.Persona.Fecha_nacimiento.Year) < 18).ToList();
            double porcentaje = CalcularProcentaje(fechas.Count(), total_pacientes);
            return porcentaje;

        }
        static double Mostrar_porcentaje_jovenes(List<Paciente> pacientes)
        {
            DateTime fecha_actual = DateTime.Today;

            var total_pacientes = pacientes.Count();

            var fechas = pacientes.Where(fecha => (fecha_actual.Year - fecha.Persona.Fecha_nacimiento.Year) > 18 && (fecha_actual.Year - fecha.Persona.Fecha_nacimiento.Year) < 30).ToList();
            double porcentaje = CalcularProcentaje(fechas.Count(), total_pacientes);
            return porcentaje;

        }
        static double Mostrar_porcentaje_adulto(List<Paciente> pacientes)
        {
            DateTime fecha_actual = DateTime.Today;

            var total_pacientes = pacientes.Count();

            var fechas = pacientes.Where(fecha => (fecha_actual.Year - fecha.Persona.Fecha_nacimiento.Year) > 30 && (fecha_actual.Year - fecha.Persona.Fecha_nacimiento.Year) < 55).ToList();
            double porcentaje = CalcularProcentaje(fechas.Count(), total_pacientes);
            return porcentaje;

        }
        static double Mostrar_porcentaje_adultoMayor(List<Paciente> pacientes)
        {
            DateTime fecha_actual = DateTime.Today;

            var total_pacientes = pacientes.Count();

            var fechas = pacientes.Where(fecha => (fecha_actual.Year - fecha.Persona.Fecha_nacimiento.Year) > 55 && (fecha_actual.Year - fecha.Persona.Fecha_nacimiento.Year) < 75).ToList();
            double porcentaje = CalcularProcentaje(fechas.Count(), total_pacientes);
            return porcentaje;

        }
        static double Mostrar_porcentaje_anciano(List<Paciente> pacientes)
        {
            DateTime fecha_actual = DateTime.Today;

            var total_pacientes = pacientes.Count();

            var fechas = pacientes.Where(fecha => (fecha_actual.Year - fecha.Persona.Fecha_nacimiento.Year) > 75).ToList();
            double porcentaje = CalcularProcentaje(fechas.Count(), total_pacientes);
            return porcentaje;

        }

        public static List<double> Mostrar_porcentajes_edades_general(List<Paciente> pacientes)
        {
            double porcentaje_niños = ListasPacientes.Mostrar_porcentaje_niños(pacientes);
            double porcentaje_adolecentes = ListasPacientes.Mostrar_porcentaje_adolecentes(pacientes);
            double porcentaje_jovenes = ListasPacientes.Mostrar_porcentaje_jovenes(pacientes);
            double porcentaje_adulto = ListasPacientes.Mostrar_porcentaje_adulto(pacientes);
            double porcentaje_AdultoMayor = ListasPacientes.Mostrar_porcentaje_adultoMayor(pacientes);
            double porcentaje_anciono = ListasPacientes.Mostrar_porcentaje_anciano(pacientes);

            List<double> total_porcentajes_edades = new List<double> { porcentaje_niños, porcentaje_adolecentes, porcentaje_jovenes, porcentaje_adulto, porcentaje_AdultoMayor, porcentaje_anciono };

            return total_porcentajes_edades;
        }

        public static double Mostrar_porcentaje_contributivo(List<Paciente> pacientes)
        {
            var total_regimen = pacientes.Where(x => x.Sistema.Tipo_regimen == "Contributivo" | x.Sistema.Tipo_regimen == "Subsidiado").ToList();
            double Tregimen = total_regimen.Count();
            
            var total_contributario = pacientes.Where(x => x.Sistema.Tipo_regimen == "Contributivo").ToList();
            double Tcontributario = total_contributario.Count();

            double porcentaje_contributario = CalcularProcentaje(Tcontributario, Tregimen);

            return porcentaje_contributario;

        }
        public static double Mostrar_porcentaje_subsidiado(List<Paciente> pacientes)
        {
            var total_regimen = pacientes.Where(x => x.Sistema.Tipo_regimen == "Contributivo" | x.Sistema.Tipo_regimen == "Subsidiado").ToList();
            double Tregimen = total_regimen.Count();

            var total_Subsidiado = pacientes.Where(x => x.Sistema.Tipo_regimen == "Subsidiado").ToList();
            double TSubsidiado = total_Subsidiado.Count();

            double porcentaje_Subsidiado = CalcularProcentaje(TSubsidiado, Tregimen);

            return porcentaje_Subsidiado;


        }
        public static List<double> Mostrar_porcentaje_regimen(List<Paciente> pacientes)
        {
            double porcentaje_contributivo = ListasPacientes.Mostrar_porcentaje_contributivo(pacientes);
            double porcentaje_subsidiado = ListasPacientes.Mostrar_porcentaje_subsidiado(pacientes);

            List<double> total_porcentajes_regimen = new List<double> { porcentaje_contributivo, porcentaje_subsidiado };

            return total_porcentajes_regimen;
        }

        static double Mostrar_porcentaje_cotizante(List<Paciente> pacientes)
        {
            var total_afilacion = pacientes.Where(x => x.Sistema.Tipo_afiliacion == "Contributivo" | x.Sistema.Tipo_afiliacion == "Beneficiario").ToList();
            double Tafilacion = total_afilacion.Count();

            var total_cotizante = pacientes.Where(x => x.Sistema.Tipo_afiliacion == "Cotizante").ToList();
            double Tcotizante = total_cotizante.Count();

            return CalcularProcentaje(Tcotizante, Tafilacion);

        }
        public static double Mostrar_porcentaje_beneficiario(List<Paciente> pacientes)
        {
            var total_afilacion = pacientes.Where(x => x.Sistema.Tipo_afiliacion == "Cotizante" | x.Sistema.Tipo_afiliacion == "Beneficiario").ToList();
            double Tafilacion = total_afilacion.Count();

            var total_beneficiario = pacientes.Where(x => x.Sistema.Tipo_afiliacion == "Beneficiario").ToList();
            double Tbeneficiario = total_beneficiario.Count();

            return CalcularProcentaje(Tbeneficiario, Tafilacion);

        }

        public static List<double> Mostrar_porcentaje_Afilacion(List<Paciente> pacientes)
        {
            double porcentaje_cotizante = ListasPacientes.Mostrar_porcentaje_cotizante(pacientes);
            double porcentaje_beneficiario = ListasPacientes.Mostrar_porcentaje_beneficiario(pacientes);

            List<double> total_porcentajes_afilacion = new List<double> { porcentaje_cotizante, porcentaje_beneficiario };

            return total_porcentajes_afilacion;
        }

        public static double Enfermedad_Relevante(List<Paciente> pacientes)
        {
            var enfermedad_cancer = pacientes.Where(x => x.Enfermedad_relevante == "cancer").ToList();

            return enfermedad_cancer.Count(); 
        }


        


    }
}