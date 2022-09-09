using Kodlama.io.Devs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Technology> Technologies { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(a =>
            {
                a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                a.Property(k => k.Id).HasColumnName("Id");
                a.Property(k => k.Name).HasColumnName("Name");
                a.HasMany(k => k.Technologies);
            });

            modelBuilder.Entity<Technology>(a =>
            {
                a.ToTable("Technologies").HasKey(k => k.Id);
                a.Property(k => k.Id).HasColumnName("Id");
                a.Property(k => k.Name).HasColumnName("Name");
                a.Property(k => k.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                a.HasOne(k => k.ProgrammingLanguage);
            });

            ProgrammingLanguage[] programmingLanguageEntitySeed = { new(1, "C#"), new(2, "Python") };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageEntitySeed);

            Technology[] technologyEntitySeed = { new(1, 1, "Asp.Net Core"), new(2, 4, "Spring"), new(3, 1, "WPF"), new(4, 4, "JSP") };
            modelBuilder.Entity<Technology>().HasData(technologyEntitySeed);
        }
    }
}
