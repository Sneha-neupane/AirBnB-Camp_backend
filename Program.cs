using CampBackend;
using CampBackend.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace CampBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped(typeof(DataBase), typeof(DataList));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton(typeof(DataBase), typeof(DataList));

            //adjust ConfigureServices-method
            builder.Services.AddCors(s => s.AddPolicy("MyPolicy", builder => builder.WithOrigins("http://localhost:8080")
                                               .AllowAnyMethod()
                                               .AllowAnyHeader()
                                               .AllowCredentials()));
            


            //adjust Configure-method 
            //place this BEFORE the mapping of the endpoints and before use of authorization

            //ConfigureServices
            builder.Services
              .AddAuthentication()
              .AddScheme<AuthenticationSchemeOptions,
                      BasicAuthenticationHandler>("BasicAuthenticationScheme", options => { });

            builder.Services.AddAuthorization(options => {
                options.AddPolicy("BasicAuthentication",
                        new AuthorizationPolicyBuilder("BasicAuthenticationScheme").RequireAuthenticatedUser().Build());
            });



            var app = builder.Build();
            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("MyPolicy");

            //Configure in Startup.cs
            app.UseHttpsRedirection();


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}