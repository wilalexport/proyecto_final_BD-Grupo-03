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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private bool mostrarPassword = false;

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
    }
}
