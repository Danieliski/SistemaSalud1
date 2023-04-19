using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaSalud1.Models
{
    public class Paciente
    {
        private Persona persona;
        private string eps;
        private string historia_clinica;
        private int cantidad_enfermedades;
        private string enfermedad_relevante;
        private Fechas fechas;
        private SistemaSalud sistema;

        public Paciente(Persona persona, string eps, string historia_clinica, int cantidad_enfermedades, string enfermedad_relevante, Fechas fechas, SistemaSalud sistema)
        {
            this.Persona = persona;
            this.Eps = eps;
            this.Historia_clinica = historia_clinica;
            this.Cantidad_enfermedades = cantidad_enfermedades;
            this.Enfermedad_relevante = enfermedad_relevante;
            this.Fechas = fechas;
            this.Sistema = sistema;
        }

        public Persona Persona { get => persona; set => persona = value; }
        public string Eps { get => eps; set => eps = value; }
        public string Historia_clinica { get => historia_clinica; set => historia_clinica = value; }
        public int Cantidad_enfermedades { get => cantidad_enfermedades; set => cantidad_enfermedades = value; }
        public string Enfermedad_relevante { get => enfermedad_relevante; set => enfermedad_relevante = value; }
        public Fechas Fechas { get => fechas; set => fechas = value; }
        public SistemaSalud Sistema { get => sistema; set => sistema = value; }
    }
}