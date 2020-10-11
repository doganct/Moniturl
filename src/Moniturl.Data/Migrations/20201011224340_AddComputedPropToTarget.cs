using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Moniturl.Data.Migrations
{
    public partial class AddComputedPropToTarget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastRequestTime",
                table: "Targets",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PassedMinutes",
                table: "Targets",
                nullable: false,
                computedColumnSql: "DATEDIFF(MINUTE, LastRequestTime, GETDATE())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassedMinutes",
                table: "Targets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastRequestTime",
                table: "Targets",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
