using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto_final_BD_Grupo_03.View.Interface
{
    public interface InterfaceCrud
    {
        DataGridView DataGridViewCRUD { get; }
        void MostrarMensaje(string mensaje, string v, MessageBoxButtons oK, MessageBoxIcon information);
    }
}
