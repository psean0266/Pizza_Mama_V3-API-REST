﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pizza_mama.Data;

#nullable disable

namespace pizza_mama_V2.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.25");

            modelBuilder.Entity("pizza_mama_V2.Models.Account", b =>
                {
                    b.Property<string>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Roles")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("pizza_mama.Models.Pizza", b =>
                {
                    b.Property<int>("PizzaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ingredients")
                        .HasColumnType("TEXT");

                    b.Property<string>("nom")
                        .HasColumnType("TEXT");

                    b.Property<float>("prix")
                        .HasColumnType("REAL");

                    b.Property<bool>("vegetarienne")
                        .HasColumnType("INTEGER");

                    b.HasKey("PizzaId");

                    b.ToTable("Pizzas");
                });
#pragma warning restore 612, 618
        }
    }
}
