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

            
            builder.Services.AddScoped(typeof(DataBase), typeof(DataList));
            builder.Services.AddControllers();
           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton(typeof(DataBase), typeof(DataList));

            
            builder.Services.AddCors(s => s.AddPolicy("MyPolicy", builder => builder.WithOrigins("http://localhost:8080")
                                               .AllowAnyMethod()
                                               .AllowAnyHeader()
                                               .AllowCredentials()));
            


            

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

            
            app.UseHttpsRedirection();


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}