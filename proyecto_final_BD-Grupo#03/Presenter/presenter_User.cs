using proyecto_final_BD_Grupo_03.Model;
using proyecto_final_BD_Grupo_03.View.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static proyecto_final_BD_Grupo_03.Model.Usuario;

namespace proyecto_final_BD_Grupo_03.Presenter
{
    internal class presenter_User
    {
        public Model_User _modelUser;
        private readonly ILoginView _Loginvista;

        presenter_User()
        {
            _modelUser = new Model_User();
        }

        public presenter_User(ILoginView vistaLogin) : this() // Llama al constructor por defecto
        {
            _Loginvista = vistaLogin;
        }

        public void IniciarSesion()
        {
            try
            {
                string nombreUsuario = _Loginvista.NombreUsuario;
                string contrasena = _Loginvista.Contrasena;

                if (_modelUser.VerificarCredenciales(nombreUsuario, contrasena))
                {
                    Usuario sesionUsuario = Model_User.ObtenerDatosEmpleadoLogueado(nombreUsuario);

                    if (sesionUsuario != null)
                    {
                        int idLogueado = sesionUsuario.id_usuario;
                        string nombreEmpleado = sesionUsuario.name;
                        string usernameEmpleado = sesionUsuario.username;
                        string emailEmpleado = sesionUsuario.email;
                        int idRol = sesionUsuario.id_rol;
                        int idDepartamento = sesionUsuario.id_departamento;

                        FormMain formMain = new FormMain(idLogueado, nombreEmpleado, usernameEmpleado, emailEmpleado, idRol, idDepartamento);

                        formMain.Show();
                    }
                    else
                    {
                        _Loginvista.MostrarMensajeError("No se encontraron datos para el usuario logueado.");
                    }
                }
                else
                {
                    _Loginvista.MostrarMensajeError("Nombre de usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                string mensajeError = $"Se produjo un error al intentar iniciar sesión. Por favor, inténtelo de nuevo más tarde.\n{ex.Message}\n{ex.StackTrace}";
                _Loginvista.MostrarMensajeError(mensajeError);
            }
        }

    }




}
