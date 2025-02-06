using MinhaAgendaDeConsultas.Communication.Resposta.Agendamento;
using MinhaAgendaDeConsultas.Domain.Repositorios;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Consultar
{
    public class ConsultarAgendamentoConsultasUseCase : IConsultarAgendamentoConsultasUseCase
    {
        private readonly IAgendamentoConsultasConsultasOnlyRepositorio _agendamentoConsultas;

        public ConsultarAgendamentoConsultasUseCase(IAgendamentoConsultasConsultasOnlyRepositorio agendamentoConsultas,
            IAgendamentoConsultasWriteOnlyRepositorio agendamentoConsultasWriteOnlyRepositorio)
        {
            this._agendamentoConsultas = agendamentoConsultas;

        }
        public async Task<IList<ResponseConsultaAgendamentos>> GetAgendamentosMedico(int medicoId)
        {
            var agendamentos = await _agendamentoConsultas.GetAgendamentosMedico(medicoId);
            return agendamentos.Select(x => new ResponseConsultaAgendamentos
            {
                PacienteId = x.PacienteId,
                MedicoId = x.MedicoId,
                DataInclusao = x.DataInclusao,
                DataHoraInicio = x.DataHoraInicio,
                DataHoraFim = x.DataHoraFim

            }).ToList();
        }

        public async Task<IList<ResponseConsultaAgendamentos>> GetAgendamentosPaciente(int pacienteId)
        {
            var agendamentos = await _agendamentoConsultas.GetAgendamentosPaciente(pacienteId);
            return agendamentos.Select(x => new ResponseConsultaAgendamentos
            {
                PacienteId = x.PacienteId,
                MedicoId = x.MedicoId,
                DataInclusao = x.DataInclusao,
                DataHoraInicio = x.DataHoraInicio,
                DataHoraFim = x.DataHoraFim

            }).ToList();
        }
    }
}
