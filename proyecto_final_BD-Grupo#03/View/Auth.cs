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
    public partial class Auth : Form,Interface.InterfaceCrud
    {
        public string CodigoIngresado { get; private set; }
        public Auth()
        {
            InitializeComponent();
        }

        public DataGridView DataGridViewCRUD => throw new NotImplementedException();

        public void MostrarMensaje(string mensaje, string v, MessageBoxButtons oK, MessageBoxIcon information)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void Auth_Load(object sender, EventArgs e)
        {

        }

        private void btnVerificar_Click_1(object sender, EventArgs e)
        {
            CodigoIngresado = txtCodigo.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
