namespace NexusPilot_Auth_Service.Models.ExceptionModels
{
    public class AuthException: Exception
    {
        public AuthException(string message): base(message) { }
    }
}
