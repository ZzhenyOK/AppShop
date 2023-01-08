using Microsoft.EntityFrameworkCore.Migrations;

namespace AppShop.Migrations
{
    public partial class AddQty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Product",
                newName: "Qty");

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "Histories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qty",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "Qty",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "Qty",
                table: "Product",
                newName: "State");
        }
    }
}
