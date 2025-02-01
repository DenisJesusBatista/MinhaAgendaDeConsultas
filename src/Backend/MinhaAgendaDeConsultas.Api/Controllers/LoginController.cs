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
        public async Task<IActionResult> Login(
            [FromServices] IFazerLoginUseCase useCase,
                [FromBody] RequisicaoLoginJson request
            )
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }
    }
}
