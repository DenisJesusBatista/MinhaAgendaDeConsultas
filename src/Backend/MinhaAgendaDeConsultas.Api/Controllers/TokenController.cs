using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Application.UseCases.RefreshToken;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Profile;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Usuario;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Token;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;
using MinhaAgendaDeConsultas.Exceptions;

namespace MinhaAgendaDeConsultas.Api.Controllers
{
    public class TokenController : MinhaAgendaDeConsultasBaseController
    {
        /// <summary>
        /// Gera um novo Access Token usando o Refresh Token fornecido.
        /// </summary>
        /// <param name="request">Dados necessários para gerar um novo Access Token.</param>
        /// <returns>Um novo Access Token gerado a partir do Refresh Token.</returns>
        /// <response code="200">Sucesso. Retorna um novo Access Token e o novo Refresh Token.</response>
        /// <response code="400">Requisição malformada. O corpo da requisição está incorreto.</response>
        /// <response code="401">Token de Refresh inválido ou expirado.</response>
        /// <response code="404">Refresh Token não encontrado ou não existe para o usuário.</response>
        /// <response code="500">Erro interno no servidor. Não foi possível processar a requisição.</response>

        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(RespostaTokenJson), StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(
                [FromServices] IUseRefreshTokenUseCase useCase,
                [FromQuery] RequisicaoNovoTokenJson request)
        {
            var response = await useCase.Executar(request);
            return Ok(response);
        }
     

    }
}
