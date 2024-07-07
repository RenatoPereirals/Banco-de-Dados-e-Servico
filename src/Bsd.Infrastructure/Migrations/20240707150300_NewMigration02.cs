using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bsd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration02 : Migration
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
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Digit = table.Column<int>(type: "INTEGER", nullable: false),
                    ServiceType = table.Column<int>(type: "INTEGER", nullable: false),
                    BsdEntityBsdId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_BsdEntities_BsdEntityBsdId",
                        column: x => x.BsdEntityBsdId,
                        principalTable: "BsdEntities",
                        principalColumn: "BsdId");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRubricAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    BsdEntityBsdId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRubricAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeRubricAssignment_BsdEntities_BsdEntityBsdId",
                        column: x => x.BsdEntityBsdId,
                        principalTable: "BsdEntities",
                        principalColumn: "BsdId");
                });

            migrationBuilder.CreateTable(
                name: "Rubric",
                columns: table => new
                {
                    RubricId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    HoursPerDay = table.Column<decimal>(type: "TEXT", nullable: false),
                    DayType = table.Column<int>(type: "INTEGER", nullable: false),
                    ServiceType = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeRubricAssignmentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubric", x => x.RubricId);
                    table.ForeignKey(
                        name: "FK_Rubric_EmployeeRubricAssignment_EmployeeRubricAssignmentId",
                        column: x => x.EmployeeRubricAssignmentId,
                        principalTable: "EmployeeRubricAssignment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_BsdEntityBsdId",
                table: "Employee",
                column: "BsdEntityBsdId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRubricAssignment_BsdEntityBsdId",
                table: "EmployeeRubricAssignment",
                column: "BsdEntityBsdId");

            migrationBuilder.CreateIndex(
                name: "IX_Rubric_EmployeeRubricAssignmentId",
                table: "Rubric",
                column: "EmployeeRubricAssignmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Rubric");

            migrationBuilder.DropTable(
                name: "EmployeeRubricAssignment");

            migrationBuilder.DropTable(
                name: "BsdEntities");
        }
    }
}
