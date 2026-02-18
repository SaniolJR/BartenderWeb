using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CA_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameNickToUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nick",
                table: "Users",
                newName: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "Nick");
        }
    }
}
