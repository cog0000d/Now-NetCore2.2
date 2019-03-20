using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Now.Data.DataContexts.NowMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                "Now");

            migrationBuilder.CreateTable(
                "Sources",
                schema: "Now",
                columns: table => new
                {
                    SourceId = table.Column<Guid>(nullable: false),
                    AddedBy = table.Column<Guid>(nullable: true),
                    AddedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    SourceDescription = table.Column<string>(maxLength: 4000, nullable: false),
                    SourceName = table.Column<string>(maxLength: 150, nullable: false),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Sources", x => x.SourceId); });

            migrationBuilder.CreateTable(
                "Types",
                schema: "Now",
                columns: table => new
                {
                    TypeId = table.Column<Guid>(nullable: false),
                    AddedBy = table.Column<Guid>(nullable: true),
                    AddedDate = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    TypeDescription = table.Column<string>(maxLength: 4000, nullable: false),
                    TypeName = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Types", x => x.TypeId); });

            migrationBuilder.CreateTable(
                "Logs",
                schema: "Now",
                columns: table => new
                {
                    LogId = table.Column<Guid>(nullable: false),
                    Data = table.Column<DateTimeOffset>(nullable: false),
                    SourceId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(maxLength: 100, nullable: false),
                    Value = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.LogId);
                    table.ForeignKey(
                        "FK_Logs_Sources_SourceId",
                        x => x.SourceId,
                        principalSchema: "Now",
                        principalTable: "Sources",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "LogDetails",
                schema: "Now",
                columns: table => new
                {
                    LogDetailId = table.Column<Guid>(nullable: false),
                    LogId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(maxLength: 256, nullable: false),
                    Value = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogDetails", x => x.LogDetailId);
                    table.ForeignKey(
                        "FK_LogDetails_Logs_LogId",
                        x => x.LogId,
                        principalSchema: "Now",
                        principalTable: "Logs",
                        principalColumn: "LogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Logs_SourceId",
                schema: "Now",
                table: "Logs",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                "IX_LogDetails_LogId",
                schema: "Now",
                table: "LogDetails",
                column: "LogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "LogDetails",
                "Now");

            migrationBuilder.DropTable(
                "Types",
                "Now");

            migrationBuilder.DropTable(
                "Logs",
                "Now");

            migrationBuilder.DropTable(
                "Sources",
                "Now");
        }
    }
}