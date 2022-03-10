using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderYourChow.DAL.CORE.Migrations
{
    public partial class AddedForeignKeysInNewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_D_DIET_DAY_S_DATE_DAY_SDateDayDateDayId",
                table: "D_DIET_DAY");

            migrationBuilder.DropForeignKey(
                name: "FK_S_DATE_DAY_S_DAY_SDayDayId",
                table: "S_DATE_DAY");

            migrationBuilder.DropIndex(
                name: "IX_S_DATE_DAY_SDayDayId",
                table: "S_DATE_DAY");

            migrationBuilder.DropIndex(
                name: "IX_D_DIET_DAY_SDateDayDateDayId",
                table: "D_DIET_DAY");

            migrationBuilder.DropColumn(
                name: "SDayDayId",
                table: "S_DATE_DAY");

            migrationBuilder.DropColumn(
                name: "SDateDayDateDayId",
                table: "D_DIET_DAY");

            migrationBuilder.CreateIndex(
                name: "IX_S_DATE_DAY_DayId",
                table: "S_DATE_DAY",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_D_DIET_DAY_DateDayId",
                table: "D_DIET_DAY",
                column: "DateDayId");

            migrationBuilder.AddForeignKey(
                name: "FK__D_DIET_DAY__S_DATE_DAY",
                table: "D_DIET_DAY",
                column: "DateDayId",
                principalTable: "S_DATE_DAY",
                principalColumn: "DateDayId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__S_DATE_DAY__S_DAY",
                table: "S_DATE_DAY",
                column: "DayId",
                principalTable: "S_DAY",
                principalColumn: "DayId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__D_DIET_DAY__S_DATE_DAY",
                table: "D_DIET_DAY");

            migrationBuilder.DropForeignKey(
                name: "FK__S_DATE_DAY__S_DAY",
                table: "S_DATE_DAY");

            migrationBuilder.DropIndex(
                name: "IX_S_DATE_DAY_DayId",
                table: "S_DATE_DAY");

            migrationBuilder.DropIndex(
                name: "IX_D_DIET_DAY_DateDayId",
                table: "D_DIET_DAY");

            migrationBuilder.AddColumn<int>(
                name: "SDayDayId",
                table: "S_DATE_DAY",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SDateDayDateDayId",
                table: "D_DIET_DAY",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_S_DATE_DAY_SDayDayId",
                table: "S_DATE_DAY",
                column: "SDayDayId");

            migrationBuilder.CreateIndex(
                name: "IX_D_DIET_DAY_SDateDayDateDayId",
                table: "D_DIET_DAY",
                column: "SDateDayDateDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_D_DIET_DAY_S_DATE_DAY_SDateDayDateDayId",
                table: "D_DIET_DAY",
                column: "SDateDayDateDayId",
                principalTable: "S_DATE_DAY",
                principalColumn: "DateDayId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_S_DATE_DAY_S_DAY_SDayDayId",
                table: "S_DATE_DAY",
                column: "SDayDayId",
                principalTable: "S_DAY",
                principalColumn: "DayId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
