
using FreshMarket.Extensions;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;

namespace Fresh_Market
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
            builder.Services.ConfigureLogger();
            builder.Services.ConfigureRepositories();
            builder.Services.ConfigureServices();
            builder.Services.ConfigureDatabaseContext();

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
