using Microsoft.EntityFrameworkCore.Migrations;

namespace VetrinaDolci.WebAPI.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Colesterolo",
                table: "Ingredienti",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Fibra",
                table: "Ingredienti",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Grassi",
                table: "Ingredienti",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Kcal",
                table: "Ingredienti",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Proteine",
                table: "Ingredienti",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Zuccheri",
                table: "Ingredienti",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "IngPrincipale",
                table: "Dolci",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Dolci",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Persone",
                table: "Dolci",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Preparazione",
                table: "Dolci",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoPiatto",
                table: "Dolci",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Colesterolo",
                table: "Ingredienti");

            migrationBuilder.DropColumn(
                name: "Fibra",
                table: "Ingredienti");

            migrationBuilder.DropColumn(
                name: "Grassi",
                table: "Ingredienti");

            migrationBuilder.DropColumn(
                name: "Kcal",
                table: "Ingredienti");

            migrationBuilder.DropColumn(
                name: "Proteine",
                table: "Ingredienti");

            migrationBuilder.DropColumn(
                name: "Zuccheri",
                table: "Ingredienti");

            migrationBuilder.DropColumn(
                name: "IngPrincipale",
                table: "Dolci");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Dolci");

            migrationBuilder.DropColumn(
                name: "Persone",
                table: "Dolci");

            migrationBuilder.DropColumn(
                name: "Preparazione",
                table: "Dolci");

            migrationBuilder.DropColumn(
                name: "TipoPiatto",
                table: "Dolci");
        }
    }
}
