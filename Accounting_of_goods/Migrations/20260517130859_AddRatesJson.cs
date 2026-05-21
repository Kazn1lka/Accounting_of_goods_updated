using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting_of_goods.Migrations
{
    /// <inheritdoc />
    public partial class AddRatesJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RatesJson",
                table: "WriteOffs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RatesJson",
                table: "Shipments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatesJson",
                table: "WriteOffs");

            migrationBuilder.DropColumn(
                name: "RatesJson",
                table: "Shipments");
        }
    }
}
