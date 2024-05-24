using FutureOFTask.Domain.IUnitOfWork;
using FutureOFTask.Helper;
using FutureOFTask.Repository.GenericRepo;
using FutureOFTask.Repository.UnitOfWork;
using FutureOFTask.Service.TokenService;
using Microsoft.AspNetCore.Mvc;

namespace FutureOFTask.Extensions
{
    public static class AppServicesExtenstions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
