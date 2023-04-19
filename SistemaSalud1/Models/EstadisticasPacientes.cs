using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaSalud1.Models
{
    public class EstadisticasPacientes
    {
        public List<double> porcentajeCostos;
        public List<double> totalCostos;
        public double pacienteSinEnfermedad;
        public Paciente pacienteMayorCosto;
        public List<double> pacientesRangoEdades;
        public List<double> pacientesPorRegimen;
        public List<double> pacientesPorAfilacion;
        public double EnfermedadRelevante;

        public EstadisticasPacientes(List<double> porcentajeCostos, List<double> totalCostos, double pacienteSinEnfermedad, Paciente pacienteMayorCosto, List<double> pacientesRangoEdades, List<double> pacientesPorRegimen, List<double> pacientesPorAfilacion, double enfermedadRelevante)
        {
            this.porcentajeCostos = porcentajeCostos;
            this.totalCostos = totalCostos;
            this.pacienteSinEnfermedad = pacienteSinEnfermedad;
            this.pacienteMayorCosto = pacienteMayorCosto;
            this.pacientesRangoEdades = pacientesRangoEdades;
            this.pacientesPorRegimen = pacientesPorRegimen;
            this.pacientesPorAfilacion = pacientesPorAfilacion;
            EnfermedadRelevante = enfermedadRelevante;
        }
    }

}