using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Application.UseCases.Login.FazerLogin;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Login;
using MinhaAgendaDeConsultas.Communication.Responses;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;

namespace MinhaAgendaDeConsultas.Api.Controllers
{
    public class LoginController : MinhaAgendaDeConsultasBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegistrarUsuarioJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespostaErroJson), StatusCodes.Status401Unauthorized)]
        //[Authorize]

        /// <summary>
        /// Gerar uma token para fazer a autenticação do usuário.
        /// </summary>
        /// <param name="request">Dados para gerar uma token.</param>
        /// <returns></returns>
        /// <response code="200">Sucesso na geração da token.</response>
        /// <response code="400">Corpo da requisição diferente do esperado.</response>
        /// <response code="401">Não foi possível autenticar o usuário com os dados fornecidos.</response>
        public async Task<IActionResult> Login(
            [FromServices] IFazerLoginUseCase useCase,
                [FromQuery] RequisicaoLoginJson request
            )
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }
    }
}
