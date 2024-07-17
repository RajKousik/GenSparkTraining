using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Contexts;
using ProductWebAPI.Interfaces;
using ProductWebAPI.Repository;
using ProductWebAPI.Services;

namespace ProductWebAPI
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
            builder.Services.AddSwaggerGen();

            // Configure Azure Key Vault


            var secretName = builder.Configuration.GetConnectionString("AZURE_SECRET_NAME");
            var keyVaultName = builder.Configuration.GetConnectionString("AZURE_KEY_VAULT_NAME");
            var kvUri = $"https://{keyVaultName}.vault.azure.net";

            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            var secret = await client.GetSecretAsync(secretName);
            var connectionString = secret.Value.Value;

            Console.WriteLine(connectionString);


            // Configure DbContext
            builder.Services.AddDbContext<ProductWebAPIContext>(options => options.UseSqlServer(connectionString));



            builder.Services.AddScoped<IRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
