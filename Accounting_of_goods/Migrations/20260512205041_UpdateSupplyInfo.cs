using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinFormsApp1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSupplyInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Supplies",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SellingPrice",
                table: "Supplies",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SupplyId",
                table: "Shipments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_SupplyId",
                table: "Shipments",
                column: "SupplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Supplies_SupplyId",
                table: "Shipments",
                column: "SupplyId",
                principalTable: "Supplies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Supplies_SupplyId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_SupplyId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "SupplyId",
                table: "Shipments");
        }
    }
}
