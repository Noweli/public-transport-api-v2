using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicTransportApi.Migrations
{
    /// <inheritdoc />
    public partial class StopPointAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StopPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Lat = table.Column<int>(type: "INTEGER", nullable: false),
                    Long = table.Column<int>(type: "INTEGER", nullable: false),
                    Identifier = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StopPoints", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StopPoints");
        }
    }
}
