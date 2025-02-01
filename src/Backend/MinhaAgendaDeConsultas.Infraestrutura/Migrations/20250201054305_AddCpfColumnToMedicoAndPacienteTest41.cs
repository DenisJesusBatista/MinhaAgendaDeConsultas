using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinhaAgendaDeConsultas.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class AddCpfColumnToMedicoAndPacienteTest41 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identificador",
                table: "Usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identificador",
                table: "Usuario",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
