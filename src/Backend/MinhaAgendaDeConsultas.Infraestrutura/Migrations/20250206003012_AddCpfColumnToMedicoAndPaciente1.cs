using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinhaAgendaDeConsultas.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class AddCpfColumnToMedicoAndPaciente1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "EntidadeBase",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataExpiracao",
                table: "EntidadeBase",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHora",
                table: "EntidadeBase",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "EntidadeBase",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Disponivel",
                table: "EntidadeBase",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MedicoId",
                table: "EntidadeBase",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioId",
                table: "EntidadeBase",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "EntidadeBase",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntidadeBase_MedicoId",
                table: "EntidadeBase",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_EntidadeBase_UsuarioId",
                table: "EntidadeBase",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntidadeBase_UsuarioMedico_MedicoId",
                table: "EntidadeBase",
                column: "MedicoId",
                principalTable: "UsuarioMedico",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntidadeBase_Usuario_UsuarioId",
                table: "EntidadeBase",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntidadeBase_UsuarioMedico_MedicoId",
                table: "EntidadeBase");

            migrationBuilder.DropForeignKey(
                name: "FK_EntidadeBase_Usuario_UsuarioId",
                table: "EntidadeBase");

            migrationBuilder.DropIndex(
                name: "IX_EntidadeBase_MedicoId",
                table: "EntidadeBase");

            migrationBuilder.DropIndex(
                name: "IX_EntidadeBase_UsuarioId",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "DataExpiracao",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "DataHora",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "Disponivel",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "EntidadeBase");
        }
    }
}
