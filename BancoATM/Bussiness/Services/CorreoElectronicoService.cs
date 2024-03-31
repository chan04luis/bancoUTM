using System;
using System.Net;
using System.Net.Mail;

public class CorreoElectronicoService : ICorreoElectronicoService
{
    private readonly string _servidorSmtp = "mail.gownetwork.com.mx"; // Cambia esto por tu servidor SMTP
    private readonly int _puertoSmtp = 587; // Cambia esto por el puerto SMTP correspondiente
    private readonly string _correoRemitente = "luischanvalle@gownetwork.com.mx"; // Cambia esto por tu dirección de correo electrónico
    private readonly string _contrasenaRemitente = "zn)pH}&m26wR"; // Cambia esto por tu contraseña de correo electrónico

    public void EnviarCorreo(string destinatario, string asunto, string cuerpo)
    {
        try
        {
            using (var clienteSmtp = new SmtpClient(_servidorSmtp, _puertoSmtp))
            {
                clienteSmtp.UseDefaultCredentials = false;
                clienteSmtp.Credentials = new NetworkCredential(_correoRemitente, _contrasenaRemitente);
                clienteSmtp.EnableSsl = true;

                using (var mensaje = new MailMessage(_correoRemitente, destinatario))
                {
                    mensaje.Subject = asunto;
                    mensaje.Body = cuerpo;
                    mensaje.IsBodyHtml = true;

                    clienteSmtp.Send(mensaje);
                }
            }
        }catch(Exception ex) { }
    }
}
