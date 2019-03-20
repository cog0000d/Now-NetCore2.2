using Microsoft.EntityFrameworkCore.Migrations;

namespace Now.Data.DataContexts.Schedule.ScheduleDbMigrations
{
    public partial class Add_OrderNo_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderNo",
                schema: "Schedule",
                table: "Schedules",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNo",
                schema: "Schedule",
                table: "Schedules");
        }
    }
}
