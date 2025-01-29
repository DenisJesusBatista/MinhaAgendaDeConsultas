using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Application.UseCases.Login.DoLogin;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar;
using MinhaAgendaDeConsultas.Communication.Request;
using MinhaAgendaDeConsultas.Communication.Requisicoes;
using MinhaAgendaDeConsultas.Communication.Responses;

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
