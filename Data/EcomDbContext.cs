using Microsoft.EntityFrameworkCore;
using eCommerce.Models;

namespace eCommerce.Data
{
    public class EcomDbContext : DbContext
    {
        public EcomDbContext(DbContextOptions<EcomDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<RegisterUser> RegisterUsers { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }  // NEW TABLE

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(50);
                entity.Property(e => e.Username).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Password).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Role).HasMaxLength(50).IsRequired();
            });

            // Products
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(50);
                entity.Property(e => e.ItemName).HasMaxLength(200).IsRequired();
                entity.Property(e => e.ItemPrice).HasColumnType("decimal(10,2)");
                entity.Property(e => e.ItemImage).HasMaxLength(500);
                               entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();

    entity.HasOne<User>()
          .WithMany()
          .HasForeignKey(e => e.UserId)
          .OnDelete(DeleteBehavior.Restrict);
});

            // Carts
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(50);
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.ProductId).HasMaxLength(50).IsRequired();

                entity.Property(e => e.ProductName).HasMaxLength(200);
                entity.Property(e => e.Price).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Total).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Quantity).IsRequired();

                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Product>()
                      .WithMany()
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // OrderItem (FINAL CLEAN VERSION)
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");  // ← IMPORTANT: use single table name

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.ProductId).HasMaxLength(50).IsRequired();

                entity.Property(e => e.Price).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Total).HasColumnType("decimal(10,2)");

                entity.Property(e => e.OrderDate).IsRequired();
                entity.Property(e => e.ExpectedDeliveryDate);

                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Product>()
                      .WithMany()
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // RegisterUsers
            modelBuilder.Entity<RegisterUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(50);
                entity.Property(e => e.Username).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Password).HasMaxLength(200).IsRequired();
                entity.Property(e => e.ProfileImage).HasMaxLength(500);
                entity.Property(e => e.Role).HasMaxLength(50);
                entity.Property(e => e.Country).HasMaxLength(100);
                entity.Property(e => e.State).HasMaxLength(100);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.TermsAccepted).IsRequired();
            });
        }
    }
}
