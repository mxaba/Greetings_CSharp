using System;
using Greetings_CSharp.Models;
using Microsoft.EntityFrameworkCore;

namespace Greetings_CSharp.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Greetings> Greetings { get; set; }
    }
}
