using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderYourChow.DAL.CORE.Migrations
{
    public partial class AddedDUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "D_DIET_DAY",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "D_USER",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(320)", nullable: true),
                    MultiplierDiet = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Syslog = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "(suser_name())"),
                    Sysdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__D_USER_UserId", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_D_DIET_DAY_UserId",
                table: "D_DIET_DAY",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__D_DIET_DAY__D_USER",
                table: "D_DIET_DAY",
                column: "UserId",
                principalTable: "D_USER",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__D_DIET_DAY__D_USER",
                table: "D_DIET_DAY");

            migrationBuilder.DropTable(
                name: "D_USER");

            migrationBuilder.DropIndex(
                name: "IX_D_DIET_DAY_UserId",
                table: "D_DIET_DAY");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "D_DIET_DAY");
        }
    }
}
