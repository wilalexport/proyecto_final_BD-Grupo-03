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
    public class Model_User
    {
        // Método para verificar las credenciales del usuario
        public bool VerificarCredenciales(string nombreUsuario, string contrasena)
        {
            try
            {
                // Obtener la cadena de conexión desde app.config
                string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(connectionString))
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


    }
}
