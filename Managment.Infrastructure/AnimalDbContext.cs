
using Managment.Core.Domain;
using Managment.Core.DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Managment.Infrastructure
{
    public class AnimalDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Residence> Residences { get; set; }

        public DbSet<Treatment> Treatments { get; set; }

        public DbSet<Volunteer> Volunteers { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Interest> Interests { get; set; }

        public DbSet<AnimalForAdoption> AnimalForAdoptions { get; set; }

        public AnimalDbContext(DbContextOptions<AnimalDbContext> options) : base(options) 
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Residence>().HasData(
                new Residence 
                {
                    Id = 1,
                    Name = "Cage1HondMan4",
                    Capacity = 4,
                    Type = AnimalType.Hond,
                    Gender = Gender.Mannetje
                },
                new Residence
                {
                    Id = 2,
                    Name = "Cage2KatVrouw4",
                    Capacity = 4,
                    Type = AnimalType.Kat,
                    Gender = Gender.Vrouwtje
                },
                new Residence 
                {
                    Id = 3,
                    Name = "Cage3KatMan4",
                    Capacity = 4,
                    Type = AnimalType.Kat,
                    Gender = Gender.Mannetje
                },
                new Residence
                {
                    Id = 4,
                    Name = "Cage4HondVrouw4",
                    Capacity = 4,
                    Type = AnimalType.Hond,
                    Gender = Gender.Vrouwtje
                },
                new Residence
                {
                    Id = 5,
                    Name = "Cage5",
                    Capacity = 1,
                }

                );
             
            
        }
    }
}
