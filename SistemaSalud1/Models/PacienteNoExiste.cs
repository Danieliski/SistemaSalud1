using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaSalud1.Models
{
    public class PacienteNoExiste : Exception
    {
        public PacienteNoExiste() : base("El Usuario Con Esa Identificacion No Existe") { }
    }
}