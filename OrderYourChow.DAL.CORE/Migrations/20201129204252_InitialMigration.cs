using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderYourChow.DAL.CORE.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "D_SHOPPING",
                columns: table => new
                {
                    ShoppingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    Cost = table.Column<decimal>(type: "money", nullable: true),
                    Shop = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Syslog = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "(suser_name())"),
                    Sysdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__D_SHOPPI__8E3AF518CAB77B3A", x => x.ShoppingId);
                });

            migrationBuilder.CreateTable(
                name: "S_DAY",
                columns: table => new
                {
                    DayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Syslog = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "(suser_name())"),
                    Sysdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__S_DAY__BF3DD8C568465FE9", x => x.DayId);
                });

            migrationBuilder.CreateTable(
                name: "S_PRODUCT_CATEGORY",
                columns: table => new
                {
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Syslog = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "(suser_name())"),
                    Sysdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__S_PRODUC__3224ECCEAB43FBAA", x => x.ProductCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "S_PRODUCT_MEASURE",
                columns: table => new
                {
                    ProductMeasureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Syslog = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "(suser_name())"),
                    Sysdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__S_PRODUC__4A00517B8885EE62", x => x.ProductMeasureId);
                });

            migrationBuilder.CreateTable(
                name: "S_RECIPE_CATEGORY",
                columns: table => new
                {
                    RecipeCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Syslog = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "(suser_name())"),
                    Sysdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__S_RECIPE__747A031B1E2535C2", x => x.RecipeCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "S_WEEK",
                columns: table => new
                {
                    WeekId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Week = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<DateTime>(type: "date", nullable: false),
                    End = table.Column<DateTime>(type: "date", nullable: false),
                    Syslog = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "(suser_name())"),
                    Sysdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__S_WEEK__C814A5C1B8C5888A", x => x.WeekId);
                });

            migrationBuilder.CreateTable(
                name: "S_PRODUCT",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Syslog = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "(suser_name())"),
                    Sysdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__S_PRODUC__B40CC6CD550497AE", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK__S_PRODUCT__Sysda__619B8048",
                        column: x => x.CategoryId,
                        principalTable: "S_PRODUCT_CATEGORY",
                        principalColumn: "ProductCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "D_RECIPE",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(2,1)", nullable: true),
                    Favourite = table.Column<bool>(type: "bit", nullable: false),
                    Meat = table.Column<bool>(type: "bit", nullable: false),
                    Syslog = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "(suser_name())"),
                    Sysdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__D_RECIPE__FDD988B019C05448", x => x.RecipeId);
                    table.ForeignKey(
                        name: "FK__D_RECIPE__Sysdat__6B24EA82",
                        column: x => x.CategoryId,
                        principalTable: "S_RECIPE_CATEGORY",
                        principalColumn: "RecipeCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "D_PLAN",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekId = table.Column<int>(type: "int", nullable: false),
                    Syslog = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "(suser_name())"),
                    Sysdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__D_PLAN__755C22B70D68E5B2", x => x.PlanId);
                    table.ForeignKey(
                        name: "FK__D_PLAN__Sysdate__44FF419A",
                        column: x => x.WeekId,
                        principalTable: "S_WEEK",
                        principalColumn: "WeekId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "D_PLAN_RECIPE",
                columns: table => new
                {
                    PlanRecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    Syslog = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "(suser_name())"),
                    Sysdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__D_PLAN_R__531B21B7FFF811D6", x => x.PlanRecipeId);
                    table.ForeignKey(
                        name: "FK__D_PLAN_RE__DayId__00200768",
                        column: x => x.DayId,
                        principalTable: "S_DAY",
                        principalColumn: "DayId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__D_PLAN_RE__Sysda__7F2BE32F",
                        column: x => x.RecipeId,
                        principalTable: "D_RECIPE",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "D_RECIPE_IMAGES",
                columns: table => new
                {
                    RecipeImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MainImage = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    Syslog = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "(suser_name())"),
                    Sysdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__D_RECIPE__23E65C238874E5A7", x => x.RecipeImageId);
                    table.ForeignKey(
                        name: "FK__D_RECIPE___Sysda__18EBB532",
                        column: x => x.RecipeId,
                        principalTable: "D_RECIPE",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "D_RECIPE_PRODUCT",
                columns: table => new
                {
                    RecipeProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductMeasureId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Syslog = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "(suser_name())"),
                    Sysdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__D_RECIPE__3879DB74C27AE08D", x => x.RecipeProductId);
                    table.ForeignKey(
                        name: "FK__D_RECIPE___Produ__05D8E0BE",
                        column: x => x.ProductId,
                        principalTable: "S_PRODUCT",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__D_RECIPE___Produ__06CD04F7",
                        column: x => x.ProductMeasureId,
                        principalTable: "S_PRODUCT_MEASURE",
                        principalColumn: "ProductMeasureId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__D_RECIPE___Sysda__04E4BC85",
                        column: x => x.RecipeId,
                        principalTable: "D_RECIPE",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "D_SHOPPING_LIST",
                columns: table => new
                {
                    ShoppingListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    ShoppingId = table.Column<int>(type: "int", nullable: false),
                    Syslog = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValueSql: "(suser_name())"),
                    Sysdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__D_SHOPPI__6CBBDD145EB0D3C8", x => x.ShoppingListId);
                    table.ForeignKey(
                        name: "FK__D_SHOPPIN__Shopp__7A672E12",
                        column: x => x.ShoppingId,
                        principalTable: "D_SHOPPING",
                        principalColumn: "ShoppingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__D_SHOPPIN__Sysda__797309D9",
                        column: x => x.RecipeId,
                        principalTable: "D_RECIPE",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_D_PLAN_WeekId",
                table: "D_PLAN",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_D_PLAN_RECIPE_DayId",
                table: "D_PLAN_RECIPE",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_D_PLAN_RECIPE_RecipeId",
                table: "D_PLAN_RECIPE",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_D_RECIPE_CategoryId",
                table: "D_RECIPE",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_D_RECIPE_IMAGES_RecipeId",
                table: "D_RECIPE_IMAGES",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_D_RECIPE_PRODUCT_ProductId",
                table: "D_RECIPE_PRODUCT",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_D_RECIPE_PRODUCT_ProductMeasureId",
                table: "D_RECIPE_PRODUCT",
                column: "ProductMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_D_RECIPE_PRODUCT_RecipeId",
                table: "D_RECIPE_PRODUCT",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_D_SHOPPING_LIST_RecipeId",
                table: "D_SHOPPING_LIST",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_D_SHOPPING_LIST_ShoppingId",
                table: "D_SHOPPING_LIST",
                column: "ShoppingId");

            migrationBuilder.CreateIndex(
                name: "IX_S_PRODUCT_CategoryId",
                table: "S_PRODUCT",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "D_PLAN");

            migrationBuilder.DropTable(
                name: "D_PLAN_RECIPE");

            migrationBuilder.DropTable(
                name: "D_RECIPE_IMAGES");

            migrationBuilder.DropTable(
                name: "D_RECIPE_PRODUCT");

            migrationBuilder.DropTable(
                name: "D_SHOPPING_LIST");

            migrationBuilder.DropTable(
                name: "S_WEEK");

            migrationBuilder.DropTable(
                name: "S_DAY");

            migrationBuilder.DropTable(
                name: "S_PRODUCT");

            migrationBuilder.DropTable(
                name: "S_PRODUCT_MEASURE");

            migrationBuilder.DropTable(
                name: "D_SHOPPING");

            migrationBuilder.DropTable(
                name: "D_RECIPE");

            migrationBuilder.DropTable(
                name: "S_PRODUCT_CATEGORY");

            migrationBuilder.DropTable(
                name: "S_RECIPE_CATEGORY");
        }
    }
}
