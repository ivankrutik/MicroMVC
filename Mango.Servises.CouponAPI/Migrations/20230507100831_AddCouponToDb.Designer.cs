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
    [Migration("20230507100831_AddCouponToDb")]
    partial class AddCouponToDb
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
                });
#pragma warning restore 612, 618
        }
    }
}
