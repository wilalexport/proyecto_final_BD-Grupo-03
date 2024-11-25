using proyecto_final_BD_Grupo_03.Model;
using proyecto_final_BD_Grupo_03.Utilities;
using proyecto_final_BD_Grupo_03.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto_final_BD_Grupo_03.Presenter
{
    public class presenter_GestionUsers
    {

        private model_GestionUsers _model;
        private  InterfaceCrud _InterCrud;

        public presenter_GestionUsers(InterfaceCrud vistaHorario) : this() // Llama al constructor por defecto
        {
            _InterCrud = vistaHorario;
        }

        presenter_GestionUsers()
        {
            
        }

        public bool ValidarCamposGestionUsuarios( string idRol, string idDepartamento, string nombre, string username, string password, string email)
        {
            bool idRolValido = Validaciones.ValidarId(idRol, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error));
            bool idDepartamentoValido = Validaciones.ValidarId(idDepartamento, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error));
            bool nombreValido = Validaciones.ValidarNombre(nombre, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Nombre");
            bool usernameValido = Validaciones.ValidarNoVacio(username, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Nombre de Usuario");
            bool passwordValido = Validaciones.ValidarNoVacio(password, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error), "Contraseña");
            bool emailValido = Validaciones.ValidarNoVacio(email, mensaje => _InterCrud.MostrarMensaje(mensaje, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error),"Email");

            return idRolValido && idDepartamentoValido && nombreValido && usernameValido && passwordValido && emailValido;
        }

        public void MostrarUsuarios(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            List<Usuario> usuarios = _model.ObtenerUsuarios();

            if (dataGridView.Columns.Count == 0)
            {
                dataGridView.Columns.Add("idUsuario", "ID Usuario");
                dataGridView.Columns.Add("idRol", "ID Rol");
                dataGridView.Columns.Add("idDepartamento", "ID Departamento");
                dataGridView.Columns.Add("name", "Nombre");
                dataGridView.Columns.Add("username", "Nombre de Usuario");
                dataGridView.Columns.Add("password", "Contraseña");
                dataGridView.Columns.Add("email", "Correo Electrónico");
            }

            foreach (var usuario in usuarios)
            {
                dataGridView.Rows.Add(usuario.IdUsuario, usuario.IdRol, usuario.IdDepartamento, usuario.Name, usuario.Username, usuario.Password, usuario.Email);
            }
        }

        public void InsertarUsuario(Usuario usuario)
        {
            bool resultado = _model.InsertarUsuario(usuario);
            if (resultado)
            {
                _InterCrud.MostrarMensaje("¡El usuario se ha insertado correctamente!", "Inserción exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarUsuarios(_InterCrud.DataGridViewCRUD);
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al insertar el usuario!", "Error al insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            bool resultado = _model.ActualizarUsuario(usuario);
            if (resultado)
            {
                _InterCrud.MostrarMensaje("¡El usuario se ha actualizado correctamente!", "Actualización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarUsuarios(_InterCrud.DataGridViewCRUD);
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al actualizar el usuario!", "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EliminarUsuario(int idUsuario)
        {
            bool resultado = _model.EliminarUsuario(idUsuario);
            if (resultado)
            {
                _InterCrud.MostrarMensaje("¡El usuario se ha eliminado correctamente!", "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarUsuarios(_InterCrud.DataGridViewCRUD);
            }
            else
            {
                _InterCrud.MostrarMensaje("¡Error al eliminar el usuario!", "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
