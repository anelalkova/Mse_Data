using Microsoft.EntityFrameworkCore;
using DataMseAPI.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using DataMseAPI.Model;

namespace DataMseAPI.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<MseData> MseData { get; set; }
        public DbSet<DateClosePrice> DateClosePrices { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
