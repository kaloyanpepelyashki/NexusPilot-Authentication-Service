using Supabase;

namespace NexusPilot_Auth_Service.DAO
{
    public class SupabaseClient
    {
        protected IConfiguration _configuration;
        protected string SupabaseProjectUrl;
        protected string SupabaseApiKey;
        protected SupabaseOptions Options;
        protected Client SupabaseAccessor;

        public SupabaseClient()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).AddEnvironmentVariables().Build();
            SupabaseProjectUrl = _configuration["SupabaseConfig:ProjectUrl"];
            SupabaseApiKey = _configuration["SupabaseConfig:ApiKey"];
            Options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };
            SupabaseAccessor = new Supabase.Client(SupabaseProjectUrl, SupabaseApiKey, Options);
        }
    }
}
