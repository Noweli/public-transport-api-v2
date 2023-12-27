using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicTransportApi.Migrations
{
    /// <inheritdoc />
    public partial class StopPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StreetName",
                table: "StopPoints",
                type: "TEXT",
                maxLength: 120,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StreetName",
                table: "StopPoints");
        }
    }
}
