using MinhaAgendaDeConsultas.Domain.Entidades;

namespace MinhaAgendaDeConsultas.Domain.Repositorios
{
    public interface IAgendamentoConsultasConsultasOnlyRepositorio
    {
        /// <summary>
        /// retona as disponibilidades de um medico ou a disponibilidade geral dos médicos
        /// conforme a quantidade máxima de dias 
        /// </summary>
        /// <param name="pacienteId"></param>
        /// <param name="medicoId"></param>
        /// <returns>Retorna uma lista de Agendamentos Disponíveis</returns>
        public Task<bool> GetDisponibilides(DateTime DataDeInicio, DateTime DataFim, long? medicoId = null);
        public Task<IList<AgendamentoConsultas>> GetAgendamentosMedico(int medicoId);
        public Task<IList<AgendamentoConsultas>> GetAgendamentosPaciente(int pacienteId);
    }
}
