﻿// <auto-generated />
using Mango.Servises.CouponAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mango.Servises.CouponAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230507102041_SecondCouponToDb")]
    partial class SecondCouponToDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mango.Servises.CouponAPI.Model.Coupon", b =>
                {
                    b.Property<long>("CouponId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CouponId"));

                    b.Property<decimal>("CouponAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CouponCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CouponId");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            CouponId = 1L,
                            CouponAmount = 10m,
                            CouponCode = "10OFF"
                        },
                        new
                        {
                            CouponId = 2L,
                            CouponAmount = 20m,
                            CouponCode = "20OFF"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
