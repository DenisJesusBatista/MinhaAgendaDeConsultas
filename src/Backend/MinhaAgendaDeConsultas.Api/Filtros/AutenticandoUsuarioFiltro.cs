using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MinhaAgendaDeConsultas.Communication.Responses;
using MinhaAgendaDeConsultas.Domain.Seguranca.Token;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;
using MinhaAgendaDeConsultas.Exceptions;

namespace MinhaAgendaDeConsultas.Api.Filtros
{
    public class AutenticandoUsuarioFiltro: IAsyncAuthorizationFilter
    {
        private readonly IValidadorTokenAcesso _validadorTokenAcesso;
        private readonly IUsuarioReadOnlyRepositorio _repository;

        public AutenticandoUsuarioFiltro(IValidadorTokenAcesso validadorTokenAcesso, IUsuarioReadOnlyRepositorio repository)
        {
            _validadorTokenAcesso = validadorTokenAcesso;
            _repository = repository;
        }


        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                // Obtém o token da requisição
                var token = TokenOnRequest(context);

                if (string.IsNullOrEmpty(token))
                {
                    // Retorna erro caso não tenha token na requisição
                    context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMessagesExceptions.SEM_TOKEN));
                    return;
                }

                // Valida o token e obtém o identificador do usuário
                var usuarioIdentificador = _validadorTokenAcesso.ValidarEObterIdentificadorUsario(token);

                // Verifica se o usuário tem permissão (usuário ativo)
                var existe = await _repository.ExisteUsarioAtivoComIdentificador(usuarioIdentificador);
                if (!existe)
                {
                    // Retorna erro 403 - Forbidden, caso o usuário não tenha permissão
                    context.Result = new ForbidResult(); // Ou pode retornar new UnauthorizedResult() para erro 401
                    return;
                }
            }
            catch (TokenExpiredException)
            {
                // Caso o token tenha expirado, retornamos uma resposta personalizada
                context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMessagesExceptions.TOKEN_EXPIRADO)
                {
                    TokenIsExpired = true
                });
            }
            catch (Exception ex)
            {
                // Captura qualquer outra exceção geral
                context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMessagesExceptions.ERRO_GENERICO)
                {
                    Detalhes = ex.Message
                });
            }
        }


        private string TokenOnRequest(AuthorizationFilterContext context)
        {
            // Obtém o token do cabeçalho Authorization da requisição
            var token = context.HttpContext.Request.Headers.Authorization.ToString();
            if (string.IsNullOrEmpty(token))
            {
                // Se não houver token, retorna um erro personalizado
                throw new TokenNotFoundException(ResourceMessagesExceptions.SEM_TOKEN);
            }

            // Retorna o token, removendo o prefixo "Bearer "
            return token["Bearer ".Length..].Trim();
        }


    }
}

