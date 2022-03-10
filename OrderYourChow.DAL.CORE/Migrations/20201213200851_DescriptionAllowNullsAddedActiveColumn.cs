using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderYourChow.DAL.CORE.Migrations
{
    public partial class DescriptionAllowNullsAddedActiveColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "D_RECIPE",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "D_RECIPE",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "D_RECIPE");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "D_RECIPE",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
