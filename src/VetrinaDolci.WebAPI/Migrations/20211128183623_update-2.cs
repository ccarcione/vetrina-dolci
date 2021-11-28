using Microsoft.EntityFrameworkCore.Migrations;

namespace VetrinaDolci.WebAPI.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientiDolce",
                table: "IngredientiDolce");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "IngredientiDolce",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientiDolce",
                table: "IngredientiDolce",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientiDolce_DolceId",
                table: "IngredientiDolce",
                column: "DolceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientiDolce",
                table: "IngredientiDolce");

            migrationBuilder.DropIndex(
                name: "IX_IngredientiDolce_DolceId",
                table: "IngredientiDolce");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "IngredientiDolce");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientiDolce",
                table: "IngredientiDolce",
                columns: new[] { "DolceId", "IngredienteId" });
        }
    }
}
