using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pizza_mama_V2.Migrations
{
    public partial class UpdAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pizzas",
                columns: table => new
                {
                    PizzaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nom = table.Column<string>(type: "TEXT", nullable: true),
                    prix = table.Column<float>(type: "REAL", nullable: false),
                    vegetarienne = table.Column<bool>(type: "INTEGER", nullable: false),
                    ingredients = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizzas", x => x.PizzaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pizzas");
        }
    }
}
