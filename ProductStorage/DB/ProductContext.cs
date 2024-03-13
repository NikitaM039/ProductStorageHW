using ProductStorageHW.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ProductStorageHW.DB
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Storage> Storages { get; set; }

        public DbSet<ProductStorage> ProductStorages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=192.168.22.115; username=postgres; Password=admin; Database=HomeWork1");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(x => x.Id).HasName("ProductId");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                .HasColumnName("ProductName")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(e => e.Cost)
                .HasColumnName("Cost")
                .IsRequired();

                entity.HasOne(x => x.Category)
                .WithMany(c => c.Products).HasForeignKey(x => x.CategoryId).HasConstraintName("CategoryToProduct");

                entity.HasMany(x => x.ProductStorage).WithOne(z => z.Product);

            });


            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasKey(x => x.Id).HasName("CategoryId");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                .HasColumnName("CategoryName")
                .HasMaxLength(255)
                .IsRequired();



            });


            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storages");

                entity.HasKey(x => x.Id).HasName("StorageId");
                entity.HasIndex(x => x.Name).IsUnique();


                entity.Property(e => e.Name)
                .HasColumnName("StorageName")
                .HasMaxLength(255)
                .IsRequired();


                entity.HasMany(a => a.ProductStorage).WithOne(b => b.Storage);

            });

            modelBuilder.Entity<ProductStorage>(entity =>
            {
                entity.ToTable("ProductStorages");

                entity.HasKey(x => x.Id).HasName("ProductStorageId");
                entity.HasIndex(x => x.Name).IsUnique();


                entity.Property(e => e.Name)
                .HasColumnName("ProductStorageName")
                .HasMaxLength(255)
                .IsRequired();


            });


        }
    }
}