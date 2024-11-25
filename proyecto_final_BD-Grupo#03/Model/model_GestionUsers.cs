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
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public int IdDepartamento { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    // Modelo/UsuarioCRUD.cs
    public class model_GestionUsers
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "SELECT id_usuario, id_rol, id_departamento, name, username, password, email FROM Usuarios";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        conexion.Open();
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Usuario usuario = new Usuario
                                {
                                    IdUsuario = reader.GetInt32(reader.GetOrdinal("id_usuario")),
                                    IdRol = reader.GetInt32(reader.GetOrdinal("id_rol")),
                                    IdDepartamento = reader.GetInt32(reader.GetOrdinal("id_departamento")),
                                    Name = reader.GetString(reader.GetOrdinal("name")),
                                    Username = reader.GetString(reader.GetOrdinal("username")),
                                    Password = reader.GetString(reader.GetOrdinal("password")),
                                    Email = reader.GetString(reader.GetOrdinal("email"))
                                };
                                usuarios.Add(usuario);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Se produjo un error al acceder a la base de datos: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error inesperado: " + ex.Message);
            }

            return usuarios;
        }

        public bool InsertarUsuario(Usuario usuario)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "INSERT INTO Usuarios (id_rol, id_departamento, name, username, password, email) " +
                                      "VALUES (@IdRol, @IdDepartamento, @Name, @Username, @Password, @Email)";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdRol", usuario.IdRol);
                        comando.Parameters.AddWithValue("@IdDepartamento", usuario.IdDepartamento);
                        comando.Parameters.AddWithValue("@Name", usuario.Name);
                        comando.Parameters.AddWithValue("@Username", usuario.Username);
                        comando.Parameters.AddWithValue("@Password", usuario.Password);
                        comando.Parameters.AddWithValue("@Email", usuario.Email);

                        conexion.Open();
                        int rowsAffected = comando.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar el usuario: " + ex.Message);
                return false;
            }
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "UPDATE Usuarios SET id_rol = @IdRol, id_departamento = @IdDepartamento, name = @Name, username = @Username, password = @Password, email = @Email " +
                                      "WHERE id_usuario = @IdUsuario";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                        comando.Parameters.AddWithValue("@IdRol", usuario.IdRol);
                        comando.Parameters.AddWithValue("@IdDepartamento", usuario.IdDepartamento);
                        comando.Parameters.AddWithValue("@Name", usuario.Name);
                        comando.Parameters.AddWithValue("@Username", usuario.Username);
                        comando.Parameters.AddWithValue("@Password", usuario.Password);
                        comando.Parameters.AddWithValue("@Email", usuario.Email);

                        conexion.Open();
                        comando.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el usuario: " + ex.Message);
                return false;
            }
        }

        public bool EliminarUsuario(int idUsuario)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string consulta = "DELETE FROM Usuarios WHERE id_usuario = @IdUsuario";

                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        conexion.Open();
                        int rowsAffected = comando.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el usuario: " + ex.Message);
                return false;
            }
        }
    }
}
