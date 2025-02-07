using MinhaAgendaDeConsultas.Communication.Resposta.Agendamento;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Consultar
{
    public interface IConsultarAgendamentoConsultasUseCase
    {
        public Task<IList<ResponseConsultaAgendamentos>> GetAgendamentosPaciente(string pacienteEmail);
        public Task<IList<ResponseConsultaAgendamentos>> GetAgendamentosMedico(string medicoEmail);
    }
}
