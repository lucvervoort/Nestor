using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Nestor.Api.Controllers;

namespace Nestor.Api
{
    public class NestorDbContext: IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public NestorDbContext(DbContextOptions<NestorDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
