using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderYourChow.DAL.CORE.Migrations
{
    public partial class AddedDietDateRecipeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "D_DIET_DAY_RECIPE",
                columns: table => new
                {
                    DietDayRecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DietDayId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    Syslog = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "(suser_name())"),
                    Sysdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__D_DIET_DAY_RECIPE_DietDayRecipeId", x => x.DietDayRecipeId);
                    table.ForeignKey(
                        name: "FK__D_DIET_DAY_RECIPE_D_DIET_DAY",
                        column: x => x.DietDayId,
                        principalTable: "D_DIET_DAY",
                        principalColumn: "DietDayId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__D_DIET_DAY_RECIPE_D_RECIPE",
                        column: x => x.RecipeId,
                        principalTable: "D_RECIPE",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_D_DIET_DAY_RECIPE_DietDayId",
                table: "D_DIET_DAY_RECIPE",
                column: "DietDayId");

            migrationBuilder.CreateIndex(
                name: "IX_D_DIET_DAY_RECIPE_RecipeId",
                table: "D_DIET_DAY_RECIPE",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "D_DIET_DAY_RECIPE");
        }
    }
}
