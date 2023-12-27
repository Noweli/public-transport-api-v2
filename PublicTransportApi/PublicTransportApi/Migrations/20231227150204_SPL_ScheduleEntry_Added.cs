using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicTransportApi.Migrations
{
    /// <inheritdoc />
    public partial class SPL_ScheduleEntry_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Long",
                table: "StopPoints",
                type: "TEXT",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Lat",
                table: "StopPoints",
                type: "TEXT",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "StopPointLineCorrelation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LineId = table.Column<int>(type: "INTEGER", nullable: true),
                    StopPointId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StopPointLineCorrelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StopPointLineCorrelation_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StopPointLineCorrelation_StopPoints_StopPointId",
                        column: x => x.StopPointId,
                        principalTable: "StopPoints",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScheduleEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsRecurring = table.Column<bool>(type: "INTEGER", nullable: false),
                    RecurringDays = table.Column<string>(type: "TEXT", nullable: true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SPLCorrelationId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleEntry_StopPointLineCorrelation_SPLCorrelationId",
                        column: x => x.SPLCorrelationId,
                        principalTable: "StopPointLineCorrelation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEntry_SPLCorrelationId",
                table: "ScheduleEntry",
                column: "SPLCorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_StopPointLineCorrelation_LineId",
                table: "StopPointLineCorrelation",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_StopPointLineCorrelation_StopPointId",
                table: "StopPointLineCorrelation",
                column: "StopPointId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleEntry");

            migrationBuilder.DropTable(
                name: "StopPointLineCorrelation");

            migrationBuilder.AlterColumn<int>(
                name: "Long",
                table: "StopPoints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Lat",
                table: "StopPoints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 30,
                oldNullable: true);
        }
    }
}
