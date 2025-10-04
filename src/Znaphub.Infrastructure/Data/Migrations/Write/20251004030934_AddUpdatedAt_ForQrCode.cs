using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZnapHub.Infrastructure.Data.Migrations.Write
{
    /// <inheritdoc />
    public partial class AddUpdatedAt_ForQrCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "QrCodes",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "QrCodes");
        }
    }
}
