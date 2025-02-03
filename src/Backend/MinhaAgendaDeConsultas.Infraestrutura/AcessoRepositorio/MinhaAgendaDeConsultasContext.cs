using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MinhaAgendaDeConsultas.Domain.Entidades;

public class MinhaAgendaDeConsultasContext : DbContext
{
    public MinhaAgendaDeConsultasContext(DbContextOptions<MinhaAgendaDeConsultasContext> options) : base(options) { }


    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Medico> Medicos { get; set; }
    public DbSet<AgendamentoConsultas> AgendamentoConsultas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MinhaAgendaDeConsultasContext).Assembly);

        // Configuração para a entidade Usuario (herda de EntidadeBase)
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id); // Chave primária

            entity.Property(e => e.Nome).IsRequired().HasColumnType("character varying");
            entity.Property(e => e.Email).IsRequired().HasColumnType("character varying");
            entity.Property(e => e.Senha).HasMaxLength(128).IsRequired();
            entity.Property(u => u.Identificador)
            .HasColumnType("uuid"); // Força o tipo correto
            entity.Property(e => e.IdentificadorString);

            // Adicionando a configuração para o Token
            entity.Property(e => e.Token)
                .HasMaxLength(512) // Definindo o comprimento máximo para o token
                .IsRequired(false); // O Token pode ser nulo ou vazio, caso o usuário ainda não tenha um token


            // Garanta que Cpf seja tratado como string, mesmo que no banco seja 'character varying'
            entity.Property(e => e.Cpf).HasMaxLength(11).IsRequired().HasColumnType("character varying");

            entity.ToTable("Usuario");
        });




        // Configuração para Paciente (herda de Usuario)
        modelBuilder.Entity<Paciente>(entityPaciente =>
        {
            entityPaciente.Property(e => e.Nome).IsRequired();
            entityPaciente.Property(e => e.Email).IsRequired();
            // O Cpf é obrigatório para Paciente
            entityPaciente.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsRequired();  // Cpf obrigatório para Paciente

            // Configuração do relacionamento com Usuario
            entityPaciente.HasOne(p => p.Usuario) // Paciente tem um Usuario
                         .WithMany() // Usuario pode ter muitos Pacientes (se aplicável)
                         .HasForeignKey(p => p.UsuarioId) // Chave estrangeira
                         .OnDelete(DeleteBehavior.Restrict); // Comportamento de deleção




            // Definindo que o Paciente será mapeado para a tabela 'UsuarioPaciente'
            entityPaciente.ToTable("UsuarioPaciente");
        });

        // Configuração para Medico (herda de Usuario)
        modelBuilder.Entity<Medico>(entityMedico =>
        {
            entityMedico.Property(e => e.Nome).IsRequired();
            entityMedico.Property(e => e.Email).IsRequired();
            // Configurações específicas de Medico, como o CRM e CPF
            entityMedico.Property(m => m.Crm).IsRequired();
            entityMedico.Property(m => m.Cpf).IsRequired();

            // Configuração do relacionamento com Usuario
            entityMedico.HasOne(m => m.Usuario) // Medico tem um Usuario
                       .WithMany() // Usuario pode ter muitos Medicos (se aplicável)
                       .HasForeignKey(m => m.UsuarioId) // Chave estrangeira
                       .OnDelete(DeleteBehavior.Restrict); // Comportamento de deleção

            // Definindo que o Medico será mapeado para a tabela 'UsuarioMedico'
            entityMedico.ToTable("UsuarioMedico");
        });

        modelBuilder.Entity<AgendamentoConsultas>(agendamento =>
        {
            agendamento.ToTable(nameof(AgendamentoConsultas));
            agendamento.HasKey(x => x.Id).HasName("Id");
            agendamento.Property(e=> e.Id).ValueGeneratedOnAdd();
            agendamento.Property(e => e.Ativo);


            agendamento.Property(e => e.DataHoraInicio).IsRequired();
            agendamento.Property(e => e.DataHoraFim).IsRequired();
            agendamento.Property(e => e.MedicoId).IsRequired();
            agendamento.Property(e => e.PacienteId).IsRequired();
            agendamento.Property(e => e.DataInclusao).IsRequired();
            
            
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
    }
    );
}
