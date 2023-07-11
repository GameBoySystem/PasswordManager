using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Server.Data;
//using PasswordManager.Server.Migrations;
//using PasswordManager.Server.Models;
using PasswordManager.Shared;

namespace PasswordManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public PasswordsController(ApplicationDbContext context, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAccount()
        {
            string userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = _context.Users.Include(s => s.Accounts).FirstOrDefault(s => s.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var accounts = _context.Accounts
                .Where(a => a.Users.Contains(user))
                .ToList();
            return accounts;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, AccountUpdate accountUpdate)
        {
            if (id != accountUpdate.Id)
            {
                return BadRequest();
            }

            var existingAccount = await _context.Accounts.FindAsync(id);

            if (existingAccount == null)
            {
                return NotFound();
            }

            existingAccount.Id = accountUpdate.Id;
            existingAccount.Login = accountUpdate.Login;
            existingAccount.Password = accountUpdate.Password;
            existingAccount.URL = accountUpdate.URL;
            existingAccount.Comment = accountUpdate.Comment;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Account'  is null.");
            }

            string userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = _context.Users.Include(s => s.Accounts).FirstOrDefault(s => s.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            account.Users = new List<ApplicationUser> { user };

            _context.Accounts.Add(account);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
