using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementDL.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Drop constraint
            migrationBuilder.DropCheckConstraint(
                name: "DateCheck",
                table: "Sessions");

            // 2. Alter column
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            // 3. Re-add constraint
            migrationBuilder.AddCheckConstraint(
                name: "DateCheck",
                table: "Sessions",
                sql: "EndDate > StartDate");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "DateCheck",
                table: "Sessions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Sessions",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Sessions",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddCheckConstraint(
                name: "DateCheck",
                table: "Sessions",
                sql: "EndDate > StartDate");
        }

    }
}
