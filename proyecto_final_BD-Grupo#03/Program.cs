using proyecto_final_BD_Grupo_03.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto_final_BD_Grupo_03
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
<<<<<<< HEAD
            Application.Run(new formGestionUsers());
=======
            Application.Run(new Login());
>>>>>>> 664af201b9f5558e6d61fbf327e3072dc8f0248d
        }
    }
}
