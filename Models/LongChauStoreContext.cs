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

    public virtual DbSet<ListProductImg> ListProductImgs { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-NKSMOFR\\SQLEXPRESS;Initial Catalog=LongChauStore;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__Brand__DAD4F3BE96CD19F1");

            entity.ToTable("Brand");

            entity.Property(e => e.BrandId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BrandID");
            entity.Property(e => e.BrandName).HasMaxLength(50);
            entity.Property(e => e.BrandOrigin).HasMaxLength(50);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CateId).HasName("PK__Category__27638D74E1545DDF");

            entity.ToTable("Category");

            entity.Property(e => e.CateId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CateID");
            entity.Property(e => e.CateImg).HasMaxLength(255);
            entity.Property(e => e.CateName).HasMaxLength(50);
            entity.Property(e => e.CateProductCount).HasDefaultValue(0);
        });

        modelBuilder.Entity<ListProductImg>(entity =>
        {
            entity.ToTable("ListProductImg");

            entity.Property(e => e.ProId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Pro).WithMany(p => p.ListProductImgs)
                .HasForeignKey(d => d.ProId)
                .HasConstraintName("FK_ListProductImg_Product");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManId).HasName("PK__Manufact__3399D649ED916B90");

            entity.ToTable("Manufacturer");

            entity.Property(e => e.ManId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ManID");
            entity.Property(e => e.ManCountry).HasMaxLength(50);
            entity.Property(e => e.ManName).HasMaxLength(255);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAFCD1701B5");

            entity.Property(e => e.OrderId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OrderID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.PaymentMethod).HasMaxLength(8);
            entity.Property(e => e.StatusOrder).HasMaxLength(8);
            entity.Property(e => e.StatusPayment).HasMaxLength(8);
            entity.Property(e => e.TimeDelivery).HasColumnType("datetime");
            entity.Property(e => e.TimeOrder).HasColumnType("datetime");
            entity.Property(e => e.TimePay).HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__UserID__656C112C");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30CE1B7FDEB");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderDetailId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OrderDetailID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.OrderId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OrderID");
            entity.Property(e => e.PaymentMethod).HasMaxLength(8);
            entity.Property(e => e.ProId)
                .HasMaxLength(50)
                .IsUnicode(false)
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
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ProID");
            entity.Property(e => e.BrandId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BrandID");
            entity.Property(e => e.BrandOriginId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BrandOriginID");
            entity.Property(e => e.CateId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CateID");
            entity.Property(e => e.ManCountryId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ManCountryID");
            entity.Property(e => e.ManId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ManID");
            entity.Property(e => e.ProName).HasMaxLength(255);
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Status).HasMaxLength(8);
            entity.Property(e => e.Unit).HasMaxLength(25);

            entity.HasOne(d => d.Brand).WithMany(p => p.ProductBrands)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Product_BrandID_Brand_BrandID");

            entity.HasOne(d => d.BrandOrigin).WithMany(p => p.ProductBrandOrigins)
                .HasForeignKey(d => d.BrandOriginId)
                .HasConstraintName("FK_Product_BrandOrigin_Brand_BrandID");

            entity.HasOne(d => d.Cate).WithMany(p => p.Products)
                .HasForeignKey(d => d.CateId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Product_Category");

            entity.HasOne(d => d.ManCountry).WithMany(p => p.ProductManCountries)
                .HasForeignKey(d => d.ManCountryId)
                .HasConstraintName("FK_Product_Manufacturer");

            entity.HasOne(d => d.Man).WithMany(p => p.ProductMen)
                .HasForeignKey(d => d.ManId)
                .HasConstraintName("FK__Product__ManID__5441852A");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RateId).HasName("PK__Rating__58A7CCBC550C0E1E");

            entity.ToTable("Rating");

            entity.Property(e => e.RateId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RateID");
            entity.Property(e => e.DayClose).HasColumnType("datetime");
            entity.Property(e => e.ProId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ProID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

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

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
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
                        j.IndexerProperty<int>("UserId").HasColumnName("UserID");
                        j.IndexerProperty<string>("ProId")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("ProID");
                    });
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__Voucher__3AEE79C12069E3CB");

            entity.ToTable("Voucher");

            entity.Property(e => e.VoucherId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VoucherID");
            entity.Property(e => e.DayClose).HasColumnType("datetime");
            entity.Property(e => e.ProId)
                .HasMaxLength(50)
                .IsUnicode(false)
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
