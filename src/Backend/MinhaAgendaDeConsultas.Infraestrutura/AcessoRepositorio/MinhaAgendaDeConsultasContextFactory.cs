using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

public class MinhaAgendaDeConsultasContextFactory : IDesignTimeDbContextFactory<MinhaAgendaDeConsultasContext>
{
    public MinhaAgendaDeConsultasContext CreateDbContext(string[] args)
    {
        // Caminho absoluto até o projeto API onde está o appsettings.json
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../MinhaAgendaDeConsultas.Api");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath) // Define a pasta da API como base
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<MinhaAgendaDeConsultasContext>();
        var connectionString = configuration.GetConnectionString("Conexao");

        optionsBuilder.UseNpgsql(connectionString); // Use o provedor correto (Npgsql para PostgreSQL)

        return new MinhaAgendaDeConsultasContext(optionsBuilder.Options);
    }
}
