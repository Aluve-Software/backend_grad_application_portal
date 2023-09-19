using Grad_Application_Portal_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grad_Application_Portal_Backend.Data
{
    public class GradPortalDBContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public GradPortalDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_configuration.GetConnectionString("ConnString"));
        }

        public DbSet<User> users { get; set; }
    }
}
