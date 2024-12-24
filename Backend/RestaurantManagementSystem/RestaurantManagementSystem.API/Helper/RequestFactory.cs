using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.Constants;
using RestaurantManagementSystem.Repository.Interface;
using RestaurantManagementSystem.Repository.Repository;
using RestaurantManagementSystem.Service.Interface;
using RestaurantManagementSystem.Service.Service;

namespace RestaurantManagementSystem.API.Helper
{
    internal static class RequestFactory
    {
        internal static void RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()        // Allow any origin
                          .AllowAnyMethod()        // Allow any HTTP method (GET, POST, etc.)
                          .AllowAnyHeader();       // Allow any header
                });
            });

            //Registered sql server
            builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(
                            builder.Configuration.GetConnectionString($"{Constants.DefaultConnection}")
                            ));

            //Service and Repository
            builder.Services.AddScoped<ITableService, TableService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<ITableRepository, TableRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
        }
    }
}
