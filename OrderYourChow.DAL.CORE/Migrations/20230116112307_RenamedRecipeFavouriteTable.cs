using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderYourChow.DAL.CORE.Migrations
{
    public partial class RenamedRecipeFavouriteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_D_D_RECIPE_FAVOURITE_D_RECIPE_DRecipeRecipeId",
                table: "D_D_RECIPE_FAVOURITE");

            migrationBuilder.RenameTable(
                name: "D_D_RECIPE_FAVOURITE",
                newName: "D_RECIPE_FAVOURITE");

            migrationBuilder.RenameIndex(
                name: "IX_D_D_RECIPE_FAVOURITE_DRecipeRecipeId",
                table: "D_RECIPE_FAVOURITE",
                newName: "IX_D_RECIPE_FAVOURITE_DRecipeRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_D_RECIPE_FAVOURITE_D_RECIPE_DRecipeRecipeId",
                table: "D_RECIPE_FAVOURITE",
                column: "DRecipeRecipeId",
                principalTable: "D_RECIPE",
                principalColumn: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_D_RECIPE_FAVOURITE_D_RECIPE_DRecipeRecipeId",
                table: "D_RECIPE_FAVOURITE");

            migrationBuilder.RenameTable(
                name: "D_RECIPE_FAVOURITE",
                newName: "D_D_RECIPE_FAVOURITE");

            migrationBuilder.RenameIndex(
                name: "IX_D_RECIPE_FAVOURITE_DRecipeRecipeId",
                table: "D_D_RECIPE_FAVOURITE",
                newName: "IX_D_D_RECIPE_FAVOURITE_DRecipeRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_D_D_RECIPE_FAVOURITE_D_RECIPE_DRecipeRecipeId",
                table: "D_D_RECIPE_FAVOURITE",
                column: "DRecipeRecipeId",
                principalTable: "D_RECIPE",
                principalColumn: "RecipeId");
        }
    }
}
