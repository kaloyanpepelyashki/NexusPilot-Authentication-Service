using NexusPilot_Auth_Service.DAO;
using NexusPilot_Auth_Service.Models;
using NexusPilot_Auth_Service.Models.ExceptionModels;
using Supabase.Gotrue;

namespace NexusPilot_Auth_Service.Services
{
    /*This class is the entry point to handle all authentication operations. Provides methods for sign-in and sign-up of users*/
    public class AuthService
    {
        private static AuthService _instance;
        protected SupabaseClient supabaseClient;
        protected Supabase.Client supabase;

        protected AuthService()
        {
            supabaseClient = SupabaseClient.GetInstance();
            supabase = supabaseClient.SupabaseAccessor;
        }

        public static AuthService GetInstance()
        {
            if(_instance == null)
            {
                _instance = new AuthService();
            }

            return _instance;
        }

        public async Task<bool> SignUp(string nickName, string bio, string role, string email, string password)
        {
            try
            {
                var userMetaData = new SignUpOptions
                {
                    Data = new Dictionary<string, object>
                    {
                        {"NickName", nickName},
                        {"Bio", bio },
                        {"Role", role },
                       
                    }
                };
                var session = await supabase.Auth.SignUp(email, password, userMetaData);

                if(session != null && session.AccessToken != null)
                {
                  
                        return true;
                        
                } else
                {
                    throw new AuthException($"Error creating user account");
                }

            } catch(Exception e)
            {

                throw new Exception($"Error creating user account: {e}");
            }
        }

        public async Task<(bool isSuccess, UserReturnObject userObject)> SignIn(string email, string password)
        {
            try
            {
                var session = await supabase.Auth.SignIn(email, password);

                if(session != null)
                {
                    if(session.User != null)
                    {
                        return (true, new UserReturnObject { Email = session.User.Email, Id = session.User.Id });
                    }

                    return (false, new UserReturnObject());
                } else
                {
                    throw new AuthException("Error signing user in.");
                }

            } catch(Exception e)
            {

                throw;
            }
        }

       
    }
}
