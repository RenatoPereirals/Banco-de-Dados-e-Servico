#nullable disable
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bsd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BsdEntities",
                columns: table => new
                {
                    BsdNumber = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BsdEntities", x => x.BsdNumber);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Registration = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Digit = table.Column<int>(type: "INTEGER", nullable: false),
                    DateService = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Registration);
                });

            migrationBuilder.CreateTable(
                name: "EmployeesBsdEntities",
                columns: table => new
                {
                    EmployeeRegistration = table.Column<int>(type: "INTEGER", nullable: false),
                    BsdEntityNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesBsdEntities", x => new { x.BsdEntityNumber, x.EmployeeRegistration });
                    table.ForeignKey(
                        name: "FK_EmployeesBsdEntities_BsdEntities_BsdEntityNumber",
                        column: x => x.BsdEntityNumber,
                        principalTable: "BsdEntities",
                        principalColumn: "BsdNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeesBsdEntities_Employees_EmployeeRegistration",
                        column: x => x.EmployeeRegistration,
                        principalTable: "Employees",
                        principalColumn: "Registration",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rubrics",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    HoursPerDay = table.Column<decimal>(type: "TEXT", nullable: false),
                    DayType = table.Column<int>(type: "INTEGER", nullable: false),
                    ServiceType = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeBsdEntityBsdEntityNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    EmployeeBsdEntityEmployeeRegistration = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubrics", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Rubrics_EmployeesBsdEntities_EmployeeBsdEntityBsdEntityNumber_EmployeeBsdEntityEmployeeRegistration",
                        columns: x => new { x.EmployeeBsdEntityBsdEntityNumber, x.EmployeeBsdEntityEmployeeRegistration },
                        principalTable: "EmployeesBsdEntities",
                        principalColumns: new[] { "BsdEntityNumber", "EmployeeRegistration" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesBsdEntities_EmployeeRegistration",
                table: "EmployeesBsdEntities",
                column: "EmployeeRegistration");

            migrationBuilder.CreateIndex(
                name: "IX_Rubrics_EmployeeBsdEntityBsdEntityNumber_EmployeeBsdEntityEmployeeRegistration",
                table: "Rubrics",
                columns: new[] { "EmployeeBsdEntityBsdEntityNumber", "EmployeeBsdEntityEmployeeRegistration" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rubrics");

            migrationBuilder.DropTable(
                name: "EmployeesBsdEntities");

            migrationBuilder.DropTable(
                name: "BsdEntities");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
