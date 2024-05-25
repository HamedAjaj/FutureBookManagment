using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection;
using FutureOFTask.Domain.Entities;
using FutureOFTask.Domain.Entities.Identity;

namespace FutureOFTask.Repository.Data
{
    public class BookDbContext : IdentityDbContext<AppUser>
    {
        public BookDbContext(DbContextOptions<BookDbContext> options):base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // apply fluent api configuration file at Repository/Config
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        
        
    }
}
