using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace proyecto_final_BD_Grupo_03.Model
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public int id_rol { get; set; }
        public int id_departamento { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }

    public class AuthCode
    {
        public int IdAutenticacion { get; set; }
        public int IdUsuario { get; set; }
        public string Codigo { get; set; }
        public DateTime FechaHoraCambio { get; set; }
    }

    public class Model_User
    {
        private readonly string _connectionString;

        // Constructor sin parámetros que obtiene la cadena de conexión desde app.config
        public Model_User()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        }

        // Constructor que acepta una cadena de conexión como parámetro
        public Model_User(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Método para verificar las credenciales del usuario
        public bool VerificarCredenciales(string nombreUsuario, string contrasena)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    // Abrir la conexión
                    conexion.Open();

                    // Consulta SQL para verificar las credenciales
                    string consulta = "SELECT COUNT(*) FROM Usuarios WHERE username = @NombreUsuario AND password = @Contrasena";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        // Añadir parámetros para evitar inyección SQL
                        comando.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                        comando.Parameters.AddWithValue("@Contrasena", contrasena);

                        // Ejecutar la consulta y obtener el resultado
                        int cantidadFilas = (int)comando.ExecuteScalar();

                        // La consulta debe devolver exactamente 1 fila si las credenciales son correctas
                        return cantidadFilas == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir
                MessageBox.Show("Error al verificar las credenciales: " + ex.Message);
                return false;
            }
        }

        public static Usuario ObtenerDatosEmpleadoLogueado(string nombreUsuario)
        {
            Usuario sesionUsuario = null;

            // Obtener la cadena de conexión desde app.config
            string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                // Consulta SQL para obtener los datos del usuario asociados al nombre de usuario
                string consulta = "SELECT id_usuario, id_rol, id_departamento, name, username, password, email " +
                                  "FROM Usuarios " +
                                  "WHERE username = @NombreUsuario";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

                    // Abrir la conexión
                    conexion.Open();

                    // Ejecutar la consulta y leer los resultados
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Asignar los valores leídos a las propiedades del objeto Usuario
                            sesionUsuario = new Usuario
                            {
                                id_usuario = reader.GetInt32(reader.GetOrdinal("id_usuario")),
                                id_rol = reader.GetInt32(reader.GetOrdinal("id_rol")),
                                id_departamento = reader.GetInt32(reader.GetOrdinal("id_departamento")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                username = reader.GetString(reader.GetOrdinal("username")),
                                password = reader.GetString(reader.GetOrdinal("password")),
                                email = reader.GetString(reader.GetOrdinal("email"))
                            };
                        }
                    }
                }
            }
            return sesionUsuario;
        }


       

        public class AuthCodeRepository
        {
            private readonly string _connectionString;

            public AuthCodeRepository()
            {
                _connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
            }

            public AuthCodeRepository(string connectionString)
            {
                _connectionString = connectionString;
            }

            public void InsertAuthCode(AuthCode authCode)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Autenticacion (id_usuario, codigo, fecha_hora_cambio) VALUES (@IdUsuario, @Codigo, @FechaHoraCambio)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdUsuario", authCode.IdUsuario);
                    command.Parameters.AddWithValue("@Codigo", authCode.Codigo);
                    command.Parameters.AddWithValue("@FechaHoraCambio", authCode.FechaHoraCambio);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            public void UpdateAuthCode(AuthCode authCode)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Autenticacion SET id_usuario = @IdUsuario, codigo = @Codigo, fecha_hora_cambio = @FechaHoraCambio WHERE id_autenticacion = @IdAutenticacion";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdAutenticacion", authCode.IdAutenticacion);
                    command.Parameters.AddWithValue("@IdUsuario", authCode.IdUsuario);
                    command.Parameters.AddWithValue("@Codigo", authCode.Codigo);
                    command.Parameters.AddWithValue("@FechaHoraCambio", authCode.FechaHoraCambio);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            public AuthCode GetAuthCode(int idUsuario, string codigo)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT id_autenticacion, id_usuario, codigo, fecha_hora_cambio FROM Autenticacion WHERE id_usuario = @IdUsuario AND codigo = @Codigo";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    command.Parameters.AddWithValue("@Codigo", codigo);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new AuthCode
                            {
                                IdAutenticacion = (int)reader["id_autenticacion"],
                                IdUsuario = (int)reader["id_usuario"],
                                Codigo = (string)reader["codigo"],
                                FechaHoraCambio = (DateTime)reader["fecha_hora_cambio"]
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }

            public bool IsAuthCodeValid(int idUsuario, string codigo)
            {
                AuthCode authCode = GetAuthCode(idUsuario, codigo);
                if (authCode != null)
                {
                    TimeSpan timeElapsed = DateTime.Now - authCode.FechaHoraCambio;
                    return timeElapsed.TotalSeconds <= 30;
                }
                return false;
            }
        }
    }
}
