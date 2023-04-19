using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaSalud1.Models;


namespace SistemaSalud1Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public Paciente crearPacientePrueba()
        {
            string id = "1000592";
            string nombre = "Daniel";
            string apellido = "Henao";
            DateTime fecha_nacimiento = DateTime.Now;
            string eps = "Sura";
            string historia = "hhhhhhhhhhhhhhhhhhh";
            int cantidad_enfermedades = 5;
            string enfermedad_rele = "cancer";
            DateTime fecha_salud = DateTime.Now;
            string regimen = "Subsidiado";
            string afiliacion = "Beneficiario";
            double semana_cotizada = 4;
            double costos = 6900;

            Persona persona = new Persona(id, nombre, apellido, fecha_nacimiento);
            Fechas fecha = new Fechas(fecha_salud);
            SistemaSalud sistema = new SistemaSalud(regimen,afiliacion,semana_cotizada,costos);

            Paciente paciente = new Paciente(persona, eps, historia, cantidad_enfermedades, enfermedad_rele, fecha, sistema);

            return paciente;

        }

        [TestMethod]
        public void ingresarPacienteTeste()
        {
            //Arrange
            SistemaSalud1.Models.ListasPacientes target = new SistemaSalud1.Models.ListasPacientes();
            string expected = "1000592";

            //act
            Paciente paciente = crearPacientePrueba();
            target.ingresarPaciente(paciente);

            //assert
            Assert.AreEqual(expected, target.Pacientes[0].Persona.Id);
               
        }

        [TestMethod]
        public void ValidarPacienteTeste()
        {
            SistemaSalud1.Models.ListasPacientes target = new SistemaSalud1.Models.ListasPacientes();
            string excepted = "1000592";

            Paciente paciente = crearPacientePrueba();
            target.ValidarPaciente(paciente.Persona.Id);

            Assert.AreEqual(excepted, target.Pacientes[0]);
        }

    }
}
