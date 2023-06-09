using System;
using System.Net;
using System.Net.Mail;

namespace EnviaEmail
{
    public static class SendEmail
    {
        public static void Send(string email, string Codigo, int TipoUser, string Tipo)
        {

            MailMessage emailMessage = new MailMessage();
            try
            {
                var smtpclient = new SmtpClient("smtp.gmail.com", 587);
                smtpclient.EnableSsl = true;
                smtpclient.UseDefaultCredentials = false;
                smtpclient.Credentials = new NetworkCredential("emerson.castro.ifsp@gmail.com", "zxcnfvpheahsscyu");
                emailMessage.From = new MailAddress("emerson.castro.ifsp@gmail.com", "Emerson");
                if (TipoUser == 1)
                {
                    if (Tipo == "Reenvio-Código")
                    {
                        emailMessage.Body = "Código de Ativação : " + Codigo;
                        emailMessage.Subject = "Código de Ativação - JR - BLOG";
                    }
                    else if (Tipo == "Redefinicao-Senha")
                    {
                        emailMessage.Body = "Código de Redefinição de senha : " + Codigo;
                        emailMessage.Subject = "Redefinição de senha - JR - BLOG";
                    }
                    else if (Tipo == "Redefinicao-Email")
                    {
                        emailMessage.Body = "Código de Redefinição de Email : " + Codigo;
                        emailMessage.Subject = "Redefinição de Email - JR - BLOG";
                    }
                }
                else
                {
                    if (Tipo == "CadastroAutor")
                    {
                        emailMessage.Body = "Senha do Primeiro Acesso : " + Codigo;
                        emailMessage.Subject = "Senha do Primeiro Acesso - JR - BLOG";
                    }
                    else if (Tipo == "Redefinicao-Senha")
                    {
                        emailMessage.Body = "Código de Redefinição de senha : " + Codigo;
                        emailMessage.Subject = "Redefinição de senha - JR - BLOG";
                    }
                    else if (Tipo == "Redefinicao-Email")
                    {
                        emailMessage.Body = "Código de Redefinição de Email : " + Codigo;
                        emailMessage.Subject = "Redefinição de Email - JR - BLOG";
                    }
                }
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