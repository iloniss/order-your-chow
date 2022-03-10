using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderYourChow.DAL.CORE.Migrations
{
    public partial class AddedForeignKeysInRecipeFavourite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DRecipeRecipeId",
                table: "D_D_RECIPE_FAVOURITE",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_D_D_RECIPE_FAVOURITE_DRecipeRecipeId",
                table: "D_D_RECIPE_FAVOURITE",
                column: "DRecipeRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_D_D_RECIPE_FAVOURITE_D_RECIPE_DRecipeRecipeId",
                table: "D_D_RECIPE_FAVOURITE",
                column: "DRecipeRecipeId",
                principalTable: "D_RECIPE",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_D_D_RECIPE_FAVOURITE_D_RECIPE_DRecipeRecipeId",
                table: "D_D_RECIPE_FAVOURITE");

            migrationBuilder.DropIndex(
                name: "IX_D_D_RECIPE_FAVOURITE_DRecipeRecipeId",
                table: "D_D_RECIPE_FAVOURITE");

            migrationBuilder.DropColumn(
                name: "DRecipeRecipeId",
                table: "D_D_RECIPE_FAVOURITE");
        }
    }
}
