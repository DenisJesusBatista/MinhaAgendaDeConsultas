using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MinhaAgendaDeConsultas.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class AddCpfColumnToMedicoAndPacienteTest22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "UsuarioSaude");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "UsuarioSaude",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

            migrationBuilder.AddColumn<Guid>(
                name: "Identificador",
                table: "UsuarioSaude",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Medico_Cpf",
                table: "UsuarioSaude",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "UsuarioSaude",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioSaude",
                table: "UsuarioSaude",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EntidadeBase",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntidadeBase", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntidadeBase");

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
                name: "Identificador",
                table: "UsuarioSaude");

            migrationBuilder.DropColumn(
                name: "Medico_Cpf",
                table: "UsuarioSaude");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "UsuarioSaude");

            migrationBuilder.RenameTable(
                name: "UsuarioSaude",
                newName: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Usuarios",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");
        }
    }
}
