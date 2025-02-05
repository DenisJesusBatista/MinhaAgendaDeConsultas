using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MinhaAgendaDeConsultas.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class AgendaMedica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "AgendaMedica",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedicoId = table.Column<long>(type: "bigint", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDisponivel = table.Column<bool>(type: "boolean", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendaMedica");

            migrationBuilder.DropPrimaryKey(
                name: "Id",
                table: "AgendamentoConsultas");

            migrationBuilder.DropColumn(
                name: "Aceite",
                table: "AgendamentoConsultas");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "AgendamentoConsultas");

            migrationBuilder.DropColumn(
                name: "DataInclusao",
                table: "AgendamentoConsultas");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Usuario",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuario",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Usuario",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataHoraInicio",
                table: "AgendamentoConsultas",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataHoraFim",
                table: "AgendamentoConsultas",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
