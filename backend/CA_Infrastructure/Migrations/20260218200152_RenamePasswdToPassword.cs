using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CA_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamePasswdToPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Passwd",
                table: "Users",
                newName: "Password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Passwd");
        }
    }
}
