using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermission_Module_ModuleId",
                table: "UserPermission");

            migrationBuilder.DropIndex(
                name: "IX_UserPermission_ModuleId",
                table: "UserPermission");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "UserPermission");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ModuleId",
                table: "UserPermission",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_ModuleId",
                table: "UserPermission",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermission_Module_ModuleId",
                table: "UserPermission",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
