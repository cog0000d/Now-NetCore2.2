﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Now.Data.DataContexts.Schedule;

namespace Now.Data.DataContexts.Schedule.ScheduleDbMigrations
{
    [DbContext(typeof(ScheduleDbContext))]
    [Migration("20190214061356_deleteEffectiveDate")]
    partial class deleteEffectiveDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Schedule")
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Now.Entities.Models.Schedule.Schedule", b =>
                {
                    b.Property<Guid>("ScheduleId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AddedBy");

                    b.Property<DateTimeOffset?>("AddedDate");

                    b.Property<string>("Employee")
                        .IsRequired();

                    b.Property<DateTimeOffset>("EndRange");

                    b.Property<DateTimeOffset>("EndTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("MemoryConsumption");

                    b.Property<Guid?>("ModifiedBy");

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<Guid>("ShiftId");

                    b.Property<DateTimeOffset>("StartRange");

                    b.Property<DateTimeOffset>("StartTime");

                    b.HasKey("ScheduleId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Now.Entities.Models.Schedule.Shift", b =>
                {
                    b.Property<Guid>("ShiftId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AddedBy");

                    b.Property<DateTimeOffset?>("AddedDate");

                    b.Property<DateTimeOffset>("EndTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("MemoryConsumption");

                    b.Property<Guid?>("ModifiedBy");

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("ShiftDescription");

                    b.Property<string>("ShiftName")
                        .IsRequired();

                    b.Property<Guid>("SiteId");

                    b.Property<DateTimeOffset>("StartTime");

                    b.Property<long>("Ticks");

                    b.HasKey("ShiftId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("Now.Entities.Models.Schedule.ShiftDetail", b =>
                {
                    b.Property<Guid>("ShiftDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AddedBy");

                    b.Property<DateTimeOffset?>("AddedDate");

                    b.Property<DateTimeOffset>("Duration");

                    b.Property<DateTimeOffset>("EndRange");

                    b.Property<DateTimeOffset>("EndTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("MemoryConsumption");

                    b.Property<Guid?>("ModifiedBy");

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("ShiftDescription");

                    b.Property<string>("ShiftDetailName");

                    b.Property<Guid>("ShiftId");

                    b.Property<DateTimeOffset>("StartRange");

                    b.Property<DateTimeOffset>("StartTime");

                    b.Property<Guid?>("TypesShiftDetailTypeId");

                    b.HasKey("ShiftDetailId");

                    b.HasIndex("ShiftId");

                    b.HasIndex("TypesShiftDetailTypeId");

                    b.ToTable("ShiftDetail");
                });

            modelBuilder.Entity("Now.Entities.Models.Schedule.ShiftDetailType", b =>
                {
                    b.Property<Guid>("ShiftDetailTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("Active");

                    b.Property<Guid?>("AddedBy");

                    b.Property<DateTimeOffset?>("AddedDate");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("MemoryConsumption");

                    b.Property<Guid?>("ModifiedBy");

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("ShiftDetailTypeId");

                    b.ToTable("ShiftDetailType");
                });

            modelBuilder.Entity("Now.Entities.Models.Schedule.ShiftDetail", b =>
                {
                    b.HasOne("Now.Entities.Models.Schedule.Shift", "Shifts")
                        .WithMany("ShiftDetails")
                        .HasForeignKey("ShiftId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Now.Entities.Models.Schedule.ShiftDetailType", "Types")
                        .WithMany()
                        .HasForeignKey("TypesShiftDetailTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
