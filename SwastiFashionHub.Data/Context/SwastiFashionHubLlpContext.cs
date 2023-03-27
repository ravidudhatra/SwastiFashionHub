using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SwastiFashionHub.Data.Models;

namespace SwastiFashionHub.Data.Context;

public partial class SwastiFashionHubLlpContext : DbContext
{
    public SwastiFashionHubLlpContext() { }

    public SwastiFashionHubLlpContext(DbContextOptions<SwastiFashionHubLlpContext> options)
        : base(options) { }

    public virtual DbSet<Design> Designs { get; set; }

    public virtual DbSet<DesignImage> DesignImages { get; set; }

    public virtual DbSet<Fabric> Fabrics { get; set; }

    public virtual DbSet<FabricChallan> FabricChallans { get; set; }

    public virtual DbSet<JobChallan> JobChallans { get; set; }

    public virtual DbSet<JobChallanTakaDetail> JobChallanTakaDetails { get; set; }

    public virtual DbSet<OrderForm> OrderForms { get; set; }

    public virtual DbSet<OrderFormDesignDetail> OrderFormDesignDetails { get; set; }

    public virtual DbSet<Party> Parties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public class SwastiFashionHubLlpContextFactory : IDesignTimeDbContextFactory<SwastiFashionHubLlpContext>
    {
        public SwastiFashionHubLlpContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<SwastiFashionHubLlpContext>();
            optionBuilder.UseSqlServer("Data Source=LAPTOP-7C70UPGB;Initial Catalog=SwastiFashionHubLLPDB;User ID=sa;Password=saadmin;TrustServerCertificate=True");
            return new SwastiFashionHubLlpContext(optionBuilder.Options);
        }
    }
}
