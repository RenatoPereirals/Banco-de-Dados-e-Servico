using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bsd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BsdEntities",
                columns: table => new
                {
                    BsdId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateService = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DayType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BsdEntities", x => x.BsdId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Digit = table.Column<int>(type: "INTEGER", nullable: false),
                    ServiceType = table.Column<int>(type: "INTEGER", nullable: false),
                    DateService = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Rubrics",
                columns: table => new
                {
                    RubricId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    HoursPerDay = table.Column<decimal>(type: "TEXT", nullable: false),
                    DayType = table.Column<int>(type: "INTEGER", nullable: false),
                    ServiceType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubrics", x => x.RubricId);
                });

            migrationBuilder.CreateTable(
                name: "BsdEntityEmployee",
                columns: table => new
                {
                    BsdEntitiesBsdId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeesEmployeeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BsdEntityEmployee", x => new { x.BsdEntitiesBsdId, x.EmployeesEmployeeId });
                    table.ForeignKey(
                        name: "FK_BsdEntityEmployee_BsdEntities_BsdEntitiesBsdId",
                        column: x => x.BsdEntitiesBsdId,
                        principalTable: "BsdEntities",
                        principalColumn: "BsdId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BsdEntityEmployee_Employees_EmployeesEmployeeId",
                        column: x => x.EmployeesEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRubrics",
                columns: table => new
                {
                    EmployeeRubricId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BsdEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    RubricId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRubrics", x => x.EmployeeRubricId);
                    table.ForeignKey(
                        name: "FK_EmployeeRubrics_BsdEntities_BsdEntityId",
                        column: x => x.BsdEntityId,
                        principalTable: "BsdEntities",
                        principalColumn: "BsdId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeRubrics_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeRubrics_Rubrics_RubricId",
                        column: x => x.RubricId,
                        principalTable: "Rubrics",
                        principalColumn: "RubricId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BsdEntityEmployee_EmployeesEmployeeId",
                table: "BsdEntityEmployee",
                column: "EmployeesEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRubrics_BsdEntityId",
                table: "EmployeeRubrics",
                column: "BsdEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRubrics_EmployeeId",
                table: "EmployeeRubrics",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRubrics_RubricId",
                table: "EmployeeRubrics",
                column: "RubricId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BsdEntityEmployee");

            migrationBuilder.DropTable(
                name: "EmployeeRubrics");

            migrationBuilder.DropTable(
                name: "BsdEntities");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Rubrics");
        }
    }
}
