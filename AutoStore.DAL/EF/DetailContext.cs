using AutoStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStore.DAL.EF
{
    public class DetailContext : DbContext
    {
        public DbSet<AutoDetail> AutoDetails { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DetailContext(string connectionStirng) : base(connectionStirng) { }
    }
}
