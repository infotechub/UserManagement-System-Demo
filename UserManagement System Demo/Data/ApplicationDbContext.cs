using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagement_System_Demo.Models;

namespace UserManagement_System_Demo.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Product> products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Wallet> Wallets { get; set; }


        public DbSet<UserAccount> UserAccounts { get; set; }


    }
}