using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MinhaAgendaDeConsultas.Domain.Entidades;
using MinhaAgendaDeConsultas.Domain.Enumeradores;

public class MinhaAgendaDeConsultasContext : DbContext
{
    public MinhaAgendaDeConsultasContext(DbContextOptions<MinhaAgendaDeConsultasContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Medico> Medicos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração para a entidade Usuario (herda de EntidadeBase)
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id); // Chave primária

            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Senha).HasMaxLength(128).IsRequired();

            // Garanta que Cpf seja tratado como string, mesmo que no banco seja 'character varying'
            entity.Property(e => e.Cpf).HasMaxLength(11).IsRequired();

            entity.ToTable("Usuario");
        });




        // Configuração para Paciente (herda de Usuario)
        modelBuilder.Entity<Paciente>(entity =>
        {
            // O Cpf é obrigatório para Paciente
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsRequired();  // Cpf obrigatório para Paciente

            // Definindo que o Paciente será mapeado para a tabela 'UsuarioPaciente'
            entity.ToTable("UsuarioPaciente");
        });

        // Configuração para Medico (herda de Usuario)
        modelBuilder.Entity<Medico>(entity =>
        {
            // Configurações específicas de Medico, como o CRM e CPF
            entity.Property(m => m.Crm).IsRequired();
            entity.Property(m => m.Cpf).IsRequired();

            // Definindo que o Medico será mapeado para a tabela 'UsuarioMedico'
            entity.ToTable("UsuarioMedico");
        });

        // Configuração da classe base (EntidadeBase)
        modelBuilder.Entity<EntidadeBase>(entity =>
        {
            entity.HasKey(e => e.Id); // A chave primária é configurada na classe base
        });
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
