using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeApp.DTO
{
    public class MailDTO
    {
        public string TO { get; set; }
        public string CC { get; set; }
        public string  FromMail { get; set; }
        public string  Subject { get; set; }
        public string Message { get; set; }
        //public string Name { get; set; }
    }
}
