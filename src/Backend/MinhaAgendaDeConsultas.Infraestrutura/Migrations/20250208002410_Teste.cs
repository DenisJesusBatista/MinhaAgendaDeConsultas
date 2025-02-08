using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MinhaAgendaDeConsultas.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class Teste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgendamentoConsultas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PacienteId = table.Column<long>(type: "bigint", nullable: false),
                    MedicoId = table.Column<long>(type: "bigint", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataHoraInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataHoraFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Aceite = table.Column<bool>(type: "boolean", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying", nullable: false),
                    Email = table.Column<string>(type: "character varying", nullable: false),
                    Senha = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Cpf = table.Column<string>(type: "character varying", maxLength: 11, nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Identificador = table.Column<Guid>(type: "uuid", nullable: false),
                    IdentificadorString = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioMedico",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cpf = table.Column<string>(type: "text", nullable: false),
                    Crm = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Especialidade = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioMedico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioMedico_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioPaciente",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPaciente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioPaciente_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntidadeBase",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    MedicoId = table.Column<long>(type: "bigint", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDisponivel = table.Column<bool>(type: "boolean", nullable: true),
                    DataHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Disponivel = table.Column<bool>(type: "boolean", nullable: true),
                    HorarioDisponivel_MedicoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntidadeBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntidadeBase_UsuarioMedico_HorarioDisponivel_MedicoId",
                        column: x => x.HorarioDisponivel_MedicoId,
                        principalTable: "UsuarioMedico",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntidadeBase_HorarioDisponivel_MedicoId",
                table: "EntidadeBase",
                column: "HorarioDisponivel_MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UsuarioId",
                table: "RefreshToken",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioMedico_UsuarioId",
                table: "UsuarioMedico",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPaciente_UsuarioId",
                table: "UsuarioPaciente",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendamentoConsultas");

            migrationBuilder.DropTable(
                name: "EntidadeBase");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "UsuarioPaciente");

            migrationBuilder.DropTable(
                name: "UsuarioMedico");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
