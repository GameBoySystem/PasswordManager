using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PasswordManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccessController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public ActionResult<List<ApplicationUser>> GetUsers(int id)
        {
            var account = _context.Accounts.Include(s => s.Users).FirstOrDefault(s => s.Id == id);

            if (account == null)
            {
                return NotFound();
            }

            var users = _context.Users
                .Where(a => a.Accounts.Contains(account))
                .ToList();
            return users;
        }

        [HttpPut("{accountId}")]
        public async Task<IActionResult> AddUser(int accountId, ApplicationUserUpdate applicationUser)
        {
            string userName = applicationUser.UserName;
            var user = _context.Users.Include(s => s.Accounts).FirstOrDefault(s => s.UserName == userName);
            string userId = user.Id;
            var currentUser = _context.Users
                .Where(a => a.Id == userId)
                .ToList();

            Account currentAccount = await _context.Accounts.FindAsync(accountId);

            var users = _context.Users
                .Where(a => a.Accounts.Contains(currentAccount))
                .ToList();

            users.AddRange(currentUser);

            currentAccount.Users = users;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string userId, int accountId)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = _context.Users.Include(s => s.Accounts).FirstOrDefault(s => s.Id == userId);

            var account = _context.Accounts.Include(s => s.Users).FirstOrDefault(s => s.Id == accountId);

            ApplicationUser currentUser = (ApplicationUser)_context.Users
                .Where(a => a.Id == userId)
                .Where(a => a.Accounts.Contains(account));

            Account currentAccount = (Account)_context.Accounts
                .Where(a => a.Id == accountId)
                .Where(a => a.Users.Contains(user));

            currentAccount.Users.Remove(currentUser);
            
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}