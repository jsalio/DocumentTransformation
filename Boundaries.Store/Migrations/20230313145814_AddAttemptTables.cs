using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boundaries.Store.Migrations
{
    public partial class AddAttemptTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attempt",
                schema: "Pdf",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchId = table.Column<long>(type: "bigint", nullable: false),
                    DocumentHandler = table.Column<long>(type: "bigint", nullable: false),
                    DocumentType = table.Column<long>(type: "bigint", nullable: false),
                    RegistryDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CaseCaseStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attempt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttemptDetail",
                schema: "Pdf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttemptId = table.Column<long>(type: "bigint", nullable: false),
                    RegistryDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ErrorDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttemptDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttemptDetail_Attempt_AttemptId",
                        column: x => x.AttemptId,
                        principalSchema: "Pdf",
                        principalTable: "Attempt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attempt_DocumentHandler",
                schema: "Pdf",
                table: "Attempt",
                column: "DocumentHandler");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptDetail_AttemptId",
                schema: "Pdf",
                table: "AttemptDetail",
                column: "AttemptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttemptDetail",
                schema: "Pdf");

            migrationBuilder.DropTable(
                name: "Attempt",
                schema: "Pdf");
        }
    }
}
