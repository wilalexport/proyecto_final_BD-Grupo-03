using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace proyecto_final_BD_Grupo_03.Utilities
{
    internal class Utilities
    {

        public static void BorderRadius(Panel panel, int radio)
        {
            // Suscribirse al evento SizeChanged del panel
            panel.SizeChanged += (sender, e) => ResizePanel(sender as Panel, radio);

            // Llamar al método de redimensionamiento una vez para aplicar el redondeado inicialmente
            ResizePanel(panel, radio);
        }

        private static void ResizePanel(Panel panel, int radio)
        {
            // Crear un rectángulo con las dimensiones del panel
            GraphicsPath forma = new GraphicsPath();
            forma.AddArc(0, 0, radio * 2, radio * 2, 180, 90);
            forma.AddArc(panel.Width - (radio * 2), 0, radio * 2, radio * 2, 270, 90);
            forma.AddArc(panel.Width - (radio * 2), panel.Height - (radio * 2), radio * 2, radio * 2, 0, 90);
            forma.AddArc(0, panel.Height - (radio * 2), radio * 2, radio * 2, 90, 90);
            forma.CloseFigure();

            // Aplicar la forma al panel
            panel.Region = new Region(forma);
        }

        public static void AjustarOpacidad(Panel panel)
        {
            if (panel != null)
            {
                panel.BackColor = Color.FromArgb(120, panel.BackColor);
            }
            return;
        }

        public static string GenerateCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Genera un código de 6 dígitos
        }

        public static void SendCodeToEmail(string email, string code)
        {
            try
            {
                MailMessage mail = new MailMessage("juliomix089@gmail.com", email);
                SmtpClient client = new SmtpClient
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = new NetworkCredential("juliomix089@gmail.com", "kazp oifl axto vykq")
                };
                mail.Subject = "Código de Verificación";
                mail.IsBodyHtml = true;
                mail.Body = $@"
            <!DOCTYPE html>
            <html lang='es'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Código de Verificación</title>
            </head>
            <body style='margin: 0; padding: 0; font-family: Arial, sans-serif; background-color: #f4f4f4;'>
                <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px; margin: 0 auto; background-color: #ffffff;'>
                    <!-- Header Panel -->
                    <tr>
                        <td style='padding: 40px 30px; background-color: #0056b3; text-align: center;'>
                            <h1 style='color: #ffffff; margin: 0;'>Código de Verificación</h1>
                        </td>
                    </tr>
                    
                    <!-- Main Content Panel -->
                    <tr>
                        <td style='padding: 40px 30px;'>
                            <p style='font-size: 16px; line-height: 24px; margin: 0 0 20px;'>Estimado usuario,</p>
                            <p style='font-size: 16px; line-height: 24px; margin: 0 0 20px;'>Su código de verificación es:</p>
                            <div style='background-color: #f0f0f0; border: 2px solid #dddddd; border-radius: 5px; padding: 20px; text-align: center; margin-bottom: 20px;'>
                                <h2 style='font-size: 36px; color: #0056b3; margin: 0;'>{code}</h2>
                            </div>
                            <p style='font-size: 16px; line-height: 24px; margin: 0 0 20px;'>Por favor, use este código para completar su verificación.</p>
                        </td>
                    </tr>
                    
                    <!-- Instructions Panel -->
                    <tr>
                        <td style='padding: 30px; background-color: #f9f9f9; border-top: 1px solid #dddddd;'>
                            <h3 style='color: #333333; margin: 0 0 20px;'>Instrucciones:</h3>
                            <ol style='margin: 0; padding-left: 20px;'>
                                <li style='margin-bottom: 10px;'>Vuelva al panel de código</li>
                                <li style='margin-bottom: 10px;'>Introduzca el código en el apartado correspondiente</li>
                                <li style='margin-bottom: 10px;'>Haga clic en ""Verificar""</li>
                            </ol>
                        </td>
                    </tr>
                    
                    <!-- Footer Panel -->
                    <tr>
                        <td style='padding: 30px; background-color: #eeeeee; text-align: center; font-size: 14px; color: #666666;'>
                            <p style='margin: 0 0 10px;'>Este es un correo electrónico automático. Por favor, no responda.</p>
                            <p style='margin: 0;'>&copy; 2023 CASIVAR. Todos los derechos reservados.</p>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";
                client.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar el correo: {ex.Message}");
            }
        }

    }

    public static class Validaciones
    {

        public static bool ValidarNombre(string nombre, Action<string> mostrarMensaje, string nombreCamp)
        {
            if (string.IsNullOrWhiteSpace(nombre) || nombre.Any(char.IsDigit))
            {
                mostrarMensaje($"El campo {nombreCamp} es inválido. No debe contener números ni estar vacío.");
                return false;
            }
            return true;
        }

        public static bool ValidarNoVacio(string valor, Action<string> mostrarMensaje, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                mostrarMensaje($"El campo '{nombreCampo}' no puede estar vacío.");
                return false;
            }
            return true;
        }

        public static bool ValidarId(string id, Action<string> mostrarMensaje)
        {
            if (string.IsNullOrWhiteSpace(id) || !int.TryParse(id, out int idValor) || idValor <= 0)
            {
                mostrarMensaje("El campo 'ID' es inválido o negativo.");
                return false;
            }
            return true;
        }

        public static bool ValidarFecha(string fecha, Action<string> mostrarMensaje)
        {
            if (string.IsNullOrWhiteSpace(fecha) || !DateTime.TryParse(fecha, out _))
            {
                mostrarMensaje("El campo 'Fecha' es inválido.");
                return false;
            }
            return true;
        }

        public static string ValidarFechas(string fechaInicio, string fechaFin)
        {
            if (!DateTime.TryParse(fechaInicio, out DateTime inicio) || !DateTime.TryParse(fechaFin, out DateTime fin))
            {
                return "Las fechas tienen un formato inválido.";
            }

            if (inicio > fin)
            {
                return "La fecha de inicio no puede ser mayor que la fecha de fin.";
            }
            if (fin < inicio)
            {
                return "La fecha fin no puede ser menor que la fecha de inicio.";
            }

            return null; // Si no hay errores, devuelve null
        }

        public static bool ValidarDecimal(string valor, Action<string> mostrarMensaje, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(valor) || !decimal.TryParse(valor, out _))
            {
                mostrarMensaje($"El campo '{nombreCampo}' es inválido.");
                return false;
            }
            return true;
        }

        public static bool ValidarTelefono(string telefono, Action<string> mostrarMensaje)
        {
            // Patrón para validar números de teléfono de 8 dígitos
            string patronTelefono = @"^\d{8}$";

            if (string.IsNullOrWhiteSpace(telefono) || !Regex.IsMatch(telefono, patronTelefono))
            {
                mostrarMensaje("El campo 'Teléfono' es inválido. Debe tener 8 dígitos.");
                return false;
            }
            return true;
        }

        public static bool ValidarHora(string hora, Action<string> mostrarMensaje, string nombreCamp)
        {
            // Patrón para validar una hora en formato HH:mm:ss
            string patronHora = @"^([01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$";

            if (string.IsNullOrWhiteSpace(hora))
            {
                mostrarMensaje($"El campo {nombreCamp} está vacío.");
                return false;
            }
            else if (!Regex.IsMatch(hora, patronHora))
            {
                mostrarMensaje($"El campo {nombreCamp} no tiene el formato correcto (HH:mm:ss).");
                return false;
            }
            else
            {
                return true;
            }
        }



    }
}
