﻿// <auto-generated />
using System;
using EpMon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EpMon.Data.Migrations
{
    [DbContext(typeof(EpMonContext))]
    partial class EpMonContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EpMon.Data.Entities.Endpoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CheckInterval");

                    b.Property<int>("CheckType");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsCritical");

                    b.Property<string>("Name");

                    b.Property<string>("Tags");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Endpoints");
                });

            modelBuilder.Entity("EpMon.Data.Entities.EndpointStat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EndpointId");

                    b.Property<bool>("IsHealthy");

                    b.Property<string>("Message");

                    b.Property<long>("ResponseTime");

                    b.Property<int>("Status");

                    b.Property<DateTime>("TimeStamp");

                    b.HasKey("Id");

                    b.HasIndex("EndpointId");

                    b.ToTable("EndpointStats");
                });

            modelBuilder.Entity("EpMon.Data.Entities.EndpointStat", b =>
                {
                    b.HasOne("EpMon.Data.Entities.Endpoint", "Endpoint")
                        .WithMany("Stats")
                        .HasForeignKey("EndpointId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
