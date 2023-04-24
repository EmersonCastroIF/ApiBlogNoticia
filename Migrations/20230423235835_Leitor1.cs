using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exemplo.Migrations
{
    /// <inheritdoc />
    public partial class Leitor1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Usuario",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(101)",
                oldMaxLength: 101);

            migrationBuilder.AddColumn<string>(
                name: "Apelido",
                table: "Usuario",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apelido",
                table: "Usuario");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Usuario",
                type: "nvarchar(101)",
                maxLength: 101,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
