using EmployeeApp.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace EmployeeApp.DataObjects.Repository
{
    public class MailRepository
    {
        private readonly DataContext _context;

        public void SendEmail(MailDTO obj)
        {
            string fromaddr = "jrkurtvonnegut@gmail.com";
            string password = "demodemo";

            MailMessage msg = new MailMessage();
            msg.Subject = obj.Subject;
            msg.From = new MailAddress("sadeeshkanna@gmail.com");
            msg.To.Add(obj.TO);
            //msg.To.Add(new MailAddress("jrkurtvonnegut@gmail.com"));
            //msg.To.Add(new MailAddress(objEmployee.EmailID));
            string body = obj.Message;
            msg.Body = body;
            msg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential(fromaddr, password);
            smtp.Credentials = nc;
            smtp.Send(msg);
        }
    }
}
