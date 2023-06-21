using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.models;

namespace PokemonReviewApp.data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }

        public DbSet<Category> CategorySet { get; set; }
        public DbSet<Country> CountrySet { get; set; }
        public DbSet<Owner> OwnerSet { get; set; }
        public DbSet<Pokemon> PokemonSet { get; set; }
        public DbSet<PokemonOwner> PokemonOwnerSet { get; set; }
        public DbSet<PokemonCategory> PokemonCategorySet { get; set; }
        public DbSet<Review> ReviewSet { get; set; }
        public DbSet<Reviewer> ReviewerSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokemonCategory>()
                .HasKey(pc => new { pc.PokemonId, pc.CategoryId });
            modelBuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Pokemon)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(pc => pc.PokemonId);
            
            modelBuilder.Entity<PokemonCategory>()
                 .HasOne(p => p.Category)
                 .WithMany(pc => pc.PokemonCategories)
                 .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<PokemonOwner>()
               .HasKey(pc => new { pc.PokemonId, pc.OwnerId });
            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Pokemon)
                .WithMany(po => po.PokemonOwners)
                .HasForeignKey(po => po.PokemonId);

            modelBuilder.Entity<PokemonOwner>()
                 .HasOne(p => p.Owner)
                 .WithMany(po => po.PokemonOwners)
                 .HasForeignKey(po => po.OwnerId);
        }
    }
}
