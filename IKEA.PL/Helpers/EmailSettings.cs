using IKEA.DAL.Models.Auth;
using System.Net;
using System.Net.Mail;

namespace IKEA.PL.Helpers;

public class EmailSettings
{
    public static void SendEmail(Email email)
    {
        var client = new SmtpClient("smtp.gmail.com", 587);
        client.EnableSsl = true;
        client.Credentials = new NetworkCredential("saadmahmoud11297@gmail.com", "nlvpzqfrfdbpbhzu"); // app password
        client.Send("saadmahmoud11297@gmail.com", email.To, email.Subject, email.Body);
    }
}
