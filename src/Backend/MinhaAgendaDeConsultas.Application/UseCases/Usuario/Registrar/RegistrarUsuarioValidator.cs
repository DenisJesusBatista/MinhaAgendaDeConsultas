using FluentValidation;
using MinhaAgendaDeConsultas.Communication.Request;
using MinhaAgendaDeConsultas.Exceptions;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar
{
    public class RegistrarUsuarioValidator: AbstractValidator<RequestRegistrarUsuarioJson>
    {
        public RegistrarUsuarioValidator()
        {
            /*Validacao das propriedades*/

            RuleFor(user => user.Nome).NotEmpty().WithMessage(ResourceMessagesExceptions.NOME_VAZIO);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.EMAIL_VAZIO);
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALIDO);
            RuleFor(user => user.Senha).MinimumLength(6).WithMessage(ResourceMessagesExceptions.SENHA_MINIMA);
        }
    }
}
