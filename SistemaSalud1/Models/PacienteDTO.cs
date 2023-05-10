using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc.Ajax;

namespace SistemaSalud1.Models
{
    public class PacienteDTO
    {
        static Conexion connection = new Conexion();
        SqlConnection sqlConnection = new SqlConnection(connection.conexion);
        public void RegistrarPacienteBD(Paciente paciente)
        {
            using (SqlConnection sqlconnection = new SqlConnection(connection.conexion))
            {
                sqlconnection.Open();

                // Inicia una transacción
                SqlTransaction transaction = sqlconnection.BeginTransaction();

                try
                {
                    // Inserta en la tabla tbpersona
                    using (SqlCommand command = new SqlCommand("INSERT INTO tbPersona (documento, nombre, apellido, fechaNacimiento) VALUES (@Id, @Nombre, @Apellido, @Fecha_nacimiento); SELECT SCOPE_IDENTITY()", sqlconnection, transaction))
                    {
                        command.Parameters.AddWithValue("@Id", paciente.Persona.Id);
                        command.Parameters.AddWithValue("@Nombre", paciente.Persona.Nombre);
                        command.Parameters.AddWithValue("@Apellido", paciente.Persona.Apellido);
                        command.Parameters.AddWithValue("@Fecha_nacimiento", paciente.Persona.Fecha_nacimiento);

                        // Obtiene el ID recién insertado
                        int personaId = Convert.ToInt32(command.ExecuteScalar());

                        // Inserta en la tabla TbTrabajador
                        using (SqlCommand command2 = new SqlCommand("INSERT INTO tbSistemaSalud (tipoRegimen, tipoAfiliacion, semanasCotizadas,costos) VALUES (@Tipo_regimen, @Tipo_afiliacion, @Semana_cotizada,@Costos); SELECT SCOPE_IDENTITY()", sqlconnection, transaction))
                        {
                            command2.Parameters.AddWithValue("@Tipo_regimen", paciente.Sistema.Tipo_regimen);
                            command2.Parameters.AddWithValue("@Tipo_afiliacion", paciente.Sistema.Tipo_afiliacion);
                            command2.Parameters.AddWithValue("@Semana_cotizada", paciente.Sistema.Semana_cotizada);
                            command2.Parameters.AddWithValue("@Costos", paciente.Sistema.Costos);


                            // Obtiene el ID recién insertado
                            int SistemaSaludId = Convert.ToInt32(command2.ExecuteScalar());

                            // Inserta en la tabla TbPersona
                            using (SqlCommand command3 = new SqlCommand("INSERT INTO tbPaciente (IdPersona,eps, historial, cantidadEnfermedades, enfermedadRelevante, fechaIngresoEps, IdSistemSalud) VALUES (@IdPersona,@Eps, @Historia_clinica, @Cantidad_enfermedades, @Enfermedad_relevante, @Fecha_ingreso_eps, @IdSistemSalud)", sqlconnection, transaction))
                            {
                                command3.Parameters.AddWithValue("@IdPersona", personaId); // Usa el ID del paciente recién insertado
                                command3.Parameters.AddWithValue("@Eps", paciente.Eps);
                                command3.Parameters.AddWithValue("@Historia_clinica", paciente.Historia_clinica);
                                command3.Parameters.AddWithValue("@Cantidad_enfermedades", paciente.Cantidad_enfermedades);
                                command3.Parameters.AddWithValue("@Enfermedad_relevante", paciente.Enfermedad_relevante);
                                command3.Parameters.AddWithValue("@Fecha_ingreso_eps", paciente.Fecha_ingreso_eps);
                                command3.Parameters.AddWithValue("@IdSistemSalud", SistemaSaludId); // Usa el ID del trabajador recién insertado

                                // Ejecuta la sentencia
                                command3.ExecuteNonQuery();
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    // Si hay un error, hace un rollback de la transacción
                    transaction.Rollback();

                    throw ex;
                }

                sqlconnection.Close();
            }
        }

        public List<Paciente> obtenerPacienteBD()
        {
            List<Paciente> lista = new List<Paciente>();
            
            using (SqlConnection sqlconnection = new SqlConnection(connection.conexion))
            {
                sqlconnection.Open();

                string query = @" SELECT pe.IdPersona, pe.documento, pe.nombre,pe.apellido,pe.fechaNacimiento,pa.eps,pa.historial,
                                         pa.cantidadEnfermedades,pa.enfermedadRelevante,pa.fechaIngresoEps, sis.IdSistemSalud,
                                         sis.tipoRegimen,sis.tipoAfiliacion,sis.semanasCotizadas,sis.costos
                                         FROM tbPaciente pa
                                         JOIN tbPersona pe ON pa.IdPersona = pe.IdPersona
                                         JOIN tbSistemaSalud sis ON pa.IdSistemSalud = sis.IdSistemSalud";
                using ( SqlCommand command = new SqlCommand(query, sqlconnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Persona persona = new Persona(reader["Id"].ToString(), reader["Nombre"].ToString(), reader["Apellido"].ToString(), Convert.ToDateTime(reader["Fecha_nacimiento"]));
                            SistemaSalud sistemasalud = new SistemaSalud(reader["tipo_regimen"].ToString(), reader["tipo_afiliacion"].ToString(), Convert.ToInt32(reader["semana_cotizada"]), Convert.ToInt32(reader["costos"]));
                            Paciente paciente = new Paciente(persona, reader["Eps"].ToString(), reader["Historia_clinica"].ToString(), Convert.ToInt32(reader["Cantidad_enfermedades"]), reader["Enfermedad_relevante"].ToString(), Convert.ToDateTime(reader["Fecha_ingreso_eps"]), sistemasalud);
                            lista.Add(paciente);
                        }
                    }
                }

                sqlconnection.Close();
            }

            return lista;

        }

        public void CambioEpsBD(string id , string new_eps)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection.conexion))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("SELECT IdPersona FROM tbPersona WHERE documento = @documento",sqlConnection);
                command.Parameters.AddWithValue("@documento",id);

