using Microsoft.EntityFrameworkCore.Migrations;

namespace AskStorage.AccesoDatos.Migrations
{
    public partial class CreacionTablaPregunta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pregunta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Interrogante = table.Column<string>(nullable: false),
                    R1 = table.Column<string>(nullable: false),
                    R2 = table.Column<string>(nullable: false),
                    R3 = table.Column<string>(nullable: false),
                    R4 = table.Column<string>(nullable: false),
                    RC = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: true),
                    CategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pregunta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pregunta_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pregunta_CategoriaId",
                table: "Pregunta",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pregunta");
        }
    }
}
