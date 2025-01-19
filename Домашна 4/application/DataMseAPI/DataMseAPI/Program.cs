using DataMseAPI.Data;
using DataMseAPI.Models;
using DataMseAPI.Repository;
using DataMseAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataMseAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
            builder.Services.AddTransient<IMseDataRepository, MseDataRepository>();

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            // Configure Database
            builder.Services.AddDbContext<MyDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Remove HTTPS redirection for Docker
            // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            app.MapControllers();

            app.Run();
        }
    }
}