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

namespace proyecto_final_BD_Grupo_03
{
    public partial class Form1 : Form
    {
        string nombre = "Wilson";
        string apellido = "Gonzalez";
        int idusuario = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GestionUsuarios gesti = new GestionUsuarios();
            gesti.Show();
        }


    }
}
