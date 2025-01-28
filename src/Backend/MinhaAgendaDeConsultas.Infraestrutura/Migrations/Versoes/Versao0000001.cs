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
            var tabela = VersaoBase.InserirColunasPadrao(Create.Table("Usuario"));

            tabela
                .WithColumn("Nome").AsString(100).NotNullable()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("Senha").AsString(14).NotNullable();

            _logger.LogInformation("Tabela 'Usuario' criada com sucesso.");
        }
    }
}
