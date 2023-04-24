using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exemplo.Migrations
{
    /// <inheritdoc />
    public partial class Leitor2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Apelido",
                table: "Usuario",
                column: "Apelido",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuario_Apelido",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_Email",
                table: "Usuario");
        }
    }
}
