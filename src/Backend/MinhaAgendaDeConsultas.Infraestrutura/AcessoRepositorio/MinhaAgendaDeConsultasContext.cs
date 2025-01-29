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
            base.OnModelCreating(modelBuilder);

            // Certifique-se de que as entidades sejam mapeadas corretamente.
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.Email).IsRequired();

                // Aqui você pode definir o nome da tabela, garantindo que seja 'Usuarios' no banco de dados
                entity.ToTable("Usuario"); // Especifica o nome da tabela no banco de dados

                // Defina o comprimento para a senha criptografada (SHA512 hash precisa de 128 caracteres)
                entity.Property(e => e.Senha)
                      .HasMaxLength(128) // Defina o comprimento para acomodar o hash SHA512
                      .IsRequired();

            });

            // Adicione outras entidades conforme necessário
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
