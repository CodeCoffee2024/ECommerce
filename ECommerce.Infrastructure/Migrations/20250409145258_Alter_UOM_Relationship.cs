using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Alter_UOM_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UnitOfMeasurements_UnitOfMeasurementTypeId",
                table: "UnitOfMeasurements");

            migrationBuilder.AddColumn<Guid>(
                name: "UnitOfMeasurementTypeId1",
                table: "UnitOfMeasurements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasurements_UnitOfMeasurementTypeId",
                table: "UnitOfMeasurements",
                column: "UnitOfMeasurementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasurements_UnitOfMeasurementTypeId1",
                table: "UnitOfMeasurements",
                column: "UnitOfMeasurementTypeId1",
                unique: true,
                filter: "[UnitOfMeasurementTypeId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitOfMeasurements_UnitOfMeasurementTypes_UnitOfMeasurementTypeId1",
                table: "UnitOfMeasurements",
                column: "UnitOfMeasurementTypeId1",
                principalTable: "UnitOfMeasurementTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitOfMeasurements_UnitOfMeasurementTypes_UnitOfMeasurementTypeId1",
                table: "UnitOfMeasurements");

            migrationBuilder.DropIndex(
                name: "IX_UnitOfMeasurements_UnitOfMeasurementTypeId",
                table: "UnitOfMeasurements");

            migrationBuilder.DropIndex(
                name: "IX_UnitOfMeasurements_UnitOfMeasurementTypeId1",
                table: "UnitOfMeasurements");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurementTypeId1",
                table: "UnitOfMeasurements");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasurements_UnitOfMeasurementTypeId",
                table: "UnitOfMeasurements",
                column: "UnitOfMeasurementTypeId",
                unique: true);
        }
    }
}
