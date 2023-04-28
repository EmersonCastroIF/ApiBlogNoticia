using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exemplo.Migrations
{
    /// <inheritdoc />
    public partial class Leitor6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropColumn(
            //     name: "UsuarioId",
            //     table: "Usuario");

            migrationBuilder.AddColumn<string>(
                name: "CodigoRedefineEmail",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodigoRedefineSenha",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoRedefineEmail",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "CodigoRedefineSenha",
                table: "Usuario");

            // migrationBuilder.AddColumn<int>(
            //     name: "UsuarioId",
            //     table: "Usuario",
            //     type: "int",
            //     nullable: false,
            //     defaultValue: 0);
        }
    }
}
