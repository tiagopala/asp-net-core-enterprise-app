﻿// <auto-generated />
using System;
using EnterpriseApp.Carrinho.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EnterpriseApp.Carrinho.API.Migrations
{
    [DbContext(typeof(ShoppingCartContext))]
    [Migration("20220309110527_One")]
    partial class One
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EnterpriseApp.Carrinho.API.Models.ShoppingCartCustomer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("HasUsedVoucher")
                        .HasColumnType("bit");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("IDX_Customer");

                    b.ToTable("CartCustomer");
                });

            modelBuilder.Entity("EnterpriseApp.Carrinho.API.Models.ShoppingCartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("ShoppingCartId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("EnterpriseApp.Carrinho.API.Models.ShoppingCartCustomer", b =>
                {
                    b.OwnsOne("EnterpriseApp.Carrinho.API.Models.Voucher", "Voucher", b1 =>
                        {
                            b1.Property<Guid>("ShoppingCartCustomerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Code")
                                .HasColumnType("varchar(50)")
                                .HasColumnName("VoucherCode");

                            b1.Property<int>("DiscountType")
                                .HasColumnType("int")
                                .HasColumnName("DiscountType");

                            b1.Property<decimal?>("DiscountValue")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("DiscountValue");

                            b1.Property<decimal?>("Percent")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Percent");

                            b1.HasKey("ShoppingCartCustomerId");

                            b1.ToTable("CartCustomer");

                            b1.WithOwner()
                                .HasForeignKey("ShoppingCartCustomerId");
                        });

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("EnterpriseApp.Carrinho.API.Models.ShoppingCartItem", b =>
                {
                    b.HasOne("EnterpriseApp.Carrinho.API.Models.ShoppingCartCustomer", "ShoppingCartCustomer")
                        .WithMany("Items")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingCartCustomer");
                });

            modelBuilder.Entity("EnterpriseApp.Carrinho.API.Models.ShoppingCartCustomer", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}