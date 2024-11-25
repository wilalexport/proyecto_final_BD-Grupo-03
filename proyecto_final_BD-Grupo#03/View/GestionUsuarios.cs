using proyecto_final_BD_Grupo_03.Model;
using proyecto_final_BD_Grupo_03.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto_final_BD_Grupo_03.View
{
    public partial class GestionUsuarios : Form
    {
        private presenter_GestionUsers _presenter;
        public GestionUsuarios(int idusuario, string nombre, string apellido)
        {
            InitializeComponent();

        }

        public GestionUsuarios()
        {
            InitializeComponent();
            txtIdUsuario.Visible = false;
            label5.Visible = false;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
           // string nombre = txt.user;

           // Usuario usuario = new Usuario();{
           //     usuario.Nombre = nombre;
           // }
           // return Usuario

           //bool validar = _presenter.ValidarCamposGestionUsuarios(nombre);
           // if (validar)
           // {
           //     _presenter.InsertarUsuario();
           // }

        }

        private void GestionUsuarios_Load(object sender, EventArgs e)
        {

        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
