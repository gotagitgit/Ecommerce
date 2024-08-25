
using Inventory.Application;
using Inventory.Infrastructure;
using Inventory.Infrastructure.Settings;
using Inventory.SQLiteInfrastructure;

namespace Inventory.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var services = builder.Services;

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.AllowAnyOrigin(); // Temporary while debugging
                    });
            });


            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var configuration = builder.Configuration;

            services.RegisterApplicationDependencies()
                    //.RegisterInfrastureDependencies()
                    .RegisterSQLiteInfrastureDependencies(configuration);

            services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
                       
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}