using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaAgendaDeConsultas.Domain.Entidades
{
    public class AgendamentoConsultas : EntidadeBase
    {
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }       
        public DateTime DataInclusao { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
    }
}
