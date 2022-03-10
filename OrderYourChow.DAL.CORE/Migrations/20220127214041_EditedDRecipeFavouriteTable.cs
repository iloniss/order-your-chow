using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderYourChow.DAL.CORE.Migrations
{
    public partial class EditedDRecipeFavouriteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__D_RECIPFAVOURITE_RecipeFavouriteId",
                table: "D_RECIPFAVOURITE");

            migrationBuilder.RenameTable(
                name: "D_RECIPFAVOURITE",
                newName: "D_D_RECIPE_FAVOURITE");

            migrationBuilder.AddPrimaryKey(
                name: "PK__D_RECIPE_FAVOURITE_RecipeFavouriteId",
                table: "D_D_RECIPE_FAVOURITE",
                column: "RecipeFavouriteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__D_RECIPE_FAVOURITE_RecipeFavouriteId",
                table: "D_D_RECIPE_FAVOURITE");

            migrationBuilder.RenameTable(
                name: "D_D_RECIPE_FAVOURITE",
                newName: "D_RECIPFAVOURITE");

            migrationBuilder.AddPrimaryKey(
                name: "PK__D_RECIPFAVOURITE_RecipeFavouriteId",
                table: "D_RECIPFAVOURITE",
                column: "RecipeFavouriteId");
        }
    }
}
