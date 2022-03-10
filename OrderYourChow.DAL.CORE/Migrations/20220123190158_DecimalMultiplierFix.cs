using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderYourChow.DAL.CORE.Migrations
{
    public partial class DecimalMultiplierFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Multiplier",
                table: "D_DIET_DAY_RECIPE",
                type: "decimal",
                nullable: false,
                defaultValue: 1m,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Multiplier",
                table: "D_DIET_DAY_RECIPE",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(decimal),
                oldType: "decimal",
                oldDefaultValue: 1m);
        }
    }
}
