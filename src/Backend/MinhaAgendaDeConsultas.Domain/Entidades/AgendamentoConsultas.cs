namespace MinhaAgendaDeConsultas.Domain.Entidades
{
    public class AgendamentoConsultas : EntidadeBase
    {
        public long PacienteId { get; set; }
        public long MedicoId { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public bool Aceite { get; set; }
    }
}
