using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Usuario;
using MinhaAgendaDeConsultas.Communication.Resposta.Agendamento;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Repositorios;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Consultar
{
    public class ConsultarAgendamentoConsultasUseCase : IConsultarAgendamentoConsultasUseCase
    {
        private readonly IAgendamentoConsultasConsultasOnlyRepositorio _agendamentoConsultas;
        private readonly IUsuarioReadOnlyRepositorio _usuarioRepository;

        public ConsultarAgendamentoConsultasUseCase(IAgendamentoConsultasConsultasOnlyRepositorio agendamentoConsultas,
            IAgendamentoConsultasWriteOnlyRepositorio agendamentoConsultasWriteOnlyRepositorio,
            IUsuarioReadOnlyRepositorio usuarioRepository)
        {
            this._agendamentoConsultas = agendamentoConsultas;
            this._usuarioRepository = usuarioRepository;

        }
        public async Task<IList<ResponseConsultaAgendamentos>> GetAgendamentosMedico(string medicoEmail)
        {
            var medico =await _usuarioRepository.RecuperarPorEmail(medicoEmail);

            var agendamentos = await _agendamentoConsultas.GetAgendamentosMedico(medico.Id);
            return agendamentos.Select(x => new ResponseConsultaAgendamentos
            {
                PacienteId = x.PacienteId,
                MedicoId = x.MedicoId,
                DataInclusao = x.DataInclusao,
                DataHoraInicio = x.DataHoraInicio,
                DataHoraFim = x.DataHoraFim

            }).ToList();
        }

        public async Task<IList<ResponseConsultaAgendamentos>> GetAgendamentosPaciente(string pacienteEmail)
        {


            var paciente = await _usuarioRepository.RecuperarPorEmail(pacienteEmail);

            var agendamentos = await _agendamentoConsultas.GetAgendamentosPaciente(paciente.Id);
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
