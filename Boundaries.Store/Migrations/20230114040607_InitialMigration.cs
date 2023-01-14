using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boundaries.Store.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Pdf");

            migrationBuilder.CreateTable(
                name: "Rules",
                schema: "Pdf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceSettings",
                schema: "Pdf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkMode = table.Column<int>(type: "int", nullable: false),
                    EnableSecondQueue = table.Column<bool>(type: "bit", nullable: false),
                    TimerWorkMode = table.Column<int>(type: "int", nullable: false),
                    TimeUnit = table.Column<int>(type: "int", nullable: false),
                    Interval = table.Column<int>(type: "int", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeInit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeEnd = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workflows",
                schema: "Pdf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Handle = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActiveQaIndex = table.Column<bool>(type: "bit", nullable: false),
                    IsActiveQaScan = table.Column<bool>(type: "bit", nullable: false),
                    IsMultipleIndexingActive = table.Column<bool>(type: "bit", nullable: false),
                    ConvertToPdf = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflows", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rules",
                schema: "Pdf");

            migrationBuilder.DropTable(
                name: "ServiceSettings",
                schema: "Pdf");

            migrationBuilder.DropTable(
                name: "Workflows",
                schema: "Pdf");
        }
    }
}
