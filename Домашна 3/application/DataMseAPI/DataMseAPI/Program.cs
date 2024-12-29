using DataMseAPI.Data;
using DataMseAPI.Models;
using DataMseAPI.Repository;
using DataMseAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DataMseAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));

            builder.Services.AddTransient<IMseDataRepository, MseDataRepository>();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            builder.Services.AddDbContext<MyDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
