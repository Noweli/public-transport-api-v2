﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PublicTransportApi.Data;

#nullable disable

namespace PublicTransportApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("PublicTransportApi.Data.Models.Line", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Identifier")
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Lines");
                });

            modelBuilder.Entity("PublicTransportApi.Data.Models.ScheduleEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRecurring")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RecurringDays")
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<int?>("SPLCorrelationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SPLCorrelationId");

                    b.ToTable("ScheduleEntries");
                });

            modelBuilder.Entity("PublicTransportApi.Data.Models.StopPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Identifier")
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Lat")
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Long")
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<string>("StreetName")
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("StopPoints");
                });

            modelBuilder.Entity("PublicTransportApi.Data.Models.StopPointLineCorrelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LineId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StopPointId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LineId");

                    b.HasIndex("StopPointId");

                    b.ToTable("StopPointLineCorrelations");
                });

            modelBuilder.Entity("PublicTransportApi.Data.Models.ScheduleEntry", b =>
                {
                    b.HasOne("PublicTransportApi.Data.Models.StopPointLineCorrelation", "SPLCorrelation")
                        .WithMany("ScheduleEntries")
                        .HasForeignKey("SPLCorrelationId");

                    b.Navigation("SPLCorrelation");
                });

            modelBuilder.Entity("PublicTransportApi.Data.Models.StopPointLineCorrelation", b =>
                {
                    b.HasOne("PublicTransportApi.Data.Models.Line", "Line")
                        .WithMany("SPLCorrelations")
                        .HasForeignKey("LineId");

                    b.HasOne("PublicTransportApi.Data.Models.StopPoint", "StopPoint")
                        .WithMany("SPLCorrelation")
                        .HasForeignKey("StopPointId");

                    b.Navigation("Line");

                    b.Navigation("StopPoint");
                });

            modelBuilder.Entity("PublicTransportApi.Data.Models.Line", b =>
                {
                    b.Navigation("SPLCorrelations");
                });

            modelBuilder.Entity("PublicTransportApi.Data.Models.StopPoint", b =>
                {
                    b.Navigation("SPLCorrelation");
                });

            modelBuilder.Entity("PublicTransportApi.Data.Models.StopPointLineCorrelation", b =>
                {
                    b.Navigation("ScheduleEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
