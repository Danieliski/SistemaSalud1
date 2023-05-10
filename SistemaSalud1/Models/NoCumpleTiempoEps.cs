using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaSalud1.Models
{
    public class NoCumpleTiempoEps : Exception
    {
        public NoCumpleTiempoEps() : base("El tiempo Que Tiene En La Eps Tiene Que Ser Mayor a 3 Meses") { }
    }
}