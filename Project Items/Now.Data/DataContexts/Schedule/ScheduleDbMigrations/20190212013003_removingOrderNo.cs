using Microsoft.EntityFrameworkCore.Migrations;

namespace Now.Data.DataContexts.Schedule.ScheduleDbMigrations
{
    public partial class removingOrderNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNo",
                schema: "Schedule",
                table: "Schedules");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderNo",
                schema: "Schedule",
                table: "Schedules",
                nullable: false,
                defaultValue: 0);
        }
    }
}
