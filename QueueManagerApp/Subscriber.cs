using System;
using System.Net;
using System.Net.Mail;

class Subscriber
{
    private string smtpServer;
    private int port;
    private string username;
    private string password;

    public Subscriber(string smtp, int port, string user, string pass)
    {
        smtpServer = smtp;
        this.port = port;
        username = user;
        password = pass;
    }

    public void SendEmail(string message)
    {
        try
        {
            MailMessage mail = new MailMessage(username, "destinatario@ejemplo.com");
            SmtpClient client = new SmtpClient(smtpServer, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            mail.Subject = "Nuevo Mensaje de la Cola";
            mail.Body = message;

            client.Send(mail);
            Console.WriteLine("Correo enviado con éxito.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al enviar el correo: " + ex.Message);
        }
    }
}
