using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DoAnPM_TH_.Models;

public partial class LongChauStoreContext : DbContext
{
    public LongChauStoreContext()
    {
    }

    public LongChauStoreContext(DbContextOptions<LongChauStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-NKSMOFR\\SQLEXPRESS;Initial Catalog=LongChauStore;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__Brand__DAD4F3BE96CD19F1");

            entity.ToTable("Brand");

            entity.Property(e => e.BrandId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BrandID");
            entity.Property(e => e.BrandName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CateId).HasName("PK__Category__27638D74E1545DDF");

            entity.ToTable("Category");

            entity.Property(e => e.CateId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CateID");
            entity.Property(e => e.CateImg).HasMaxLength(255);
            entity.Property(e => e.CateName).HasMaxLength(50);
            entity.Property(e => e.CateProductCount).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManId).HasName("PK__Manufact__3399D649ED916B90");

            entity.ToTable("Manufacturer");

            entity.Property(e => e.ManId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ManID");
            entity.Property(e => e.ManName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAFCD1701B5");

            entity.Property(e => e.OrderId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("OrderID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.PaymentMethod).HasMaxLength(8);
            entity.Property(e => e.StatusOrder).HasMaxLength(8);
            entity.Property(e => e.StatusPayment).HasMaxLength(8);
            entity.Property(e => e.TimeDelivery).HasColumnType("datetime");
            entity.Property(e => e.TimeOrder).HasColumnType("datetime");
            entity.Property(e => e.TimePay).HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");
            entity.Property(e => e.UserId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__UserID__656C112C");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30CE1B7FDEB");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderDetailId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("OrderDetailID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.OrderId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("OrderID");
            entity.Property(e => e.PaymentMethod).HasMaxLength(8);
            entity.Property(e => e.ProId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ProID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__68487DD7");

            entity.HasOne(d => d.Pro).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProId)
                .HasConstraintName("FK__OrderDeta__ProID__693CA210");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProId).HasName("PK__Product__620295F07D4C4D6C");

            entity.ToTable("Product", tb =>
                {
                    tb.HasTrigger("trg_UpdateProductCountOnDelete");
                    tb.HasTrigger("trg_UpdateProductCountOnInsert");
                    tb.HasTrigger("trg_UpdateProductCountOnUpdate");
                });

            entity.Property(e => e.ProId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ProID");
            entity.Property(e => e.BrandId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BrandID");
            entity.Property(e => e.CateId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CateID");
            entity.Property(e => e.Descript).HasMaxLength(300);
            entity.Property(e => e.ManId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ManID");
            entity.Property(e => e.Origin).HasMaxLength(8);
            entity.Property(e => e.ProName).HasMaxLength(255);
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Status).HasMaxLength(8);
            entity.Property(e => e.Unit).HasMaxLength(25);

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__Product__BrandID__534D60F1");

            entity.HasOne(d => d.Cate).WithMany(p => p.Products)
                .HasForeignKey(d => d.CateId)
                .HasConstraintName("FK__Product__CateID__52593CB8");

            entity.HasOne(d => d.Man).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManId)
                .HasConstraintName("FK__Product__ManID__5441852A");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RateId).HasName("PK__Rating__58A7CCBC550C0E1E");

            entity.ToTable("Rating");

            entity.Property(e => e.RateId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("RateID");
            entity.Property(e => e.DayClose).HasColumnType("datetime");
            entity.Property(e => e.ProId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ProID");
            entity.Property(e => e.UserId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UserID");

            entity.HasOne(d => d.Pro).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ProId)
                .HasConstraintName("FK__Rating__ProID__619B8048");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Rating__UserID__628FA481");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACCEA163A0");

            entity.Property(e => e.UserId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Role)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasMany(d => d.Pros).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Cart",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Cart__ProID__5812160E"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Cart__UserID__571DF1D5"),
                    j =>
                    {
                        j.HasKey("UserId", "ProId").HasName("PK__Cart__01A8E5F385C08520");
                        j.ToTable("Cart");
                        j.IndexerProperty<string>("UserId")
                            .HasMaxLength(8)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("UserID");
                        j.IndexerProperty<string>("ProId")
                            .HasMaxLength(8)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("ProID");
                    });
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__Voucher__3AEE79C12069E3CB");

            entity.ToTable("Voucher");

            entity.Property(e => e.VoucherId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("VoucherID");
            entity.Property(e => e.DayClose).HasColumnType("datetime");
            entity.Property(e => e.ProId)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ProID");
            entity.Property(e => e.VoucherCode)
                .HasMaxLength(8)
                .IsUnicode(false);

            entity.HasOne(d => d.Pro).WithMany(p => p.Vouchers)
                .HasForeignKey(d => d.ProId)
                .HasConstraintName("FK__Voucher__ProID__5DCAEF64");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
