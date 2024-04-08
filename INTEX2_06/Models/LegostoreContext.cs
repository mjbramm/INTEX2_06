using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace INTEX2_06.Models;

public partial class LegostoreContext : DbContext
{
    public LegostoreContext()
    {
    }

    public LegostoreContext(DbContextOptions<LegostoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Lego> Legos { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<LineItem> LineItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lego>(entity =>
        {
            entity.HasIndex(e => e.product_ID, "INTEX2_06_Legos_product_ID").IsUnique();

            entity.Property(e => e.product_ID).HasColumnName("product_ID");
            entity.Property(e => e.secondary_color).HasColumnName("secondary_color");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
