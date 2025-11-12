using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZnapHub.Infrastructure.Data.Migrations.Write
{
    /// <inheritdoc />
    public partial class Unknown : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Url", table: "Photos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Photos",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: ""
            );
        }
    }
}
