using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Infrastructure.Repositories;
using RestaurantManagementSystem.Infrastructure.Repositories.Interfaces;
using RestaurantManagementSystem.Infrastructure.Services;
using RestaurantManagementSystem.Infrastructure.Services.Interfaces;
using RestaurantManagementSystem.Infrastructure.Validators;
using RestaurantManagementSystem.Infrastructure.Validators.Interfaces;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.Constants;

namespace RestaurantManagementSystem.API.Helper
{
    internal static class RequestFactory
    {
        internal static void RegisterServices(this WebApplicationBuilder builder)
        {
            _ = builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            _ = builder.Services.AddEndpointsApiExplorer();
            _ = builder.Services.AddSwaggerGen();

            _ = builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    _ = policy.AllowAnyOrigin()        
                          .AllowAnyMethod()        
                          .AllowAnyHeader();       
                });
            });

            //Registered sql server
            _ = builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(
                            builder.Configuration.GetConnectionString(Constants.DefaultConnection!)
                            ));

            //Services
            _ = builder.Services.AddScoped<ILogService, LogService>();
            _ = builder.Services.AddScoped<ITableService, TableService>();
            _ = builder.Services.AddScoped<IReservationService, ReservationService>();

            //Validator
            _ = builder.Services.AddScoped<IBusinessRuleValidator, BusinessRuleValidator>();

            //Repository
            _ = builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
