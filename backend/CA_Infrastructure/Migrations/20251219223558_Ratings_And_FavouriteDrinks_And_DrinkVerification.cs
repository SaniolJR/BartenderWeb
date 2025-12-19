using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CA_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Ratings_And_FavouriteDrinks_And_DrinkVerification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Drinks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "Drinks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutorId = table.Column<int>(type: "int", nullable: false),
                    CertainDrinkId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Drinks_CertainDrinkId",
                        column: x => x.CertainDrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_UserId",
                table: "Drinks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_AutorId",
                table: "Ratings",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CertainDrinkId",
                table: "Ratings",
                column: "CertainDrinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_Users_UserId",
                table: "Drinks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_Users_UserId",
                table: "Drinks");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_UserId",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "Verified",
                table: "Drinks");
        }
    }
}
