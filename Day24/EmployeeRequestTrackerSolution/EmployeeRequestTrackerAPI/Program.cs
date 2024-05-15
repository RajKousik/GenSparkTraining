using EmployeeRequestTrackerAPI.Contexts;
using EmployeeRequestTrackerAPI.Interfaces;
using EmployeeRequestTrackerAPI.Models;
using EmployeeRequestTrackerAPI.Repositories;
using EmployeeRequestTrackerAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRequestTrackerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region DbContexts

            builder.Services.AddDbContext<RequestTrackerContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
                );

            #endregion

            #region Repositories
            builder.Services.AddScoped<IRepository<int, Employee>, EmployeeRepository>();
            builder.Services.AddScoped<IRepository<int, User>, UserRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IUserService, UserService>();
            #endregion

            var app = builder.Build();

            #region Developement Configuration & Swagger
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            #endregion

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
