﻿// <auto-generated />
using System;
using LightStore.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LightStore.Persistence.Migrations
{
    [DbContext(typeof(LightStoreDbContext))]
    [Migration("20221226154846_m1")]
    partial class m1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LightStore.Persistence.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasMaxLength(72)
                        .HasColumnType("varbinary(72)");

                    b.Property<byte>("Role")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.DeliveryInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("DeliveryInformation");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Cost")
                        .HasPrecision(15, 2)
                        .HasColumnType("decimal(15,2)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DeliveryInformationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DeliveryInformationId")
                        .IsUnique()
                        .HasFilter("[DeliveryInformationId] IS NOT NULL");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<decimal>("Price")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("UnitOfMeasure")
                        .HasColumnType("tinyint");

                    b.Property<double>("Weight")
                        .HasPrecision(7, 3)
                        .HasColumnType("float(7)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.ProductInAdding", b =>
                {
                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductAddingID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<Guid?>("ProductsAddingId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductID", "ProductAddingID")
                        .IsClustered();

                    b.HasIndex("ProductsAddingId");

                    b.HasIndex("ProductID", "ProductAddingID")
                        .IsUnique();

                    b.ToTable("ProductsInAddings");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.ProductInOrder", b =>
                {
                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.HasKey("ProductID", "OrderID")
                        .IsClustered();

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID", "OrderID")
                        .IsUnique();

                    b.ToTable("ProductsInOrders");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.ProductInWarehouse", b =>
                {
                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WarehouseID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.HasKey("ProductID", "WarehouseID")
                        .IsClustered();

                    b.HasIndex("WarehouseID");

                    b.HasIndex("ProductID", "WarehouseID")
                        .IsUnique();

                    b.ToTable("ProductsInWarehouses");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.ProductsAdding", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("ProductsAddings");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.ProductsCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("ProductsCategorys");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.Warehouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.Customer", b =>
                {
                    b.HasOne("LightStore.Persistence.Entities.AppUser", "AppUser")
                        .WithOne("Customer")
                        .HasForeignKey("LightStore.Persistence.Entities.Customer", "AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.Employee", b =>
                {
                    b.HasOne("LightStore.Persistence.Entities.AppUser", "AppUser")
                        .WithOne("Employee")
                        .HasForeignKey("LightStore.Persistence.Entities.Employee", "AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.Order", b =>
                {
                    b.HasOne("LightStore.Persistence.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");

                    b.HasOne("LightStore.Persistence.Entities.DeliveryInformation", "DeliveryInformation")
                        .WithOne("Order")
                        .HasForeignKey("LightStore.Persistence.Entities.Order", "DeliveryInformationId");

                    b.Navigation("Customer");

                    b.Navigation("DeliveryInformation");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.Product", b =>
                {
                    b.HasOne("LightStore.Persistence.Entities.ProductsCategory", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.ProductInAdding", b =>
                {
                    b.HasOne("LightStore.Persistence.Entities.Product", "Product")
                        .WithMany("ProductInAddings")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LightStore.Persistence.Entities.ProductsAdding", "ProductsAdding")
                        .WithMany("ProductsInAdding")
                        .HasForeignKey("ProductsAddingId");

                    b.Navigation("Product");

                    b.Navigation("ProductsAdding");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.ProductInOrder", b =>
                {
                    b.HasOne("LightStore.Persistence.Entities.Order", "Order")
                        .WithMany("ProductsInOrder")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LightStore.Persistence.Entities.Product", "Product")
                        .WithMany("ProductInOrders")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.ProductInWarehouse", b =>
                {
                    b.HasOne("LightStore.Persistence.Entities.Product", "Product")
                        .WithMany("ProductInWarehouses")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LightStore.Persistence.Entities.Warehouse", "Warehouse")
                        .WithMany("ProductsInWarehouse")
                        .HasForeignKey("WarehouseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.ProductsAdding", b =>
                {
                    b.HasOne("LightStore.Persistence.Entities.Employee", "Employee")
                        .WithMany("ProductAddings")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.ProductsCategory", b =>
                {
                    b.HasOne("LightStore.Persistence.Entities.ProductsCategory", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.AppUser", b =>
                {
                    b.Navigation("Customer");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.DeliveryInformation", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.Employee", b =>
                {
                    b.Navigation("ProductAddings");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.Order", b =>
                {
                    b.Navigation("ProductsInOrder");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.Product", b =>
                {
                    b.Navigation("ProductInAddings");

                    b.Navigation("ProductInOrders");

                    b.Navigation("ProductInWarehouses");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.ProductsAdding", b =>
                {
                    b.Navigation("ProductsInAdding");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.ProductsCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("LightStore.Persistence.Entities.Warehouse", b =>
                {
                    b.Navigation("ProductsInWarehouse");
                });
#pragma warning restore 612, 618
        }
    }
}
