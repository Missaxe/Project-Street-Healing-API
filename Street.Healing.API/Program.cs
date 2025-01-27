
using Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Street.Healing.API.Context.GoogleUser;
using Street.Healing.API.Context.User;
using Street.Healing.API.Helpers;
using Street.Healing.API.Services;

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
            builder.Services.AddDbContext<GoogleUserDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("connectionstring")));


            //Add Email Configs
            var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
           builder.Services.AddSingleton(emailConfig);


            //Services Dependency Injection 

            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IEmailServices, EmailServices>();
            builder.Services.AddScoped<IPasswordServices, PasswordServices>();
            builder.Services.AddTransient<IJwtHandler, JwtHandler>();

            builder.Services.AddIdentity<GoogleUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 7;
                opt.Password.RequireDigit = false;

                opt.User.RequireUniqueEmail = true;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                opt.Lockout.MaxFailedAccessAttempts = 3;
            }).AddEntityFrameworkStores<GoogleUserDbContext>()
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
                 builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
             );


            app.MapControllers();

            app.Run();
        }
    }
}
