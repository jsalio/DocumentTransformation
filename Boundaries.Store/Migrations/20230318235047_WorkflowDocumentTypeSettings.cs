using Microsoft.EntityFrameworkCore.Migrations;

namespace Boundaries.Store.Migrations
{
    public partial class WorkflowDocumentTypeSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentConvertSettings",
                schema: "Pdf",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkflowId = table.Column<int>(type: "int", nullable: false),
                    ConvertPdf = table.Column<bool>(type: "bit", nullable: true),
                    SupportOcr = table.Column<bool>(type: "bit", nullable: true),
                    DocumentTypeName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DocumentTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentConvertSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentConvertSettings_Workflows_WorkflowId",
                        column: x => x.WorkflowId,
                        principalSchema: "Pdf",
                        principalTable: "Workflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentConvertSettings_DocumentTypeId",
                schema: "Pdf",
                table: "DocumentConvertSettings",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentConvertSettings_DocumentTypeName",
                schema: "Pdf",
                table: "DocumentConvertSettings",
                column: "DocumentTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentConvertSettings_WorkflowId",
                schema: "Pdf",
                table: "DocumentConvertSettings",
                column: "WorkflowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentConvertSettings",
                schema: "Pdf");
        }
    }
}
