using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DataStreamingService.Models;
using Microsoft.EntityFrameworkCore;

namespace DataStreamingService.Data
{
    public class DataContext : DbContext
    {

        private readonly IConfiguration _configuration;
        public DataContext(IConfiguration config)
        {
            _configuration = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("GeodicInstance"));
        }
        public virtual DbSet<StreamData> StreamData { get; set; }
    }
}
