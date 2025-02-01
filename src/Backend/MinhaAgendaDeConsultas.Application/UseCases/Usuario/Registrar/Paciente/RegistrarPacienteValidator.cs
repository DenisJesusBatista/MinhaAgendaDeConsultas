using FluentValidation;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Paciente;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Exceptions;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Paciente
{
    public class RegistrarPacienteValidator : AbstractValidator<RequisicaoRegistrarPacienteJson>
    {
        public RegistrarPacienteValidator()
        {
            /*Validacao das propriedades*/

            RuleFor(paciente => paciente.Cpf).NotEmpty().WithMessage(ResourceMessagesExceptions.CPF_INVALIDO);
            RuleFor(paciente => paciente.Nome).NotEmpty().WithMessage(ResourceMessagesExceptions.NOME_VAZIO);

        }
    }
}
