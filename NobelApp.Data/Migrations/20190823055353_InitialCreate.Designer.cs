﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NobelApp.Data;

namespace NobelApp.Data.Migrations
{
    [DbContext(typeof(NobelContext))]
    [Migration("20190823055353_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NobelApp.Domain.Laureate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BirthCity");

                    b.Property<string>("BirthCountry");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("FullName");

                    b.Property<int?>("OrganisationId");

                    b.Property<int?>("PrizeId");

                    b.Property<int>("Sex");

                    b.HasKey("Id");

                    b.HasIndex("OrganisationId");

                    b.HasIndex("PrizeId");

                    b.ToTable("Laureates");
                });

            modelBuilder.Entity("NobelApp.Domain.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("NobelApp.Domain.Prize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Category");

                    b.Property<string>("Motivation");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Prizes");
                });

            modelBuilder.Entity("NobelApp.Domain.Laureate", b =>
                {
                    b.HasOne("NobelApp.Domain.Organization", "Organisation")
                        .WithMany()
                        .HasForeignKey("OrganisationId");

                    b.HasOne("NobelApp.Domain.Prize")
                        .WithMany("Laureates")
                        .HasForeignKey("PrizeId");
                });
#pragma warning restore 612, 618
        }
    }
}
