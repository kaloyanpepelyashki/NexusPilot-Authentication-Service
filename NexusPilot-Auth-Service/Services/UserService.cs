using NexusPilot_Auth_Service.DAO;
using NexusPilot_Auth_Service.Models;
using Supabase;

namespace NexusPilot_Auth_Service.Services
{
    public class UserService
    {
        private readonly SupabaseClient _supabaseClient;
        private readonly Client supabase;

        public UserService()
        {
            _supabaseClient = new SupabaseClient();
            supabase = _supabaseClient.SupabaseAccessor;
        }

        public async Task<(bool isSuccess, UserAccount userAccount)> GetUserAccount(string userUUID)
        {
            try
            {
                var userGuid = new Guid(userUUID);
                var result = await supabase.From<UserAccount>().Where(user => user.Id == userGuid).Get();

                if(result != null)
                {
                    if(result.Models.Count > 0)
                    {
                        UserAccount returnedUser = new UserAccount { Id = result.Models[0].Id, NickName = result.Models[0].NickName, Bio = result.Models[0].Bio, Email = result.Models[0].Email, AvatartImageUrl = result.Models[0].AvatartImageUrl, AccountDeleted = result.Models[0].AccountDeleted, Role = result.Models[0].Role };
                        return (true, returnedUser);
                    }

                }

                return (false, new UserAccount());

            } catch(Exception e)
            {
                Console.WriteLine($"Error getting user account: {e}");
                throw;
            }
        }
    }
}
