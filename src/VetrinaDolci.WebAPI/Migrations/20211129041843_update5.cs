using Microsoft.EntityFrameworkCore.Migrations;

namespace VetrinaDolci.WebAPI.Migrations
{
    public partial class update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DolceInVenditaId",
                table: "Dolci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DolceInVenditaId",
                table: "Dolci",
                type: "INTEGER",
                nullable: true);
        }
    }
}
