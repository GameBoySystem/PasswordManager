using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Shared
{
    public class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string URL { get; set; }
        public string? Comment { get; set; }

        public ICollection<ApplicationUser>? Users { get; set; }
    }
}
