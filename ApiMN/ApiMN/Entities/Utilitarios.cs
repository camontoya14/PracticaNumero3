using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace ApiMN.Entities
{
    public class Utilitarios
    {

        public void EnvioCorreos(string destino, string asunto, string contenido)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(ConfigurationManager.AppSettings["cuentaCorreo"]);
            message.To.Add(new MailAddress(destino));
            message.Subject = asunto;
            message.Body = contenido;
            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = ConfigurationManager.AppSettings["servidorCorreo"];
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["cuentaCorreo"], ConfigurationManager.AppSettings["claveCorreo"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

    }
}