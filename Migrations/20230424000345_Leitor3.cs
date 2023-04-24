using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exemplo.Migrations
{
    /// <inheritdoc />
    public partial class Leitor3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuario_Apelido",
                table: "Usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Apelido",
                table: "Usuario",
                column: "Apelido",
                unique: true);
        }
    }
}
