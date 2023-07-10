using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PasswordManager.Shared
{
    public class ApplicationUser : IdentityUser
    {
        public List<Account> Accounts { get; set; }
    }
}
