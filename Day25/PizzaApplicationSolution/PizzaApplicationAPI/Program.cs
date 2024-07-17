using Easy_Password_Validator.Models;
using Easy_Password_Validator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PizzaApplicationAPI.Contexts;
using PizzaApplicationAPI.Interfaces;
using PizzaApplicationAPI.Models;
using PizzaApplicationAPI.Repositories;
using PizzaApplicationAPI.Services;
using System.Text;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System.Diagnostics;



namespace PizzaApplicationAPI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                           new OpenApiSecurityScheme
                           {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                           },
                           new string[] {}
                     }
                });
            });

            #region Logging
            builder.Services.AddLogging(l => l.AddLog4Net());
            #endregion

            #region DbContexts

            #region Azure Key-Vault

            const string secretName = "connectionString";
            var keyVaultName = "secretkeys-kousik";
            var kvUri = $"https://{keyVaultName}.vault.azure.net";
               
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            //Debug.WriteLine($"Retrieving your secret from {keyVaultName}.");
            var secret = await client.GetSecretAsync(secretName);
            //Debug.WriteLine($"Your secret is '{secret.Value.Value}'.");

            #endregion

            var connectionString = secret.Value.Value;

            //Debug.WriteLine(builder.Configuration.GetConnectionString("defaultConnection"));
            //Debug.WriteLine(connectionString);

            //builder.Services.AddDbContext<PizzaDbContext>(
            //    options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
            //    );

            builder.Services.AddDbContext<PizzaDbContext>(
                options => options.UseSqlServer(connectionString)
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
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddTransient(service => new PasswordValidatorService(new PasswordRequirements()));
            #endregion

            #region AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            #endregion

            #region Authentication

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey:JWT"]))
                    };

                });

            #endregion


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
