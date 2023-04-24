using System;
using System.Net;
using System.Net.Mail;

namespace EnviaEmail
{
    public static class SendEmail
    {
        public static void Send(string email, string Codigo)
        {

            MailMessage emailMessage = new MailMessage();
            try
            {
                var smtpclient = new SmtpClient("smtp.gmail.com", 587);
                smtpclient.EnableSsl = true;
                smtpclient.UseDefaultCredentials = false;
                smtpclient.Credentials = new NetworkCredential("emerson.castro.ifsp@gmail.com", "zxcnfvpheahsscyu");
                emailMessage.From = new MailAddress("emerson.castro.ifsp@gmail.com", "Emerson");
                emailMessage.Body = "Código de Ativação : "+ Codigo;
                emailMessage.Subject = "Código de Ativação - JR - BLOG";
                emailMessage.IsBodyHtml = true;
                emailMessage.Priority = MailPriority.Normal;
                emailMessage.To.Add(email);

                smtpclient.Send(emailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}