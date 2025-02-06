using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MinhaAgendaDeConsultas.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelaRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntidadeBase_Usuario_UsuarioId",
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
                name: "UsuarioId",
                table: "EntidadeBase");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "EntidadeBase");

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

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UsuarioId",
                table: "RefreshToken",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

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
                name: "IX_EntidadeBase_UsuarioId",
                table: "EntidadeBase",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntidadeBase_Usuario_UsuarioId",
                table: "EntidadeBase",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
