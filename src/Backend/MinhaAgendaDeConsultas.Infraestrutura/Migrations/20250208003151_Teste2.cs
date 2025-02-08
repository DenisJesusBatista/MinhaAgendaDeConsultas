using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinhaAgendaDeConsultas.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class Teste2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntidadeBase_UsuarioMedico_HorarioDisponivel_MedicoId",
                table: "EntidadeBase");

            migrationBuilder.DropIndex(
                name: "IX_EntidadeBase_HorarioDisponivel_MedicoId",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "DataHora",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "DataInicio",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "Disponivel",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "HorarioDisponivel_MedicoId",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "IsDisponivel",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "EntidadeBase");

            migrationBuilder.CreateTable(
                name: "AgendaMedica",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MedicoId = table.Column<long>(type: "bigint", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDisponivel = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaMedica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendaMedica_EntidadeBase_Id",
                        column: x => x.Id,
                        principalTable: "EntidadeBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HorarioDisponivel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    DataHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Disponivel = table.Column<bool>(type: "boolean", nullable: false),
                    MedicoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioDisponivel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HorarioDisponivel_EntidadeBase_Id",
                        column: x => x.Id,
                        principalTable: "EntidadeBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HorarioDisponivel_UsuarioMedico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "UsuarioMedico",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HorarioDisponivel_MedicoId",
                table: "HorarioDisponivel",
                column: "MedicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendaMedica");

            migrationBuilder.DropTable(
                name: "HorarioDisponivel");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "EntidadeBase",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHora",
                table: "EntidadeBase",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicio",
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
                name: "HorarioDisponivel_MedicoId",
                table: "EntidadeBase",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisponivel",
                table: "EntidadeBase",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MedicoId",
                table: "EntidadeBase",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntidadeBase_HorarioDisponivel_MedicoId",
                table: "EntidadeBase",
                column: "HorarioDisponivel_MedicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntidadeBase_UsuarioMedico_HorarioDisponivel_MedicoId",
                table: "EntidadeBase",
                column: "HorarioDisponivel_MedicoId",
                principalTable: "UsuarioMedico",
                principalColumn: "Id");
        }
    }
}
