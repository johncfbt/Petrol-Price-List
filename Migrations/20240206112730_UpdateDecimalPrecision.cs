using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetrolPrice.Migrations
{
    public partial class UpdateDecimalPrecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "PetrolStation",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "PetrolStation",
                type: "decimal(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");
        }
    }
}
