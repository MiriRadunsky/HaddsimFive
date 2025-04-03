using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class DB_Manager : DbContext
{
    public DB_Manager()
    {
    }

    public DB_Manager(DbContextOptions<DB_Manager> options)
        : base(options)
    {
    }

    public virtual DbSet<Good> Goods { get; set; }

    public virtual DbSet<GoodsToOrder> GoodsToOrders { get; set; }

    public virtual DbSet<GoodsToSupplier> GoodsToSuppliers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='C:\\Users\\User\\Desktop\\הדסים\\part 4\\ShopProj\\DAL\\data\\DB.mdf';Integrated Security=True;Connect Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Good>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Goods__3214EC070A81DBE6");

            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<GoodsToOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GoodsToO__3214EC07AA6C1C77");

            entity.HasOne(d => d.IdGoodsNavigation).WithMany(p => p.GoodsToOrders)
                .HasForeignKey(d => d.IdGoods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GoodsToOrders_ToTable");

            entity.HasOne(d => d.IdOrdersNavigation).WithMany(p => p.GoodsToOrders)
                .HasForeignKey(d => d.IdOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GoodsToOrders_ToTable1");
        });

        modelBuilder.Entity<GoodsToSupplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GoodsToS__3214EC078F4F1972");

            entity.HasOne(d => d.IdGoodsNavigation).WithMany(p => p.GoodsToSuppliers)
                .HasForeignKey(d => d.IdGoods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GoodsToSuppliers_ToTable_1");

            entity.HasOne(d => d.IdSuppliersNavigation).WithMany(p => p.GoodsToSuppliers)
                .HasForeignKey(d => d.IdSuppliers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GoodsToSuppliers_ToTable");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC071EDE88A5");

            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("waiting")
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            entity.HasOne(d => d.IdSuppliersNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdSuppliers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_ToTable");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC07C6C8F3E5");

            entity.Property(e => e.Company)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.RepresentativeName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
