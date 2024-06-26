﻿using Supabase;
using Supabase.Postgrest;

namespace NexusPilot_Auth_Service.DAO
{
    public class SupabaseClient
    {
        private static SupabaseClient _instance;

        protected IConfiguration _configuration;
        protected string SupabaseProjectUrl;
        protected string SupabaseApiKey;
        protected SupabaseOptions Options;
        public Supabase.Client SupabaseAccessor { get; }

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

        public static SupabaseClient GetInstance()
        {
            if(_instance == null)
            {
                _instance = new SupabaseClient();
            }

            return _instance;
        }
    }
}
