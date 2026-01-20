using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CA_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IngredientsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Verified",
                table: "Drinks",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AverageRating",
                table: "Drinks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Drinks");

            migrationBuilder.AlterColumn<bool>(
                name: "Verified",
                table: "Drinks",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
