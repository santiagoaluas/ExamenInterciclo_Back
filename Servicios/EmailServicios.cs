using ExamenInterciclo_Back.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenInterciclo_Back.Servicios
{
    public class EmailServicios : IEmail
    {
        public string enviarEmailAuth(string url,string destinoEmail)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("santiagoalulema@gmail.com"));
                email.To.Add(MailboxAddress.Parse(destinoEmail));
                email.Subject = "Verificar su correo";
                email.Body = new TextPart(TextFormat.Plain) { Text = $"Ingrese al siguiente link para verificar su correo: {url}"};

                // send email

                using var smtp = new SmtpClient();
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.Connect("smtp.gmail.com", 587, false);
                smtp.Authenticate("santiagoalulema@gmail.com", "Alulema_0105784847");
                smtp.Send(email);
                smtp.Disconnect(true);
                return "";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
           

        }

        public string enviarPagosEmail(string destinoEmail, string mesaje)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("santiagoalulema@gmail.com"));
                email.To.Add(MailboxAddress.Parse(destinoEmail));
                email.Subject = "Verificar su correo";
                email.Body = new TextPart(TextFormat.Plain) { Text = mesaje };

                // send email

                using var smtp = new SmtpClient();
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.Connect("smtp.gmail.com", 587, false);
                smtp.Authenticate("santiagoalulema@gmail.com", "Alulema_0105784847");
                smtp.Send(email);
                smtp.Disconnect(true);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
