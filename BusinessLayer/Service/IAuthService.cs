using SahredLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
        public Task<(int, string)> ForgotPassword(string email);
        public Task<(int, string)> ResetPassword(Resetmodel resetmodel);
    }
}
