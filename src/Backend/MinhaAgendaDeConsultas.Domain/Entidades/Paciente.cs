using MinhaAgendaDeConsultas.Domain.Enumeradores;

namespace MinhaAgendaDeConsultas.Domain.Entidades
{
    public class Paciente
    {
        public long Id { get; set; }
        public string? Cpf { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Nome { get; set; }
        public TipoUsuario Tipo { get; set; } // Usando o Enum

        // Chave estrangeira para o Usuario
        public long UsuarioId { get; set; }

        // Propriedade de navegação para o Usuario
        public Usuario Usuario { get; set; }
    }
}
