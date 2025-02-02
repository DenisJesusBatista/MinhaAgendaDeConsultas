using FluentValidation;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Exceptions;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.ObterUsuario
{
    public class ObterUsuarioValidator : AbstractValidator<RequisicaoObterUsuarioJson>
    {
        public ObterUsuarioValidator()
        {

            /*Validacao das propriedades*/

            RuleFor(usuario => usuario.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.USUARIO_NAO_ENCONTRADO_EMAIL);


        }
      
    }
}
