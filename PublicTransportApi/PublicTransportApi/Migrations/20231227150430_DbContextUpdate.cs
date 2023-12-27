using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicTransportApi.Migrations
{
    /// <inheritdoc />
    public partial class DbContextUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleEntry_StopPointLineCorrelation_SPLCorrelationId",
                table: "ScheduleEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_StopPointLineCorrelation_Lines_LineId",
                table: "StopPointLineCorrelation");

            migrationBuilder.DropForeignKey(
                name: "FK_StopPointLineCorrelation_StopPoints_StopPointId",
                table: "StopPointLineCorrelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StopPointLineCorrelation",
                table: "StopPointLineCorrelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleEntry",
                table: "ScheduleEntry");

            migrationBuilder.RenameTable(
                name: "StopPointLineCorrelation",
                newName: "StopPointLineCorrelations");

            migrationBuilder.RenameTable(
                name: "ScheduleEntry",
                newName: "ScheduleEntries");

            migrationBuilder.RenameIndex(
                name: "IX_StopPointLineCorrelation_StopPointId",
                table: "StopPointLineCorrelations",
                newName: "IX_StopPointLineCorrelations_StopPointId");

            migrationBuilder.RenameIndex(
                name: "IX_StopPointLineCorrelation_LineId",
                table: "StopPointLineCorrelations",
                newName: "IX_StopPointLineCorrelations_LineId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleEntry_SPLCorrelationId",
                table: "ScheduleEntries",
                newName: "IX_ScheduleEntries_SPLCorrelationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StopPointLineCorrelations",
                table: "StopPointLineCorrelations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleEntries",
                table: "ScheduleEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleEntries_StopPointLineCorrelations_SPLCorrelationId",
                table: "ScheduleEntries",
                column: "SPLCorrelationId",
                principalTable: "StopPointLineCorrelations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StopPointLineCorrelations_Lines_LineId",
                table: "StopPointLineCorrelations",
                column: "LineId",
                principalTable: "Lines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StopPointLineCorrelations_StopPoints_StopPointId",
                table: "StopPointLineCorrelations",
                column: "StopPointId",
                principalTable: "StopPoints",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleEntries_StopPointLineCorrelations_SPLCorrelationId",
                table: "ScheduleEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_StopPointLineCorrelations_Lines_LineId",
                table: "StopPointLineCorrelations");

            migrationBuilder.DropForeignKey(
                name: "FK_StopPointLineCorrelations_StopPoints_StopPointId",
                table: "StopPointLineCorrelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StopPointLineCorrelations",
                table: "StopPointLineCorrelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleEntries",
                table: "ScheduleEntries");

            migrationBuilder.RenameTable(
                name: "StopPointLineCorrelations",
                newName: "StopPointLineCorrelation");

            migrationBuilder.RenameTable(
                name: "ScheduleEntries",
                newName: "ScheduleEntry");

            migrationBuilder.RenameIndex(
                name: "IX_StopPointLineCorrelations_StopPointId",
                table: "StopPointLineCorrelation",
                newName: "IX_StopPointLineCorrelation_StopPointId");

            migrationBuilder.RenameIndex(
                name: "IX_StopPointLineCorrelations_LineId",
                table: "StopPointLineCorrelation",
                newName: "IX_StopPointLineCorrelation_LineId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleEntries_SPLCorrelationId",
                table: "ScheduleEntry",
                newName: "IX_ScheduleEntry_SPLCorrelationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StopPointLineCorrelation",
                table: "StopPointLineCorrelation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleEntry",
                table: "ScheduleEntry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleEntry_StopPointLineCorrelation_SPLCorrelationId",
                table: "ScheduleEntry",
                column: "SPLCorrelationId",
                principalTable: "StopPointLineCorrelation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StopPointLineCorrelation_Lines_LineId",
                table: "StopPointLineCorrelation",
                column: "LineId",
                principalTable: "Lines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StopPointLineCorrelation_StopPoints_StopPointId",
                table: "StopPointLineCorrelation",
                column: "StopPointId",
                principalTable: "StopPoints",
                principalColumn: "Id");
        }
    }
}
