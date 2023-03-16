using Microsoft.EntityFrameworkCore.Migrations;

namespace Boundaries.Store.Migrations
{
    public partial class AddEngineTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PdfEngines",
                schema: "Pdf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EngineTypeName = table.Column<int>(type: "int", nullable: false),
                    EngineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineType = table.Column<int>(type: "int", nullable: false),
                    EngineStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseType = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    SupportOcr = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdfEngines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EngineLicenses",
                schema: "Pdf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EngineId = table.Column<int>(type: "int", nullable: false),
                    LicenseString = table.Column<string>(type: "Text", maxLength: 20000000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineLicenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngineLicenses_PdfEngines_EngineId",
                        column: x => x.EngineId,
                        principalSchema: "Pdf",
                        principalTable: "PdfEngines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EngineLicenses_EngineId",
                schema: "Pdf",
                table: "EngineLicenses",
                column: "EngineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PdfEngines_EngineTypeName",
                schema: "Pdf",
                table: "PdfEngines",
                column: "EngineTypeName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EngineLicenses",
                schema: "Pdf");

            migrationBuilder.DropTable(
                name: "PdfEngines",
                schema: "Pdf");
        }
    }
}
