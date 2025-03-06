
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace WordFlowServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Add allow CORS
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DataContext>(options =>
            {
                //Microsoft.EntityFrameworkCore.SqlServer package is necessery
                options.UseSqlServer(builder.Configuration.GetConnectionString("BasicConnection"));
            });

            //builder.Services.AddDbContext<DataContext>(options =>
            //    options.UseInMemoryDatabase("TestDatabase"));

            // Cors services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:5173") // Dokładny adres Twojego frontendu
                             .AllowAnyMethod() // Zezwól na wszystkie metody (GET, POST itd.)
                             .AllowAnyHeader() // Zezwól na dowolne nagłówki
                             .AllowCredentials(); // Zezwól na dołączanie ciasteczek do zapytań
                      });
            });

            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<DataContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Use Cors
            app.UseCors(MyAllowSpecificOrigins);

            app.MapIdentityApi<IdentityUser>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
