
using Microsoft.EntityFrameworkCore;
using System;

namespace WordFlowServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddDbContext<DataContext>(options =>
            //{
            //    //Microsoft.EntityFrameworkCore.SqlServer package is necessery
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("BasicConnection"));
            //});

            builder.Services.AddDbContext<DataContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));

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
