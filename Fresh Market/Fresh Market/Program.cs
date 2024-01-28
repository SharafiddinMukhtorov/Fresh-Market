
using FreshMarket.Extensions;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

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
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddAuthentication("Bearer")
           .AddJwtBearer(options => options.TokenValidationParameters = new()
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = "MarketUz-api",
               ValidAudience = "MarketUz",
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("login sharafiddin_m_secret_key1234"))
           });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // app.UseAuthorization();

            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
