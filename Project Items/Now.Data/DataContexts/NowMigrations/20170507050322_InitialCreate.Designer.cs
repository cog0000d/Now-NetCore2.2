using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Now.Data.DataContexts;

namespace Now.Data.DataContexts.NowMigrations
{
    [DbContext(typeof(NowDbContext))]
    [Migration("20170507050322_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("Now")
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Now.Entities.Models.Time.Log", b =>
                {
                    b.Property<Guid>("LogId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Data");

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
        }
    }
}
