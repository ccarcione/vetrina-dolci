using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VetrinaDolci.WebAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DolciInVendita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Disponibilita = table.Column<int>(type: "INTEGER", nullable: false),
                    InVenditaDa = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DolciInVendita", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredienti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredienti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dolci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Prezzo = table.Column<string>(type: "TEXT", nullable: true),
                    DolceInVenditaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dolci", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dolci_DolciInVendita_DolceInVenditaId",
                        column: x => x.DolceInVenditaId,
                        principalTable: "DolciInVendita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngredientiDolce",
                columns: table => new
                {
                    DolceId = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantita = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitaDiMisura = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientiDolce", x => new { x.DolceId, x.IngredienteId });
                    table.ForeignKey(
                        name: "FK_IngredientiDolce_Dolci_DolceId",
                        column: x => x.DolceId,
                        principalTable: "Dolci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientiDolce_Ingredienti_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingredienti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dolci_DolceInVenditaId",
                table: "Dolci",
                column: "DolceInVenditaId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientiDolce_IngredienteId",
                table: "IngredientiDolce",
                column: "IngredienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientiDolce");

            migrationBuilder.DropTable(
                name: "Dolci");

            migrationBuilder.DropTable(
                name: "Ingredienti");

            migrationBuilder.DropTable(
                name: "DolciInVendita");
        }
    }
}
