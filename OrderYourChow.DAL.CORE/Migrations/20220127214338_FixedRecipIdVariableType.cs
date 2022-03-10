using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderYourChow.DAL.CORE.Migrations
{
    public partial class FixedRecipIdVariableType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "D_D_RECIPE_FAVOURITE",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(320)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RecipeId",
                table: "D_D_RECIPE_FAVOURITE",
                type: "nvarchar(320)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
