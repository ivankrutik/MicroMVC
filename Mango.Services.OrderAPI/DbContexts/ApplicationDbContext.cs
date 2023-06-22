using Mango.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Mango.Services.OrderAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<OrderDetail> Details { get; set; }

        public DbSet<OrderHeader> Headers { get; set; }
    }
}
