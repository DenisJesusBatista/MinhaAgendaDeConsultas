using MinhaAgendaDeConsultas.Domain.Enumeradores;

namespace MinhaAgendaDeConsultas.Domain.Entidades
{
    public class Medico
    {
        public long Id { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;
        public string? Nome { get; set; }
        public string Email { get; set; } = string.Empty;
        public TipoUsuario Tipo { get; set; } // Usando o Enum

        // Chave estrangeira para o Usuario
        public long UsuarioId { get; set; }

        // Propriedade de navegação para o Usuario
        public Usuario Usuario { get; set; }

        public IEnumerable<HorarioDisponivel> HorariosDisponiveis { get; set; }
    }
}
