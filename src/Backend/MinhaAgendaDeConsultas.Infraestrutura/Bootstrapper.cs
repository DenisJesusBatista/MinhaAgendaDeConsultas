using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Extension;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain.Repositorios.Usuario;
using MinhaAgendaDeConsultas.Domain.Seguranca.Token;
using MinhaAgendaDeConsultas.Infraestrutura.AcessoRepositorio;
using MinhaAgendaDeConsultas.Infraestrutura.Seguranca.Token.Acesso.Gerador;
using System.Reflection;

namespace MinhaAgendaDeConsultas.Infraestrutura
{
    public static class Bootstrapper
    {
        public static void AddRepositorio(this IServiceCollection services, IConfiguration configurationManager)
        {
            AddFluentMigratorPostgres(services, configurationManager);
            AddUnidadeDeTrabalho(services);
            AddRepositorios(services);
            AddContexto(services, configurationManager);
            AddToken(services, configurationManager);
        }

        private static void AddUnidadeDeTrabalho(this IServiceCollection services)
        {
            services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
        }

        private static void AddContexto(IServiceCollection services, IConfiguration configurationManager)
        {
            var connectionString = configurationManager.GetConexaoCompleta();

            services.AddDbContext<MinhaAgendaDeConsultasContext>(dbCobtextoOpcoes =>
            {
                dbCobtextoOpcoes.UseNpgsql(connectionString);
            });
        }
        private static void AddFluentMigratorPostgres(IServiceCollection services, IConfiguration configurationManager)
        {
            services.AddFluentMigratorCore().ConfigureRunner(c =>
            c.AddPostgres()
            .WithGlobalConnectionString(configurationManager.GetConexaoCompleta()).ScanIn(Assembly.Load("MinhaAgendaDeConsultas.Infraestrutura")).For.All());
        }

        private static void AddRepositorios(IServiceCollection services)
        {
            services.AddScoped<IUsuarioWriteOnlyRepositorio, UsuarioRepositorio>()
                .AddScoped<IUsuarioReadOnlyRepositorio, UsuarioRepositorio>()                
             .AddScoped<IUsuarioUpdateOnlyRepositorio, UsuarioRepositorio>();
        }

        private static void AddToken(IServiceCollection services, IConfiguration configurationManager)
        {
           var expiracaoMinutos = configurationManager.GetValue<uint>("Settings:Jwt:ExpiracaoMinutos");
            //var chaveAssinatura = configurationManager.GetValue<string>("Settings:Jwt:ChaveAssinatura"); 
            var chaveAssinatura = configurationManager.GetValue<string>("Jwt:ChaveAssinatura");

            // Verifique se a chave de assinatura foi configurada corretamente
            if (string.IsNullOrEmpty(chaveAssinatura))
            {
                throw new InvalidOperationException("A chave de assinatura JWT não foi configurada corretamente.");
            }

            services.AddScoped<IGeradorTokenAcesso>(option => new JwtTokenGerador(expiracaoMinutos, chaveAssinatura!));
        }   
    }
}
