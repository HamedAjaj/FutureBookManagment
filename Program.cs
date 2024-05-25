
using FutureOFTask.Domain.Entities.Identity;
using FutureOFTask.Extensions;
using FutureOFTask.Repository.Data;
using FutureOFTask.Repository.Data.DataSeeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FutureOFTask
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);

            var connectionString = builder.Configuration.GetConnectionString("BookConnection") ??
                    throw new InvalidOperationException("Error in Database Connection");
            builder.Services.AddDbContext<BookDbContext>(options => options.UseSqlServer(connectionString));
            



            var app = builder.Build();


            // Execute  update-database automatically without make it manually  
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<BookDbContext>();
                await dbContext.Database.MigrateAsync();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await AppIdentitySeed.SeedUsersAsync(userManager,roleManager);
                await AppIdentitySeed.SeedRoles(services);
                await SeedGenres.SeedAsync(dbContext);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occured during  apply the migration ");
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
