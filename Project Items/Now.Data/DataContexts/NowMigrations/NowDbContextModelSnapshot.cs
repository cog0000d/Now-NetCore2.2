﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Now.Data.DataContexts.NowMigrations
{
    [DbContext(typeof(NowDbContext))]
    internal class NowDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Now")
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                b.Property<string>("ShiftName");

                b.Property<Guid>("SiteId");

                b.Property<DateTimeOffset>("StartTime");

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

                b.Property<Guid?>("TypesTypeId");

                b.HasKey("ShiftDetailId");

                b.HasIndex("ShiftId");

                b.HasIndex("TypesTypeId");

                b.ToTable("ShiftDetails");
            });

            modelBuilder.Entity("Now.Entities.Models.Schedule.Type", b =>
            {
                b.Property<Guid>("TypeId")
                    .ValueGeneratedOnAdd();

                b.Property<short>("Active");

                b.Property<Guid?>("AddedBy");

                b.Property<DateTimeOffset?>("AddedDate");

                b.Property<Guid>("EntityId");

                b.Property<bool>("IsDeleted");

                b.Property<int>("MemoryConsumption");

                b.Property<Guid?>("ModifiedBy");

                b.Property<DateTimeOffset?>("ModifiedDate");

                b.Property<string>("TypeDescription");

                b.Property<string>("TypeName");

                b.HasKey("TypeId");

                b.ToTable("Type");
            });

            modelBuilder.Entity("Now.Entities.Models.Time.Log", b =>
            {
                b.Property<Guid>("LogId")
                    .ValueGeneratedOnAdd();

                b.Property<DateTimeOffset>("Data");

                b.Property<DateTimeOffset>("DownloadDate");

                b.Property<Guid>("SourceId");

                b.Property<string>("Type")
                    .IsRequired()
                    .HasMaxLength(100);

                b.Property<string>("Value")
                    .IsRequired()
                    .HasMaxLength(100);

                b.HasKey("LogId");

                b.HasIndex("SourceId");

                b.ToTable("Logs");
            });

            modelBuilder.Entity("Now.Entities.Models.Time.LogDetail", b =>
            {
                b.Property<Guid>("LogDetailId")
                    .ValueGeneratedOnAdd();

                b.Property<Guid>("LogId");

                b.Property<string>("Type")
                    .IsRequired()
                    .HasMaxLength(256);

                b.Property<string>("Value")
                    .IsRequired()
                    .HasMaxLength(256);

                b.HasKey("LogDetailId");

                b.HasIndex("LogId");

                b.ToTable("LogDetails");
            });

            modelBuilder.Entity("Now.Entities.Models.Time.Source", b =>
            {
                b.Property<Guid>("SourceId")
                    .ValueGeneratedOnAdd();

                b.Property<Guid?>("AddedBy");

                b.Property<DateTimeOffset?>("AddedDate");

                b.Property<bool>("IsDeleted");

                b.Property<int>("MemoryConsumption");

                b.Property<Guid?>("ModifiedBy");

                b.Property<DateTimeOffset?>("ModifiedDate");

                b.Property<string>("SourceDescription")
                    .IsRequired()
                    .HasMaxLength(4000);

                b.Property<string>("SourceName")
                    .IsRequired()
                    .HasMaxLength(150);

                b.Property<Guid?>("TenantId");

                b.HasKey("SourceId");

                b.ToTable("Sources");
            });

            modelBuilder.Entity("Now.Entities.Models.Time.Type", b =>
            {
                b.Property<Guid>("TypeId")
                    .ValueGeneratedOnAdd();

                b.Property<Guid?>("AddedBy");

                b.Property<DateTimeOffset?>("AddedDate");

                b.Property<bool>("IsDeleted");

                b.Property<int>("MemoryConsumption");

                b.Property<Guid?>("ModifiedBy");

                b.Property<DateTimeOffset?>("ModifiedDate");

                b.Property<Guid?>("TenantId");

                b.Property<string>("TypeDescription")
                    .IsRequired()
                    .HasMaxLength(4000);

                b.Property<string>("TypeName")
                    .IsRequired()
                    .HasMaxLength(150);

                b.HasKey("TypeId");

                b.ToTable("Types");
            });

            modelBuilder.Entity("Now.Entities.Models.Schedule.ShiftDetail", b =>
            {
                b.HasOne("Now.Entities.Models.Schedule.Shift", "Shifts")
                    .WithMany("ShiftDetails")
                    .HasForeignKey("ShiftId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("Now.Entities.Models.Schedule.Type", "Types")
                    .WithMany()
                    .HasForeignKey("TypesTypeId");
            });

            modelBuilder.Entity("Now.Entities.Models.Time.Log", b =>
            {
                b.HasOne("Now.Entities.Models.Time.Source")
                    .WithMany("Logs")
                    .HasForeignKey("SourceId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Now.Entities.Models.Time.LogDetail", b =>
            {
                b.HasOne("Now.Entities.Models.Time.Log", "Logs")
                    .WithMany("LogDetails")
                    .HasForeignKey("LogId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
#pragma warning restore 612, 618
        }
    }
}