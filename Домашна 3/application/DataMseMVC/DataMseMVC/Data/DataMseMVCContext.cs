using DataMseMVC.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.Sockets;

namespace DataMseMVC.Data
{
    public class DataMseMVCContext : DbContext
    {
        public DataMseMVCContext(DbContextOptions<DataMseMVCContext> options) : base(options)
        {
        }

        public DbSet<MseData> MseData { get; set; }
        public DbSet<DateClosePrice> DateClosePrice { get; set; }
    }
}
