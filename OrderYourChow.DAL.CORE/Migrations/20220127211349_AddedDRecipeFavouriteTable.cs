using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderYourChow.DAL.CORE.Migrations
{
    public partial class AddedDRecipeFavouriteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "D_RECIPFAVOURITE",
                columns: table => new
                {
                    RecipeFavouriteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<string>(type: "nvarchar(320)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__D_RECIPFAVOURITE_RecipeFavouriteId", x => x.RecipeFavouriteId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "D_RECIPFAVOURITE");
        }
    }
}
