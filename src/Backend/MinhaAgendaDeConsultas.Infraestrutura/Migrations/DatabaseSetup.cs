using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace MinhaAgendaDeConsultas.Infraestrutura.Migrations
{
    public static class DatabaseSetup
    {
        public static void AtualizarBaseDeDados(IConfiguration configuration, IApplicationBuilder app)
        {
            DatabaseInitializer.AtualizarBaseDeDados(configuration, app);
        }
    }
}
