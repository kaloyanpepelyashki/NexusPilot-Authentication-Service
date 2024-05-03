using NexusPilot_Auth_Service.Models;

namespace NexusPilot_Auth_Service.Services.Intefaces
{
    public interface IAuthService
    {
        public Task<(bool isSuccess, UserAccount userAccount)> SignIn(string email, string password);
        public Task<(bool isSuccess, UserAccount userAccount)> SignUp(string nickName, string bio, string role, string email, string password);
    }
}
