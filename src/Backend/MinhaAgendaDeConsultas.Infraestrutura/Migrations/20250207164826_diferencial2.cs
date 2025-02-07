using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MinhaAgendaDeConsultas.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class diferencial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
           name: "AgendamentoConsultas",
           columns: table => new
           {
               Id = table.Column<long>(nullable: false)
                   .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
               PacienteId = table.Column<long>(nullable: false),
               MedicoId = table.Column<long>(nullable: false),
               DataInclusao = table.Column<DateTime>(nullable: false),
               DataHoraInicio = table.Column<DateTime>(nullable: false),
               DataHoraFim = table.Column<DateTime>(nullable: false),
               Aceite = table.Column<bool>(nullable: false),
               Ativo = table.Column<bool>(nullable: false)
           },
           constraints: table =>
           {
               table.PrimaryKey("PK_AgendamentoConsultas", x => x.Id);
           });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
           name: "AgendamentoConsultas");
        }
    }
}
