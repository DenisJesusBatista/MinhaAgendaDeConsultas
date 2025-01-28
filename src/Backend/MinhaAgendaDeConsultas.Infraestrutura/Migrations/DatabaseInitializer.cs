using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using MinhaAgendaDeConsultas.Domain.Extension;

namespace MinhaAgendaDeConsultas.Infraestrutura.Migrations
{
    public static class DatabaseInitializer
    {
        public static async Task AtualizarBaseDeDados(IConfiguration configuration, IApplicationBuilder app)
        {
            var conexao = configuration.GetConexao();
            var nomeDatabase = configuration.GetNomeDataBase();


            // Verifica se o banco de dados existe
            bool bancoExiste = await Database.VerificarExistenciaDatabaseAsync(conexao, nomeDatabase);

            if (bancoExiste)
            {
                app.MigrateBancoDadosAsync();
            }
            else
            {
                Database.CriarDatabase(conexao, nomeDatabase);
                app.MigrateBancoDadosAsync();
            }
        }
    }
}
