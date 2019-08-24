using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMDB.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<ConfigurationItem> ConfigurationItems { get; set; }
        public DbSet<Dependency> Dependencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dependency>()
                .HasOne(item => item.DependencyCI)
                .WithMany(configItem => configItem.DependencyItems)
                .HasForeignKey(item => item.DependencyCIName);

            modelBuilder.Entity<Dependency>()
                .HasOne(item => item.BaseCI)
                .WithMany(configItem => configItem.BaseItems)
                .HasForeignKey(item => item.BaseCIName);

        }


    }
}
