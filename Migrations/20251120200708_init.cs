using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fit_life.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HabitoTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Execucao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recomendacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tempo = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitoTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TreinoTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tempo = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreinoTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExercicioTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instrucoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaTreinada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Repeticoes = table.Column<int>(type: "int", nullable: false),
                    Series = table.Column<int>(type: "int", nullable: false),
                    TreinoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercicioTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExercicioTable_TreinoTable_TreinoId",
                        column: x => x.TreinoId,
                        principalTable: "TreinoTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExercicioTable_TreinoId",
                table: "ExercicioTable",
                column: "TreinoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExercicioTable");

            migrationBuilder.DropTable(
                name: "HabitoTable");

            migrationBuilder.DropTable(
                name: "TreinoTable");
        }
    }
}
