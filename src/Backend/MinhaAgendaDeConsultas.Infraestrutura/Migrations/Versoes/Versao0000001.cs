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
            CriarTabelaUsuarioPaciente();
            CriarTabelaUsuarioMedico();
        }

        private void CriarTabelaUsuario()
        {
            var tabela = VersaoBase.InserirColunasPadrao(Create.Table("Usuario"));

            tabela
                .WithColumn("Nome").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Cpf").AsString(2000)
                .WithColumn("Senha").AsString(2000).NotNullable()
                .WithColumn("Tipo").AsString(20).NotNullable()
                .WithColumn("Identificador").AsGuid().NotNullable();

            _logger.LogInformation("Tabela 'Usuario' criada com sucesso.");
        }

        private void CriarTabelaUsuarioPaciente()
        {
            var tabela = VersaoBase.InserirColunasPadrao(Create.Table("UsuarioPaciente"));

            tabela
                .WithColumn("Nome").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Senha").AsString(2000).NotNullable()
                .WithColumn("Cpf").AsString(2000)                
                .WithColumn("Crm").AsString(20)
                .WithColumn("Tipo").AsString(20).NotNullable()
                .WithColumn("Identificador").AsGuid().NotNullable();


            _logger.LogInformation("Tabela 'UsuarioPaciente' criada com sucesso.");
        }

        private void CriarTabelaUsuarioMedico()
        {
            var tabela = VersaoBase.InserirColunasPadrao(Create.Table("UsuarioMedico"));

            tabela
                .WithColumn("Nome").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Senha").AsString(2000).NotNullable()
                .WithColumn("Cpf").AsString(2000)
                .WithColumn("Crm").AsString(20)
                .WithColumn("Tipo").AsString(20).NotNullable()
                .WithColumn("Identificador").AsGuid().NotNullable();


            _logger.LogInformation("Tabela 'UsuarioMedico' criada com sucesso.");
        }
    }
}
