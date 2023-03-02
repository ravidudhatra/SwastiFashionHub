using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SwastiFashionHub.Data.Models;

namespace SwastiFashionHub.Data.Context;

public partial class SwastiFashionHubLlpContext : DbContext
{
    public SwastiFashionHubLlpContext()
    {
    }

    public SwastiFashionHubLlpContext(DbContextOptions<SwastiFashionHubLlpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Design> Designs { get; set; }

    public virtual DbSet<Fabric> Fabrics { get; set; }

    public virtual DbSet<FabricChallan> FabricChallans { get; set; }

    public virtual DbSet<JobChallan> JobChallans { get; set; }

    public virtual DbSet<JobChallanTakaDetail> JobChallanTakaDetails { get; set; }

    public virtual DbSet<OrderForm> OrderForms { get; set; }

    public virtual DbSet<OrderFormDesignDetail> OrderFormDesignDetails { get; set; }

    public virtual DbSet<Party> Parties { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-7C70UPGB;Initial Catalog=SwastiFashionHubLLP;User ID=sa;Password=saadmin;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Design>(entity =>
        {
            entity.ToTable("Design");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Fabric>(entity =>
        {
            entity.ToTable("Fabric");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<FabricChallan>(entity =>
        {
            entity.ToTable("FabricChallan");

            entity.Property(e => e.ChallanDate).HasColumnType("datetime");
            entity.Property(e => e.ChallanNo).HasMaxLength(512);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<JobChallan>(entity =>
        {
            entity.ToTable("JobChallan");

            entity.Property(e => e.ChallanDate).HasColumnType("datetime");
            entity.Property(e => e.ChallanNo).HasMaxLength(512);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<JobChallanTakaDetail>(entity =>
        {
            entity.HasOne(d => d.JobChallan).WithMany(p => p.JobChallanTakaDetails)
                .HasForeignKey(d => d.JobChallanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobChallanTakaDetails_JobChallan");
        });

        modelBuilder.Entity<OrderForm>(entity =>
        {
            entity.ToTable("OrderForm");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.JobNo).HasMaxLength(512);
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.TotalMtr).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalQty).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Unit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<OrderFormDesignDetail>(entity =>
        {
            entity.Property(e => e.ColorQty).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.OrderForm).WithMany(p => p.OrderFormDesignDetails)
                .HasForeignKey(d => d.OrderFormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderFormDesignDetails_OrderForm");
        });

        modelBuilder.Entity<Party>(entity =>
        {
            entity.ToTable("Party");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
