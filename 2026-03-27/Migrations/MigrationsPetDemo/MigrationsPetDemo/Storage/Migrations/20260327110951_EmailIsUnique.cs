using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MigrationsPetDemo.Storage.Migrations
{
    /// <inheritdoc />
    public partial class EmailIsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Owners_Email",
                schema: "Pets",
                table: "Owners",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Owners_Email",
                schema: "Pets",
                table: "Owners");
        }
    }
}
