using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MigrationsPetDemo.Storage.Migrations
{
    /// <inheritdoc />
    public partial class LongerNoteAndPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "Pets",
                table: "Owners",
                type: "character varying(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                schema: "Pets",
                table: "Notes",
                type: "character varying(5000)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "Pets",
                table: "Owners");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                schema: "Pets",
                table: "Notes",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(5000)",
                oldMaxLength: 5000);
        }
    }
}
