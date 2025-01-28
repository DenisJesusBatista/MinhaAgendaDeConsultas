using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using MinhaAgendaDeConsultas.Domain.Extension;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Infraestrutura.AcessoRepositorio;
//using MinhaAgendaDeConsultas.Infraestrutura.AcessoRepositorio.Repositorio;
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
    }
}