                int idBD = Convert.ToInt32(command.ExecuteScalar());

                String query = "UPDATE tbPaciente SET eps = @new_eps WHERE IdPersona = @IdPersona";

                var parametros = new List<SqlParameter>
                {
                    new SqlParameter("@IdPersona",idBD),
                    new SqlParameter("@new_eps",new_eps)
                };
                SqlCommand updateCommand = new SqlCommand(query, sqlConnection);
                updateCommand.Parameters.AddRange(parametros.ToArray());

                updateCommand.ExecuteNonQuery();

                sqlConnection.Close();

            }

        }

        public void ActualizarEnfermedadBD( string id, string enfermedadRelevante)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection.conexion))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("SELECT IdPersona FROM tbPersona WHERE documento = @documento", sqlConnection);
                command.Parameters.AddWithValue("@documento", id);

                int idBD = Convert.ToInt32(command.ExecuteScalar());

                String query = "UPDATE tbPaciente" +
                    "" +
                    "" +
                    "" +
                    "rial SET enfermedadRelevante = @enfermedadRelevante WHERE IdPersona = @IdPersona";
                var parametros = new List<SqlParameter>
                {
                    new SqlParameter("@IdPersona",idBD),
                    new SqlParameter("@enfermedadRelevante",enfermedadRelevante )
                };
                
                SqlCommand updateCommand = new SqlCommand(query, sqlConnection);
                updateCommand.Parameters.AddRange(parametros.ToArray());

                updateCommand.ExecuteNonQuery();

                sqlConnection.Close();


            }
        }
        public void ActualizarHistorialClinicoBD(string id, string historial)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection.conexion))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("SELECT IdPersona FROM tbPersona WHERE documento = @documento", sqlConnection);
                command.Parameters.AddWithValue("@documento", id);

                int idBD = Convert.ToInt32(command.ExecuteScalar());

                String query = "UPDATE tbPaciente SET historial = @historial WHERE IdPersona = @IdPersona";
                
                var parametros = new List<SqlParameter>
                {
                    new SqlParameter("@IdPersona",idBD),
                    new SqlParameter("@historial",historial )
                };
                SqlCommand updateCommand = new SqlCommand(query, sqlConnection);
                updateCommand.Parameters.AddRange(parametros.ToArray());

                updateCommand.ExecuteNonQuery();

                sqlConnection.Close();



            }
        }

        public void ActualizarCostoBD(string id, int costos)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection.conexion))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("SELECT IdPersona FROM tbPersona WHERE documento = @documento", sqlConnection);
                command.Parameters.AddWithValue("@documento", id);

                int idBD = Convert.ToInt32(command.ExecuteScalar());
                
                String query = "UPDATE tbSistemaSalud SET costos = @costos WHERE IdPersona = @IdPersona";
                var parametros = new List<SqlParameter>
                {
                    new SqlParameter("@IdPersona",idBD),
                    new SqlParameter("@costos",costos )
                };
                SqlCommand updateCommand = new SqlCommand(query, sqlConnection);
                updateCommand.Parameters.AddRange(parametros.ToArray());

                updateCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
        }

        public void ActualizarAfiliacionBD(string id, string Tipo_afiliacion)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection.conexion))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand("SELECT IdPersona FROM tbPersona WHERE documento = @documento", sqlConnection);
                command.Parameters.AddWithValue("@documento", id);

                int idBD = Convert.ToInt32(command.ExecuteScalar());

                String query = "UPDATE tbSistemaSalud SET tipoAfiliacion = @tipoAfiliacion WHERE IdPersona = @IdPersona";
                var parametros = new List<SqlParameter>
                {
                    new SqlParameter("@IdPersona",idBD),
                    new SqlParameter("@tipoAfiliacion",Tipo_afiliacion )
                };
                SqlCommand updateCommand = new SqlCommand(query, sqlConnection);
                updateCommand.Parameters.AddRange(parametros.ToArray());

                updateCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
        }
    }

}