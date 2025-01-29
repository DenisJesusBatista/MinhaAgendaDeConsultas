using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Api.Response;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar;
using MinhaAgendaDeConsultas.Communication.Request;
using MinhaAgendaDeConsultas.Communication.Responses;

namespace MinhaAgendaDeConsultas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegistrarContato(
                [FromServices] IRegistrarUsuarioUseCase useCase,
                [FromBody] RequisicaoRegistrarUsuarioJson request)
        {
            await useCase.Executar(request);

            return Ok(ResponseMessages.UsuarioCriado);
        }        
    }
}
