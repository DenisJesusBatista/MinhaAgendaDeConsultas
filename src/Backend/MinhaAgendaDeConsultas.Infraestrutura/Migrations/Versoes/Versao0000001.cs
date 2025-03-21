﻿using FluentMigrator;
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
            CriaTabelaAgendamentoConsulta();
            CriaTabelaAgendaMedica();
            CriarTabelaRefreshToken();
        }

        private void CriarTabelaRefreshToken()
        {
            var tabela = VersaoBase.InserirColunasPadrao(Create.Table("RefreshToken"));

            tabela
               .WithColumn("Value").AsString(255).NotNullable()
               .WithColumn("UuarioId").AsInt64().NotNullable()
               .ForeignKey("FK_RefreshTokens_Usuario_Id", "Usuario", "Id");


            _logger.LogInformation("Tabela 'RefreshToken' criada com sucesso.");
        }

        private void CriaTabelaAgendaMedica()
        {
            var tabela = VersaoBase.InserirColunasPadrao(Create.Table("AgendaMedica"));
            tabela
                .WithColumn("Id").AsInt32().PrimaryKey().Identity() // Chave primária autoincrementável
                .WithColumn("MedicoId").AsInt32().NotNullable() // ID do médico
                .WithColumn("DataInicio").AsDateTime().NotNullable() // Data de início da agenda médica
                .WithColumn("DataFim").AsDateTime().NotNullable() // Data de fim da agenda médica
                .WithColumn("IsDisponivel").AsBoolean().NotNullable(); // Indica se está disponível

            _logger.LogInformation("Tabela 'AgendaMedica' criada com sucesso.");
        }

        private void CriaTabelaAgendamentoConsulta()
        {
            var tabela = VersaoBase.InserirColunasPadrao(Create.Table("AgendamentoConsultas"));
            tabela
                .WithColumn("PacienteId").AsInt32().NotNullable()
                .WithColumn("MedicoId").AsInt32().NotNullable()
                .WithColumn("DataInclusao").AsDateTime().NotNullable()
                .WithColumn("DataHoraInicio").AsDateTime().NotNullable()
                .WithColumn("DataHoraFim").AsDateTime().NotNullable()
                .WithColumn("Ativo").AsBoolean();
            _logger.LogInformation("Tabela 'AgendamentoConsultas' criada com sucesso.");

        }
        private void CriarTabelaUsuario()
        {
            var tabela = VersaoBase.InserirColunasPadrao(Create.Table("Usuario"));

            tabela
                 .WithColumn("Nome").AsString(255).NotNullable()
                 .WithColumn("Email").AsString(255).NotNullable()
                 .WithColumn("Cpf").AsString(2000)  // Considerando que CPF tenha um tamanho razoável
                 .WithColumn("Senha").AsString(2000).NotNullable()  // Tamanho do hash de senha
                 .WithColumn("Ativo").AsBoolean().NotNullable().WithDefaultValue(true)
                 .WithColumn("Tipo").AsInt32().NotNullable()  // Tipo de usuário (ex: Medico, Paciente)
                 .WithColumn("Identificador").AsGuid().NotNullable()  // GUID para identificar o usuário
                 .WithColumn("IdentificadorString").AsString(36).NotNullable()  // 36 caracteres para GUID em formato string
                 .WithColumn("Token").AsString(1000).Nullable();  // Adicionando a coluna para armazenar o token gerado

            _logger.LogInformation("Tabela 'Usuario' criada com sucesso.");

        }

        private void CriarTabelaUsuarioPaciente()
        {
            var tabela = VersaoBase.InserirColunasPadrao(Create.Table("UsuarioPaciente"));

            tabela
                .WithColumn("UsuarioId").AsInt64().NotNullable() // Chave estrangeira
                .WithColumn("Nome").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Cpf").AsString(2000)
                .WithColumn("Tipo").AsString(20);

            //tabela
            //    .ForeignKey("FK_UsuarioPaciente_Usuario", "Usuario", "Id")
            //    .OnDeleteOrUpdate(Rule.None); // Equivalente ao DeleteBehavior.Restrict


            _logger.LogInformation("Tabela 'UsuarioPaciente' criada com sucesso.");
        }

        private void CriarTabelaUsuarioMedico()
        {
            var tabela = VersaoBase.InserirColunasPadrao(Create.Table("UsuarioMedico"));

            tabela
                .WithColumn("UsuarioId").AsInt64().NotNullable() // Chave estrangeira
                .WithColumn("Nome").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Cpf").AsString(2000)
                .WithColumn("Crm").AsString(20)
                .WithColumn("Especialidade").AsString(200).NotNullable()
                .WithColumn("Tipo").AsString(20);

            //tabela
            //     .ForeignKey("FK_UsuarioMedico_Usuario", "Usuario", "Id")
            //     .OnDeleteOrUpdate(Rule.None); // Equivalente ao DeleteBehavior.Restrict


            _logger.LogInformation("Tabela 'UsuarioMedico' criada com sucesso.");
        }
    }
}
