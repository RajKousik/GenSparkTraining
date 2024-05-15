using Microsoft.EntityFrameworkCore;
using PizzaApplicationAPI.Contexts;
using PizzaApplicationAPI.Interfaces;
using PizzaApplicationAPI.Models;
using PizzaApplicationAPI.Repositories;
using PizzaApplicationAPI.Services;

namespace PizzaApplicationAPI
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

            builder.Services.AddDbContext<PizzaDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
                );

            #endregion

            #region Repositories
            builder.Services.AddScoped<IRepository<int, User>, UserRepository>();
            builder.Services.AddScoped<IRepository<int, Pizza>, PizzaRepository>();
            builder.Services.AddScoped<IRepository<int, Order>, OrderRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPizzaService, PizzaService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            #endregion

            #region AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            #endregion




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
