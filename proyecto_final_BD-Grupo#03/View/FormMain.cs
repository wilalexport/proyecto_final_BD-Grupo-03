using proyecto_final_BD_Grupo_03.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using proyecto_final_BD_Grupo_03.Utilities;

namespace proyecto_final_BD_Grupo_03
{
    public partial class FormMain : Form
    {
        public Utilities.Utilities _utilities;
        public FormMain()
        {
            InitializeComponent();
            _utilities = new Utilities.Utilities();
            Utilities.Utilities.BorderRadius(panelChild, 10);
        }
        public FormMain(int idLogueado, string nombreEmpleado, string usernameEmpleado, string emailEmpleado, int idRol, int idDepartamento)
        {
            _utilities = new Utilities.Utilities();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            home form1 = new home();
            _utilities.ShowOrOpenFormInPanel(form1, "home", panelChild);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formGestionUsers form1 = new formGestionUsers();
            _utilities.ShowOrOpenFormInPanel(form1, "formGestionUsers", panelChild);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void panelChild_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
