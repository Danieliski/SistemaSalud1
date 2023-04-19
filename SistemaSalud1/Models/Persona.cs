using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaSalud1.Models
{
    public class Persona
    {
        private string id;
        private string nombre;
        private string apellido;
        private DateTime fecha_nacimiento;

        public Persona(string id, string nombre, string apellido, DateTime fecha_nacimiento)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Fecha_nacimiento = fecha_nacimiento;
        }

        public string Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public DateTime Fecha_nacimiento { get => fecha_nacimiento; set => fecha_nacimiento = value; }
    }
}