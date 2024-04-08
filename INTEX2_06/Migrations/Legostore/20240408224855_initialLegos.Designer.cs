﻿// <auto-generated />
using INTEX2_06.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace INTEX2_06.Migrations.Legostore
{
    [DbContext(typeof(LegostoreContext))]
    [Migration("20240408224855_initialLegos")]
    partial class initialLegos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("INTEX2_06.Models.Customer", b =>
                {
                    b.Property<int>("customer_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("customer_ID"));

                    b.Property<double>("avg_rating")
                        .HasColumnType("float");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("img_link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("num_parts")
                        .HasColumnType("int");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<string>("primary_color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("secondary_color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("total_ordered")
                        .HasColumnType("int");

                    b.Property<int>("year")
                        .HasColumnType("int");

                    b.HasKey("customer_ID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("INTEX2_06.Models.Lego", b =>
                {
                    b.Property<int>("product_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("product_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("product_ID"));

                    b.Property<double>("avg_rating")
                        .HasColumnType("float");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("img_link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("num_parts")
                        .HasColumnType("int");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<string>("primary_color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("secondary_color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("secondary_color");

                    b.Property<int>("total_ordered")
                        .HasColumnType("int");

                    b.Property<int>("year")
                        .HasColumnType("int");

                    b.HasKey("product_ID");

                    b.HasIndex(new[] { "product_ID" }, "INTEX2_06_Legos_product_ID")
                        .IsUnique();

                    b.ToTable("Legos");
                });

            modelBuilder.Entity("INTEX2_06.Models.LineItem", b =>
                {
                    b.Property<int>("transaction_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("transaction_ID"));

                    b.Property<int>("product_ID")
                        .HasColumnType("int");

                    b.Property<int>("qty")
                        .HasColumnType("int");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.HasKey("transaction_ID");

                    b.ToTable("LineItems");
                });

            modelBuilder.Entity("INTEX2_06.Models.Order", b =>
                {
                    b.Property<int>("transaction_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("transaction_ID"));

                    b.Property<int>("amount")
                        .HasColumnType("int");

                    b.Property<string>("bank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("complete")
                        .HasColumnType("bit");

                    b.Property<string>("country_of_transaction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("customer_ID")
                        .HasColumnType("int");

                    b.Property<string>("date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("day_of_week")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("entry_mode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("fraud")
                        .HasColumnType("bit");

                    b.Property<bool>("predict_fraud")
                        .HasColumnType("bit");

                    b.Property<string>("shipping_address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type_of_card")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type_of_transaction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("transaction_ID");

                    b.ToTable("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
