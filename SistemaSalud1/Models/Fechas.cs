using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaSalud1.Models
{
    public class Fechas
    {

        private DateTime fecha_ingreso_eps;

        public Fechas(DateTime fecha_ingreso_eps)
        {
            this.Fecha_ingreso_eps = fecha_ingreso_eps;
        }

        public DateTime Fecha_ingreso_eps { get => fecha_ingreso_eps; set => fecha_ingreso_eps = value; }
    }    
}