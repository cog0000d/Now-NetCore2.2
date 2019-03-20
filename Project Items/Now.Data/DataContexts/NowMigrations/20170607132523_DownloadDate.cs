using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Now.Data.DataContexts.NowMigrations
{
    public partial class DownloadDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                "DownloadDate",
                schema: "Now",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "DownloadDate",
                schema: "Now",
                table: "Logs");
        }
    }
}