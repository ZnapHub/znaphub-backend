using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZnapHub.Infrastructure.Data.Migrations.Write
{
    /// <inheritdoc />
    public partial class RemoveEventTimeRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndsAt",
                schema: "public",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartsAt",
                schema: "public",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Photos",
                schema: "public",
                newName: "Photos");

            migrationBuilder.RenameTable(
                name: "Events",
                schema: "public",
                newName: "Events");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "Photos",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Events",
                newSchema: "public");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EndsAt",
                schema: "public",
                table: "Events",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartsAt",
                schema: "public",
                table: "Events",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
