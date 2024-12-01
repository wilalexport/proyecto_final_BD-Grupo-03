using proyecto_final_BD_Grupo_03.Model;
using proyecto_final_BD_Grupo_03.Presenter;
using proyecto_final_BD_Grupo_03.View;
using proyecto_final_BD_Grupo_03.View.Interface;
using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static proyecto_final_BD_Grupo_03.Model.Model_User;

namespace proyecto_final_BD_Grupo_03
{
    public partial class Login : Form, ILoginView
    {
        private Model_User modelUser;
        private presenter_User presenterUser;
        private string generatedCode;
        private AuthCodeRepository authCodeRepo;

        public Login()
        {
            InitializeComponent();
            presenterUser = new presenter_User(this);
            authCodeRepo = new AuthCodeRepository();
        }

        private bool mostrarPassword = false;

        public string NombreUsuario => txtUser.Text;
        public string Contrasena => txtPass.Text;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblPassEye_Click(object sender, EventArgs e)
        {
            if (!mostrarPassword)
            {
                txtPass.UseSystemPasswordChar = false;
                txtPass.Text = txtPass.Text;
                mostrarPassword = true;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
                txtPass.Text = txtPass.Text;
                mostrarPassword = false;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnSession_Click(object sender, EventArgs e)
        {
            // Asegúrate de que modelUser esté inicializado
            if (modelUser == null)
            {
                modelUser = new Model_User();
            }

            string nombreUsuario = txtUser.Text;
            string contrasena = txtPass.Text;

            // Verificar las credenciales ingresadas
            if (modelUser.VerificarCredenciales(nombreUsuario, contrasena))
            {
                // Generar y enviar el código al correo del usuario
                Usuario sesionUsuario = Model_User.ObtenerDatosEmpleadoLogueado(nombreUsuario);
                if (sesionUsuario != null)
                {
                    generatedCode = Utilities.Utilities.GenerateCode();
                    Utilities.Utilities.SendCodeToEmail(sesionUsuario.email, generatedCode);

                    // Insertar el código de autenticación en la base de datos
                    AuthCode authCode = new AuthCode
                    {
                        IdUsuario = sesionUsuario.id_usuario,
                        Codigo = generatedCode,
                        FechaHoraCambio = DateTime.Now
                    };
                    authCodeRepo.InsertAuthCode(authCode);

                    // Abrir el formulario para ingresar el código
                    using (Auth formCodigo = new Auth())
                    {
                        if (formCodigo.ShowDialog() == DialogResult.OK)
                        {
                            // Verificar el código ingresado
                            if (authCodeRepo.IsAuthCodeValid(sesionUsuario.id_usuario, formCodigo.CodigoIngresado))
                            {
                                presenterUser.IniciarSesion();
                                this.Hide(); // Cierra el formulario de inicio de sesión y elimina la instancia de la memoria
                                FormMain formMain = new FormMain();
                                formMain.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                MostrarMensajeError("Código incorrecto o ha expirado. Inténtelo de nuevo.");
                            }
                        }
                    }
                }
                else
                {
                    MostrarMensajeError("No se encontraron datos para el usuario logueado.");
                }
            }
            else
            {
                MostrarMensajeError("Nombre de usuario o contraseña incorrectos");
            }
        }

        public void MostrarMensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
