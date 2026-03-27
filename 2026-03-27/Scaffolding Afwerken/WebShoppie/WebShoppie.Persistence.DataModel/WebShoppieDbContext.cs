using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebShoppie.DataModel.Entities;

namespace WebShoppie.DataModel;

public partial class WebShoppieDbContext : DbContext
{
    public WebShoppieDbContext()
    {
    }

    public WebShoppieDbContext(DbContextOptions<WebShoppieDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderproduct> Orderproducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Customerid).HasName("pk_customers");

            entity.ToTable("customers", "storefront");

            entity.HasIndex(e => e.Email, "uk_customers").IsUnique();

            entity.Property(e => e.Customerid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("customerid");
            entity.Property(e => e.Addressline1)
                .HasMaxLength(36)
                .HasColumnName("addressline1");
            entity.Property(e => e.Addressline2)
                .HasMaxLength(36)
                .HasColumnName("addressline2");
            entity.Property(e => e.Addressline3)
                .HasMaxLength(36)
                .HasColumnName("addressline3");
            entity.Property(e => e.Country)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("country");
            entity.Property(e => e.Dateofbirth)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateofbirth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(60)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(60)
                .HasColumnName("lastname");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Orderid).HasName("pk_orders");

            entity.ToTable("orders", "storefront");

            entity.HasIndex(e => e.Customerid, "fki_orders_customerid");

            entity.HasIndex(e => new { e.Customerid, e.Orderdate }, "uk_orders").IsUnique();

            entity.Property(e => e.Orderid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("orderid");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Orderdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("orderdate");
            entity.Property(e => e.Totalprice)
                .HasPrecision(8, 2)
                .HasColumnName("totalprice");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Customerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orders_customers");
        });

        modelBuilder.Entity<Orderproduct>(entity =>
        {
            entity.HasKey(e => e.Orderproductid).HasName("pk_orderproducts");

            entity.ToTable("orderproducts", "storefront");

            entity.HasIndex(e => e.Orderid, "fki_orderproducts_orderid");

            entity.HasIndex(e => e.Productid, "fki_orderproducts_productid");

            entity.HasIndex(e => new { e.Orderid, e.Productid }, "uk_orderproducts").IsUnique();

            entity.Property(e => e.Orderproductid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("orderproductid");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Price)
                .HasPrecision(6, 2)
                .HasColumnName("price");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderproducts)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orderproducts_orders");

            entity.HasOne(d => d.Product).WithMany(p => p.Orderproducts)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orderproducts_products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Productid).HasName("pk_products");

            entity.ToTable("products", "storefront");

            entity.HasIndex(e => e.Title, "uk_products").IsUnique();

            entity.Property(e => e.Productid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("productid");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.Price)
                .HasPrecision(8, 2)
                .HasColumnName("price");
            entity.Property(e => e.Stockcount).HasColumnName("stockcount");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
