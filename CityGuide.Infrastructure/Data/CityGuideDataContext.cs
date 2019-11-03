using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CityGuide.Domain.Entity;

namespace CityGuide.Infrastructure.Data
{
    public class CityGuideDataContext : DbContext
    {
        public CityGuideDataContext(DbContextOptions<CityGuideDataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().ToTable("City", "dbo");
            modelBuilder.Entity<User>().ToTable("User", "dbo");
            modelBuilder.Entity<Photo>().ToTable("Photo", "dbo");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
