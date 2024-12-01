using SahredLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IEmailService
    {
        public Task<bool> SendMail(string toEmail, string subject, string body);
    }
}
