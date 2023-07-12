using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PasswordManager.Shared
{
    public class Account
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string URL { get; set; }
        
        public string? Comment { get; set; }
        
        [JsonIgnore]
        public ICollection<ApplicationUser>? Users { get; set; }
    }
}
