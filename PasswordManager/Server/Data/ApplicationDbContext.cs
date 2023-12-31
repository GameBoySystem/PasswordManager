﻿using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
//using PasswordManager.Server.Models;
using PasswordManager.Shared;
using System.Reflection.Emit;

namespace PasswordManager.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .Navigation(e => e.Accounts).AutoInclude();
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountUpdate> AccountUpdates { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationUserUpdate> ApplicationUserUpdates { get; set; }
    }
}