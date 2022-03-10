using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderYourChow.DAL.CORE.Migrations
{
    public partial class ModifiedMainImageRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImage",
                table: "D_RECIPE_IMAGES");

            migrationBuilder.AddColumn<string>(
                name: "MainImage",
                table: "D_RECIPE",
                type: "nvarchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImage",
                table: "D_RECIPE");

            migrationBuilder.AddColumn<bool>(
                name: "MainImage",
                table: "D_RECIPE_IMAGES",
                type: "bit",
                nullable: true,
                defaultValueSql: "((0))");
        }
    }
}
