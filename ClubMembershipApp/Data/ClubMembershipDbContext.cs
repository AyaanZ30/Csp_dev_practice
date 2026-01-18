using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubMembershipApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClubMembershipApp.Data
{
    // DbContext : EF's base class (source : Microsoft.EntityFrameworkCore.Sqlite)
    public class ClubMembershipDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = {AppDomain.CurrentDomain.BaseDirectory}ClubMembershipDb.db");
            base.OnConfiguring(optionsBuilder);    // passed to DbContext class
        }

        // create a tabular structure for User (model) (SQlite queries generated via LINQ)
        public DbSet<User> Users { get; set; }
        
    }
}