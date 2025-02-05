namespace MinhaAgendaDeConsultas.Domain.Entidades
{
    public class AgendaMedica : EntidadeBase
    {
        public long Id { get; set; }
        public long MedicoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool IsDisponivel { get; set; }
    }
}
