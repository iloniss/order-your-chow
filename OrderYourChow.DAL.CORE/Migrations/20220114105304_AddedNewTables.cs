using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderYourChow.DAL.CORE.Migrations
{
    public partial class AddedNewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "S_DATE_DAY",
                columns: table => new
                {
                    DateDayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    DateDay = table.Column<DateTime>(type: "date", nullable: false),
                    SDayDayId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__S_DATE_DAY__DateDayId", x => x.DateDayId);
                    table.ForeignKey(
                        name: "FK_S_DATE_DAY_S_DAY_SDayDayId",
                        column: x => x.SDayDayId,
                        principalTable: "S_DAY",
                        principalColumn: "DayId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "D_DIET_DAY",
                columns: table => new
                {
                    DietDayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDayId = table.Column<int>(type: "int", nullable: false),
                    Syslog = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "(suser_name())"),
                    Sysdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    SDateDayDateDayId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__D_DIET_DAY__DietDayId", x => x.DietDayId);
                    table.ForeignKey(
                        name: "FK_D_DIET_DAY_S_DATE_DAY_SDateDayDateDayId",
                        column: x => x.SDateDayDateDayId,
                        principalTable: "S_DATE_DAY",
                        principalColumn: "DateDayId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_D_DIET_DAY_SDateDayDateDayId",
                table: "D_DIET_DAY",
                column: "SDateDayDateDayId");

            migrationBuilder.CreateIndex(
                name: "IX_S_DATE_DAY_SDayDayId",
                table: "S_DATE_DAY",
                column: "SDayDayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "D_DIET_DAY");

            migrationBuilder.DropTable(
                name: "S_DATE_DAY");
        }
    }
}
