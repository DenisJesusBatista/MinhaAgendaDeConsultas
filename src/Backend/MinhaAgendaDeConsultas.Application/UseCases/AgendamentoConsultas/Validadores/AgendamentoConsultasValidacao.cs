using FluentValidation;
using MinhaAgendaDeConsultas.Communication.Requisicoes;
using MinhaAgendaDeConsultas.Exceptions;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Validadores
{
    public class AgendamentoConsultasValidacao:AbstractValidator<RequisicaoAgendamentoConsultasJson>
    {
        public AgendamentoConsultasValidacao()
        {
            RuleFor(agendamento => agendamento.DataHoraInicio).NotEmpty().WithMessage(ResourceMessagesExceptions.DATA_HORA_VAZIO);
            RuleFor(agendamento => agendamento.DataHoraFim).NotEmpty().WithMessage(ResourceMessagesExceptions.DATA_HORA_VAZIO);
            RuleFor(agendamento => agendamento.MedicoId).NotEmpty().WithMessage(ResourceMessagesExceptions.MEDICO_NAO_INFORMADO);
            RuleFor(agendamento => agendamento.PacienteId).NotEmpty().WithMessage(ResourceMessagesExceptions.PACIENTE_DEVE_SER_INFORMADO);
            RuleFor(agendamento=> agendamento.DataHoraInicio).LessThan(agendamento => agendamento.DataHoraFim).WithMessage(ResourceMessagesExceptions.DATA_HORA_INICIO_MAIOR_DATA_HORA_FIM);
            RuleFor(agendamento => agendamento.DataHoraInicio).GreaterThan(agendamento => agendamento.DataInclusao).WithMessage(ResourceMessagesExceptions.DATA_HORA_INICIO_MAIOR_DATA_INCLUSAO);

         
        }
    }
}
