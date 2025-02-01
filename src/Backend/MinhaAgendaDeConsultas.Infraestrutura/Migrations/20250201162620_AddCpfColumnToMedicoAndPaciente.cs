using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MinhaAgendaDeConsultas.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class AddCpfColumnToMedicoAndPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioMedico_Usuario_Id",
                table: "UsuarioMedico");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioPaciente_Usuario_Id",
                table: "UsuarioPaciente");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "UsuarioPaciente",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "UsuarioPaciente",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "UsuarioPaciente",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "UsuarioMedico",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "UsuarioMedico",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "UsuarioMedico",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "Identificador",
                table: "Usuario",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioPaciente",
                table: "UsuarioPaciente",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioMedico",
                table: "UsuarioMedico",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioPaciente",
                table: "UsuarioPaciente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioMedico",
                table: "UsuarioMedico");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "UsuarioPaciente");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "UsuarioPaciente");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "UsuarioMedico");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "UsuarioMedico");

            migrationBuilder.DropColumn(
                name: "Identificador",
                table: "Usuario");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "UsuarioPaciente",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "UsuarioMedico",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioMedico_Usuario_Id",
                table: "UsuarioMedico",
                column: "Id",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioPaciente_Usuario_Id",
                table: "UsuarioPaciente",
                column: "Id",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
