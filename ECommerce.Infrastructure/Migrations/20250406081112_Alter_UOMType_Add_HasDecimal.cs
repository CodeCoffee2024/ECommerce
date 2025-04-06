using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Alter_UOMType_Add_HasDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasDecimal",
                table: "UnitOfMeasurements");

            migrationBuilder.AddColumn<bool>(
                name: "HasDecimal",
                table: "UnitOfMeasurementTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasDecimal",
                table: "UnitOfMeasurementTypes");

            migrationBuilder.AddColumn<bool>(
                name: "HasDecimal",
                table: "UnitOfMeasurements",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}