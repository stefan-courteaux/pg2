using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MigrationsPetDemo.Storage.Migrations
{
    /// <inheritdoc />
    public partial class AddVet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vets",
                schema: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PetVet",
                schema: "Pets",
                columns: table => new
                {
                    PetsId = table.Column<long>(type: "bigint", nullable: false),
                    VetsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetVet", x => new { x.PetsId, x.VetsId });
                    table.ForeignKey(
                        name: "FK_PetVet_Pets_PetsId",
                        column: x => x.PetsId,
                        principalSchema: "Pets",
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetVet_Vets_VetsId",
                        column: x => x.VetsId,
                        principalSchema: "Pets",
                        principalTable: "Vets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetVet_VetsId",
                schema: "Pets",
                table: "PetVet",
                column: "VetsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetVet",
                schema: "Pets");

            migrationBuilder.DropTable(
                name: "Vets",
                schema: "Pets");
        }
    }
}
