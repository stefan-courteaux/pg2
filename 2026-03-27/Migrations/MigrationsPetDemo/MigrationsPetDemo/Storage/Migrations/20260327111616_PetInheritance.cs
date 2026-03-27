using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MigrationsPetDemo.Storage.Migrations
{
    /// <inheritdoc />
    public partial class PetInheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "Pets",
                table: "Pets",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDomesticatedBreed",
                schema: "Pets",
                table: "Pets",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoeSizeBack",
                schema: "Pets",
                table: "Pets",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoeSizeFront",
                schema: "Pets",
                table: "Pets",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "Pets",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "IsDomesticatedBreed",
                schema: "Pets",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "ShoeSizeBack",
                schema: "Pets",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "ShoeSizeFront",
                schema: "Pets",
                table: "Pets");
        }
    }
}
