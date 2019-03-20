using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Now.Data.DataContexts.NowMigrations
{
    public partial class AddShiftsAndDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "MemoryConsumption",
                schema: "Now",
                table: "Types",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "MemoryConsumption",
                schema: "Now",
                table: "Sources",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                "Shifts",
                schema: "Now",
                columns: table => new
                {
                    ShiftId = table.Column<Guid>(nullable: false),
                    AddedDate = table.Column<DateTimeOffset>(nullable: true),
                    AddedBy = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MemoryConsumption = table.Column<int>(nullable: false),
                    SiteId = table.Column<Guid>(nullable: false),
                    ShiftName = table.Column<string>(nullable: true),
                    ShiftDescription = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTimeOffset>(nullable: false),
                    EndTime = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Shifts", x => x.ShiftId); });

            migrationBuilder.CreateTable(
                "Type",
                schema: "Now",
                columns: table => new
                {
                    TypeId = table.Column<Guid>(nullable: false),
                    AddedDate = table.Column<DateTimeOffset>(nullable: true),
                    AddedBy = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TypeName = table.Column<string>(nullable: true),
                    TypeDescription = table.Column<string>(nullable: true),
                    Active = table.Column<short>(nullable: false),
                    MemoryConsumption = table.Column<int>(nullable: false),
                    EntityId = table.Column<Guid>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Type", x => x.TypeId); });

            migrationBuilder.CreateTable(
                "ShiftDetails",
                schema: "Now",
                columns: table => new
                {
                    ShiftDetailId = table.Column<Guid>(nullable: false),
                    AddedDate = table.Column<DateTimeOffset>(nullable: true),
                    AddedBy = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MemoryConsumption = table.Column<int>(nullable: false),
                    ShiftId = table.Column<Guid>(nullable: false),
                    ShiftDetailName = table.Column<string>(nullable: true),
                    ShiftDescription = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTimeOffset>(nullable: false),
                    EndTime = table.Column<DateTimeOffset>(nullable: false),
                    Duration = table.Column<DateTimeOffset>(nullable: false),
                    StartRange = table.Column<DateTimeOffset>(nullable: false),
                    EndRange = table.Column<DateTimeOffset>(nullable: false),
                    TypesTypeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftDetails", x => x.ShiftDetailId);
                    table.ForeignKey(
                        "FK_ShiftDetails_Shifts_ShiftId",
                        x => x.ShiftId,
                        principalSchema: "Now",
                        principalTable: "Shifts",
                        principalColumn: "ShiftId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ShiftDetails_Type_TypesTypeId",
                        x => x.TypesTypeId,
                        principalSchema: "Now",
                        principalTable: "Type",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_ShiftDetails_ShiftId",
                schema: "Now",
                table: "ShiftDetails",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                "IX_ShiftDetails_TypesTypeId",
                schema: "Now",
                table: "ShiftDetails",
                column: "TypesTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "ShiftDetails",
                "Now");

            migrationBuilder.DropTable(
                "Shifts",
                "Now");

            migrationBuilder.DropTable(
                "Type",
                "Now");

            migrationBuilder.DropColumn(
                "MemoryConsumption",
                schema: "Now",
                table: "Types");

            migrationBuilder.DropColumn(
                "MemoryConsumption",
                schema: "Now",
                table: "Sources");
        }
    }
}