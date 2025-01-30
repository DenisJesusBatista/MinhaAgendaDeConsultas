using FluentMigrator;
using Microsoft.Extensions.Logging;
using MinhaAgendaDeConsultas.Infraestrutura.Logging;

namespace MinhaAgendaDeConsultas.Infraestrutura.Migrations.Versoes
{
    [Migration((long)NumeroVersoes.CriarPopularTabelas, "Cria e popular tabelas")]
    public class Versao0000001 : Migration
    {
        private readonly ILogger<Migration> _logger;

        public Versao0000001(ILogger<Migration> logger)
        {
            _logger = logger;
            CustomLogger.Arquivo = true;
        }

        public override void Down()
        {
        }

        public override void Up()
        {
            CustomLogger.Arquivo = true;

            CriarTabelaUsuario();
        }

        private void CriarTabelaUsuario()
        {
            var tabela = VersaoBase.InserirColunasPadrao(Create.Table("Usuarios"));

            tabela
                .WithColumn("Nome").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Senha").AsString(2000).NotNullable()
                .WithColumn("Identificador").AsGuid().NotNullable();

            _logger.LogInformation("Tabela 'Usuarios' criada com sucesso.");
        }
    }
}
