using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinhaAgendaDeConsultas.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class AddCpfColumnToMedicoAndPacienteTest30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioSaude",
                table: "UsuarioSaude");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "UsuarioSaude");

            migrationBuilder.DropColumn(
                name: "Crm",
                table: "UsuarioSaude");

            migrationBuilder.DropColumn(
                name: "Medico_Cpf",
                table: "UsuarioSaude");

            migrationBuilder.RenameTable(
                name: "UsuarioSaude",
                newName: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "Tipo",
                table: "Usuario",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UsuarioMedico",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Cpf = table.Column<string>(type: "text", nullable: false),
                    Crm = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioMedico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioMedico_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioPaciente",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPaciente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioPaciente_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioMedico");

            migrationBuilder.DropTable(
                name: "UsuarioPaciente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "UsuarioSaude");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "UsuarioSaude",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "UsuarioSaude",
                type: "character varying(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Crm",
                table: "UsuarioSaude",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medico_Cpf",
                table: "UsuarioSaude",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioSaude",
                table: "UsuarioSaude",
                column: "Id");
        }
    }
}
