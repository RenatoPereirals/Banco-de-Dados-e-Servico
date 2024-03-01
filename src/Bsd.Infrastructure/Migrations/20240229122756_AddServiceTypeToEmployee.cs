using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bsd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceTypeToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceType",
                table: "Employees",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Employees");
        }
    }
}
