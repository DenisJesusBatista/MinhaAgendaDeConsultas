using FluentValidation;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Medico;
using MinhaAgendaDeConsultas.Exceptions;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Medico
{
    public class RegistrarMedicoValidator : AbstractValidator<RequisicaoRegistrarMedicoJson>
    {
        public RegistrarMedicoValidator()
        {
            /*Validacao das propriedades*/

            RuleFor(medico => medico.Cpf).NotEmpty().WithMessage(ResourceMessagesExceptions.CPF_INVALIDO);
            RuleFor(medico => medico.Crm).NotEmpty().WithMessage(ResourceMessagesExceptions.CRM_VAZIO);
            RuleFor(medico => medico.Nome).NotEmpty().WithMessage(ResourceMessagesExceptions.NOME_VAZIO);


        }
    }
}
