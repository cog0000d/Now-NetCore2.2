using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Now.Data.DataContexts.Schedule.ScheduleDbMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Schedule");

            migrationBuilder.CreateTable(
                name: "Schedules",
                schema: "Schedule",
                columns: table => new
                {
                    AddedDate = table.Column<DateTimeOffset>(nullable: true),
                    AddedBy = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MemoryConsumption = table.Column<int>(nullable: false),
                    ScheduleId = table.Column<Guid>(nullable: false),
                    ShiftId = table.Column<Guid>(nullable: false),
                    Employee = table.Column<string>(nullable: false),
                    StartRange = table.Column<DateTimeOffset>(nullable: false),
                    StartTime = table.Column<DateTimeOffset>(nullable: false),
                    EndTime = table.Column<DateTimeOffset>(nullable: false),
                    EndRange = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "ShiftDetailType",
                schema: "Schedule",
                columns: table => new
                {
                    AddedDate = table.Column<DateTimeOffset>(nullable: true),
                    AddedBy = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MemoryConsumption = table.Column<int>(nullable: false),
                    ShiftDetailTypeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Active = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftDetailType", x => x.ShiftDetailTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                schema: "Schedule",
                columns: table => new
                {
                    AddedDate = table.Column<DateTimeOffset>(nullable: true),
                    AddedBy = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MemoryConsumption = table.Column<int>(nullable: false),
                    ShiftId = table.Column<Guid>(nullable: false),
                    SiteId = table.Column<Guid>(nullable: false),
                    ShiftName = table.Column<string>(nullable: false),
                    ShiftDescription = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTimeOffset>(nullable: false),
                    EndTime = table.Column<DateTimeOffset>(nullable: false),
                    Ticks = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.ShiftId);
                });

            migrationBuilder.CreateTable(
                name: "ShiftDetail",
                schema: "Schedule",
                columns: table => new
                {
                    AddedDate = table.Column<DateTimeOffset>(nullable: true),
                    AddedBy = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MemoryConsumption = table.Column<int>(nullable: false),
                    ShiftDetailId = table.Column<Guid>(nullable: false),
                    ShiftId = table.Column<Guid>(nullable: false),
                    ShiftDetailName = table.Column<string>(nullable: true),
                    ShiftDescription = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTimeOffset>(nullable: false),
                    EndTime = table.Column<DateTimeOffset>(nullable: false),
                    Duration = table.Column<DateTimeOffset>(nullable: false),
                    StartRange = table.Column<DateTimeOffset>(nullable: false),
                    EndRange = table.Column<DateTimeOffset>(nullable: false),
                    TypesShiftDetailTypeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftDetail", x => x.ShiftDetailId);
                    table.ForeignKey(
                        name: "FK_ShiftDetail_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalSchema: "Schedule",
                        principalTable: "Shifts",
                        principalColumn: "ShiftId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShiftDetail_ShiftDetailType_TypesShiftDetailTypeId",
                        column: x => x.TypesShiftDetailTypeId,
                        principalSchema: "Schedule",
                        principalTable: "ShiftDetailType",
                        principalColumn: "ShiftDetailTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShiftDetail_ShiftId",
                schema: "Schedule",
                table: "ShiftDetail",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftDetail_TypesShiftDetailTypeId",
                schema: "Schedule",
                table: "ShiftDetail",
                column: "TypesShiftDetailTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules",
                schema: "Schedule");

            migrationBuilder.DropTable(
                name: "ShiftDetail",
                schema: "Schedule");

            migrationBuilder.DropTable(
                name: "Shifts",
                schema: "Schedule");

            migrationBuilder.DropTable(
                name: "ShiftDetailType",
                schema: "Schedule");
        }
    }
}
