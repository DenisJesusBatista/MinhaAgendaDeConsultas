using MinhaAgendaDeConsultas.Communication.Resposta.Agendamento;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Consultar
{
    public interface IConsultarAgendamentoConsultasUseCase
    {
        public Task<IList<ResponseConsultaAgendamentos>> GetAgendamentosPaciente(int pacienteId);
        public Task<IList<ResponseConsultaAgendamentos>> GetAgendamentosMedico(int medicoId);
    }
}
