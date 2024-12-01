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

namespace proyecto_final_BD_Grupo_03.View
{
    public partial class formGestionUsers : Form, InterfaceCrud
    {
        private presenter_GestionUsers _presenter;
        public formGestionUsers()
        {
            InitializeComponent();
            _presenter = new presenter_GestionUsers(this);
            _presenter.MostrarUsuarios(dgvUsuarios);
        }
        public void MostrarMensaje(string mensaje, string titulo, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, botones, icono);
        }

        public DataGridView DataGridViewCRUD => dgvUsuarios;

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsuarios.Rows[e.RowIndex];
                txtIdUsuario.Text = row.Cells["idUsuario"].Value.ToString();
                txtIdRol.Text = row.Cells["idRol"].Value.ToString();
                txtIdDepartamento.Text = row.Cells["idDepartamento"].Value.ToString();
                txtNombre.Text = row.Cells["name"].Value.ToString();
                txtUsuario.Text = row.Cells["username"].Value.ToString();
                txtContraseña.Text = row.Cells["password"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();
            }
        }


        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (_presenter.ValidarCamposGestionUsuarios(txtIdRol.Text, txtIdDepartamento.Text, txtNombre.Text, txtUsuario.Text, txtContraseña.Text, txtEmail.Text))
            {
                Usuario usuario = new Usuario
                {
                    id_rol = int.Parse(txtIdRol.Text),
                    id_departamento = int.Parse(txtIdDepartamento.Text),
                    name = txtNombre.Text,
                    username = txtUsuario.Text,
                    password = txtContraseña.Text,
                    email = txtEmail.Text
                };
                _presenter.InsertarUsuario(usuario);
            }
        }

        private void btnActu_Click(object sender, EventArgs e)
        {
            if (_presenter.ValidarCamposGestionUsuarios(txtIdRol.Text, txtIdDepartamento.Text, txtNombre.Text, txtUsuario.Text, txtContraseña.Text, txtEmail.Text))
            {
                Usuario usuario = new Usuario
                {
                    id_usuario = int.Parse(txtIdUsuario.Text),
                    id_rol = int.Parse(txtIdRol.Text),
                    id_departamento = int.Parse(txtIdDepartamento.Text),
                    name = txtNombre.Text,
                    username = txtUsuario.Text,
                    password = txtContraseña.Text,
                    email = txtEmail.Text
                };
                _presenter.ActualizarUsuario(usuario);
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            int idUsuario;
            if (int.TryParse(txtIdUsuario.Text, out idUsuario))
            {
                _presenter.EliminarUsuario(idUsuario);
            }
            else
            {
                MostrarMensaje("Por favor, ingrese un ID de usuario válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
                // Verificar si hay una fila seleccionada
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                 DataGridViewRow filaSeleccionada = dgvUsuarios.SelectedRows[0];

                // Verificar si la fila seleccionada no está vacía
            if (!filaSeleccionada.IsNewRow && filaSeleccionada.Cells.Cast<DataGridViewCell>().All(cell => cell.Value != null))
            {
                txtIdUsuario.Text = filaSeleccionada.Cells["idUsuario"].Value.ToString();
                txtIdRol.Text = filaSeleccionada.Cells["idRol"].Value.ToString();
                txtIdDepartamento.Text = filaSeleccionada.Cells["idDepartamento"].Value.ToString();
                txtNombre.Text = filaSeleccionada.Cells["name"].Value.ToString();
                txtUsuario.Text = filaSeleccionada.Cells["username"].Value.ToString();
                txtContraseña.Text = filaSeleccionada.Cells["password"].Value.ToString();
                txtEmail.Text = filaSeleccionada.Cells["email"].Value.ToString();
            
            }

            else
                {
                 LimpiarCampos();
                }
            }
        }

        
        private void LimpiarCampos()
        {
            txtIdUsuario.Text = "";
            txtNombre.Text = "";
            txtEmail.Text = "";
            txtIdRol.Text = "";
            txtIdDepartamento.Text = "";
            txtContraseña.Text = "";
            txtUsuario.Text = "";
        }

    }
}
