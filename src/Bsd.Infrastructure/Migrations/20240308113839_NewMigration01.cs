using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bsd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesBsdEntities_BsdEntities_BsdEntityNumber",
                table: "EmployeesBsdEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_Rubrics_EmployeesBsdEntities_EmployeeBsdEntityBsdEntityNumber_EmployeeBsdEntityEmployeeRegistration",
                table: "Rubrics");

            migrationBuilder.DropIndex(
                name: "IX_Rubrics_EmployeeBsdEntityBsdEntityNumber_EmployeeBsdEntityEmployeeRegistration",
                table: "Rubrics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeesBsdEntities",
                table: "EmployeesBsdEntities");

            migrationBuilder.DropIndex(
                name: "IX_EmployeesBsdEntities_EmployeeRegistration",
                table: "EmployeesBsdEntities");

            migrationBuilder.RenameColumn(
                name: "EmployeeBsdEntityBsdEntityNumber",
                table: "Rubrics",
                newName: "EmployeeBsdEntityBsdNumber");

            migrationBuilder.RenameColumn(
                name: "BsdEntityNumber",
                table: "EmployeesBsdEntities",
                newName: "BsdNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeesBsdEntities",
                table: "EmployeesBsdEntities",
                columns: new[] { "EmployeeRegistration", "BsdNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_Rubrics_EmployeeBsdEntityEmployeeRegistration_EmployeeBsdEntityBsdNumber",
                table: "Rubrics",
                columns: new[] { "EmployeeBsdEntityEmployeeRegistration", "EmployeeBsdEntityBsdNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesBsdEntities_BsdNumber",
                table: "EmployeesBsdEntities",
                column: "BsdNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesBsdEntities_BsdEntities_BsdNumber",
                table: "EmployeesBsdEntities",
                column: "BsdNumber",
                principalTable: "BsdEntities",
                principalColumn: "BsdNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rubrics_EmployeesBsdEntities_EmployeeBsdEntityEmployeeRegistration_EmployeeBsdEntityBsdNumber",
                table: "Rubrics",
                columns: new[] { "EmployeeBsdEntityEmployeeRegistration", "EmployeeBsdEntityBsdNumber" },
                principalTable: "EmployeesBsdEntities",
                principalColumns: new[] { "EmployeeRegistration", "BsdNumber" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesBsdEntities_BsdEntities_BsdNumber",
                table: "EmployeesBsdEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_Rubrics_EmployeesBsdEntities_EmployeeBsdEntityEmployeeRegistration_EmployeeBsdEntityBsdNumber",
                table: "Rubrics");

            migrationBuilder.DropIndex(
                name: "IX_Rubrics_EmployeeBsdEntityEmployeeRegistration_EmployeeBsdEntityBsdNumber",
                table: "Rubrics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeesBsdEntities",
                table: "EmployeesBsdEntities");

            migrationBuilder.DropIndex(
                name: "IX_EmployeesBsdEntities_BsdNumber",
                table: "EmployeesBsdEntities");

            migrationBuilder.RenameColumn(
                name: "EmployeeBsdEntityBsdNumber",
                table: "Rubrics",
                newName: "EmployeeBsdEntityBsdEntityNumber");

            migrationBuilder.RenameColumn(
                name: "BsdNumber",
                table: "EmployeesBsdEntities",
                newName: "BsdEntityNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeesBsdEntities",
                table: "EmployeesBsdEntities",
                columns: new[] { "BsdEntityNumber", "EmployeeRegistration" });

            migrationBuilder.CreateIndex(
                name: "IX_Rubrics_EmployeeBsdEntityBsdEntityNumber_EmployeeBsdEntityEmployeeRegistration",
                table: "Rubrics",
                columns: new[] { "EmployeeBsdEntityBsdEntityNumber", "EmployeeBsdEntityEmployeeRegistration" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesBsdEntities_EmployeeRegistration",
                table: "EmployeesBsdEntities",
                column: "EmployeeRegistration");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesBsdEntities_BsdEntities_BsdEntityNumber",
                table: "EmployeesBsdEntities",
                column: "BsdEntityNumber",
                principalTable: "BsdEntities",
                principalColumn: "BsdNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rubrics_EmployeesBsdEntities_EmployeeBsdEntityBsdEntityNumber_EmployeeBsdEntityEmployeeRegistration",
                table: "Rubrics",
                columns: new[] { "EmployeeBsdEntityBsdEntityNumber", "EmployeeBsdEntityEmployeeRegistration" },
                principalTable: "EmployeesBsdEntities",
                principalColumns: new[] { "BsdEntityNumber", "EmployeeRegistration" });
        }
    }
}
