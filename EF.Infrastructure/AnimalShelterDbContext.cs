using Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace EF.Infrastructure
{
    public class AnimalShelterDbContext : DbContext
    {
        public AnimalShelterDbContext(DbContextOptions<AnimalShelterDbContext> options): base(options)
        {

        }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Residence> Residences{ get; set; }
        public DbSet<Treatment> Treatments{ get; set; }
        
        public DbSet<Volunteer> Volunteers{ get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>().HasIndex(o => o.ClientNumber).IsUnique();
            modelBuilder.Entity<Client>().HasKey(o => o.ClientNumber);
            modelBuilder.Entity<Client>().HasOne(o => o.Cart);

            modelBuilder.Entity<Animal>().HasIndex(o => o.Id).IsUnique();
            modelBuilder.Entity<Animal>().HasKey(o => o.Id);
            modelBuilder.Entity<Animal>().HasOne<Residence>(s => s.Residence).WithMany(g => g.Animals).HasForeignKey(s => s.ResidenceId);
            modelBuilder.Entity<Animal>().HasOne<Client>(s => s.AdoptedBy).WithMany(g => g.Animals).HasForeignKey(s => s.ClientNumber);
            modelBuilder.Entity<Animal>().HasOne<Volunteer>(s => s.Volunteer).WithMany(g => g.Animals).HasForeignKey(s => s.VolunteerId);

            modelBuilder.Entity<Cart>().HasIndex(o => o.Id).IsUnique();
            modelBuilder.Entity<Cart>().HasKey(o => o.Id);
            modelBuilder.Entity<Cart>().HasOne(o => o.Client);

            modelBuilder.Entity<CartItem>().HasIndex(o => o.Id).IsUnique();
            modelBuilder.Entity<CartItem>().HasKey(o => o.Id);
            modelBuilder.Entity<CartItem>().HasOne<Cart>(s => s.Cart).WithMany(g => g.CartItems).HasForeignKey(s => s.CartId);


            modelBuilder.Entity<Comment>().HasIndex(o => o.CommentId).IsUnique();
            modelBuilder.Entity<Comment>().HasKey(o => o.CommentId);
            modelBuilder.Entity<Comment>().HasOne<Animal>(s => s.Animal).WithMany(g => g.Comments).HasForeignKey(s => s.AnimalId);
            modelBuilder.Entity<Comment>().HasOne<Volunteer>(s => s.CommentMadeBy).WithMany(g => g.Comments).HasForeignKey(s => s.VolunteerId);


            modelBuilder.Entity<Residence>().HasIndex(o => o.Id).IsUnique();
            modelBuilder.Entity<Residence>().HasKey(o => o.Id);

            modelBuilder.Entity<Treatment>().HasIndex(o => o.Id).IsUnique();
            modelBuilder.Entity<Treatment>().HasKey(o => o.Id);
            modelBuilder.Entity<Treatment>().HasOne<Animal>(s => s.Animal).WithMany(g => g.Treatments).HasForeignKey(s => s.AnimalId);


            modelBuilder.Entity<Volunteer>().HasIndex(o => o.VolunteerId).IsUnique();
            modelBuilder.Entity<Volunteer>().HasKey(o => o.VolunteerId);


            modelBuilder.Entity<Application>().HasIndex(o => o.Id).IsUnique();
            modelBuilder.Entity<Application>().HasKey(o => o.Id);
            modelBuilder.Entity<Application>().HasOne(o => o.Client).WithMany(g => g.Applications).HasForeignKey(s => s.ClientNumber);
        }
    }
}
