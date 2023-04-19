using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaSalud1.Models
{
    public class SistemaSalud
    {

        private string tipo_regimen;
        private string tipo_afiliacion;
        private double semana_cotizada;
        private double costos;

        public SistemaSalud(string tipo_regimen, string tipo_afiliacion, double semana_cotizada, double costos)
        {
            this.Tipo_regimen = tipo_regimen;
            this.Tipo_afiliacion = tipo_afiliacion;
            this.Semana_cotizada = semana_cotizada;
            this.Costos = costos;
        }

        public string Tipo_regimen { get => tipo_regimen; set => tipo_regimen = value; }
        public string Tipo_afiliacion { get => tipo_afiliacion; set => tipo_afiliacion = value; }
        public double Semana_cotizada { get => semana_cotizada; set => semana_cotizada = value; }
        public double Costos { get => costos; set => costos = value; }
    }   
}