using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_final_BD_Grupo_03.View.Interface
{
    internal interface ILoginView
    {
        string NombreUsuario { get; }
        string Contrasena { get; }

        void MostrarMensajeError(string mensaje);
    }
}
