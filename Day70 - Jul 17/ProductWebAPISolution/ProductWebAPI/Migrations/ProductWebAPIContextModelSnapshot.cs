﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductWebAPI.Contexts;

#nullable disable

namespace ProductWebAPI.Migrations
{
    [DbContext(typeof(ProductWebAPIContext))]
    partial class ProductWebAPIContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ProductWebAPI.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 101,
                            ImageUrl = "https://kousikblob.blob.core.windows.net/images/sword.png",
                            Name = "Sword",
                            Price = 2000m
                        },
                        new
                        {
                            Id = 102,
                            ImageUrl = "https://kousikblob.blob.core.windows.net/images/laptop.png",
                            Name = "Laptop",
                            Price = 55000m
                        },
                        new
                        {
                            Id = 103,
                            ImageUrl = "https://kousikblob.blob.core.windows.net/images/dumbells.png",
                            Name = "Dumbells",
                            Price = 1500m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
