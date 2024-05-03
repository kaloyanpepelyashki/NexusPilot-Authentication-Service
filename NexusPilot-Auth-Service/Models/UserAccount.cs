using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace NexusPilot_Auth_Service.Models
{
    [Table("user_accounts")]
    public class UserAccount : BaseModel
    {
        [PrimaryKey("user_unique_key")]
        public string Id { get; set; }
        [Column("nick_name")]
        public string NickName { get; set; }
        [Column("bio")]
        public string Bio { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("accountdeleted")]
        public bool AccountDeleted { get; set; }
        [Column("role")]
        public string Role { get; set; }

    }
}
