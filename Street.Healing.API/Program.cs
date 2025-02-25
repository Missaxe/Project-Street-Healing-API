
using Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Street.Healing.API.Middlewares;
using Street.Healing.Business.Core.Core.Repository;
using Street.Healing.Business.Core.Core.Services;
using Street.Healing.Business.Tech.Helpers;
using Street.Healing.Business.Tech.Tech.Services;
using Street.Healing.DAO.Context;
using Street.Healing.DAO.Models;
using Street.Healing.DAO.Repository;


namespace Street.Healing.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //Configuration Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //logging
            builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

            //Database Configuration
            builder.Services.AddDbContext<UserDbContext>(item =>
            item.UseSqlServer(builder.Configuration.GetConnectionString("connectionstring")));
            builder.Services.AddDbContext<UserGoogleDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("connectionstring")));

            //API key Configuration




            //Add Email Configs
            var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);


            //Services Dependency Injection 

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IEmailServices, EmailServices>();
            builder.Services.AddScoped<IPasswordServices, PasswordServices>();
            builder.Services.AddTransient<IJwtHandler, JwtHandler>();

            //Transien for Middleware Request
            builder.Services.AddTransient<RateLimitingMiddleware>();
            builder.Services.AddTransient<JWTTokenMiddleware>();

            builder.Services.AddIdentity<UserGoogle, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 7;
                opt.Password.RequireDigit = false;

                opt.User.RequireUniqueEmail = true;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                opt.Lockout.MaxFailedAccessAttempts = 3;
            }).AddEntityFrameworkStores<UserGoogleDbContext>()
            .AddDefaultTokenProviders();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //App.UseSwagger
            app.UseSwagger();
            app.UseSwaggerUI();



            //Allow Access to Client
            app.UseCors(
                 builder => builder.WithOrigins()
                      .AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                    
             );

            //Using API key middleware to validate key from client request 

            app.UseMiddleware<RateLimitingMiddleware>();

            app.UseMiddleware<JWTTokenMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
