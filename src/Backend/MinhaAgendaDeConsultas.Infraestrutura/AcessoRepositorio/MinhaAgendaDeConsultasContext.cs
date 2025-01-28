using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MinhaAgendaDeConsultas.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Infraestrutura.AcessoRepositorio
{
    public class MinhaAgendaDeConsultasContext: DbContext
    {
        public MinhaAgendaDeConsultasContext(DbContextOptions<MinhaAgendaDeConsultasContext> options) : base(options) { }

        //Variavel
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*Responsavel em fazer a configuraçoes necessaria para fazer a 
             * conexão da variavel com a tabela usuario*/
            modelBuilder.Entity<Usuario>()
                .HasOne(r => r.Nome);          
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_loggerFactory) // Habilita o log de SQL
                .EnableSensitiveDataLogging(); // Opcional: inclui parâmetros de consulta na saída do log            
        }

        private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter((category, level) =>
                category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                .AddConsole();
        });
    }
}
