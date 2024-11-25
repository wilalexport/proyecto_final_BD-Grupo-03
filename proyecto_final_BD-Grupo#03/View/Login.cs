using proyecto_final_BD_Grupo_03.Model;
using proyecto_final_BD_Grupo_03.Presenter;
using proyecto_final_BD_Grupo_03.View.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto_final_BD_Grupo_03
{
    public partial class Login : Form, ILoginView
    {
        private Model_User modelUser;
        private presenter_User presenterUser;
        public Login()
        {
            InitializeComponent();
            presenterUser = new presenter_User(this);
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
                presenterUser.IniciarSesion();
                this.Hide(); // Cierra el formulario de inicio de sesión y elimina la instancia de la memoria
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrectos");
            }
        }

        public void MostrarMensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
