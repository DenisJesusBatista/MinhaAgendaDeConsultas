using FluentValidation;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Exceptions;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Usuario
{
    public class RegistrarUsuarioValidator : AbstractValidator<RequisicaoRegistrarUsuarioJson>
    {
        public RegistrarUsuarioValidator()
        {
            /*Validacao das propriedades*/

            RuleFor(user => user.Nome).NotEmpty().WithMessage(ResourceMessagesExceptions.NOME_VAZIO);
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALIDO);
            RuleFor(user => user.Senha).MinimumLength(6).WithMessage(ResourceMessagesExceptions.SENHA_MINIMA);
        }
    }
}
