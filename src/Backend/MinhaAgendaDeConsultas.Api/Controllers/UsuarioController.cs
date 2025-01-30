using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Api.Response;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar;
using MinhaAgendaDeConsultas.Communication.Request;
using MinhaAgendaDeConsultas.Communication.Responses;

namespace MinhaAgendaDeConsultas.Api.Controllers
{   
    public class UsuarioController : MinhaAgendaDeConsultasBaseController
    {
        [HttpPost]
        public async Task<IActionResult> RegistrarContato(
                [FromServices] IRegistrarUsuarioUseCase useCase,
                [FromBody] RequisicaoRegistrarUsuarioJson request)
        {
            await useCase.Executar(request);

            return Ok(request);
        }
    }
}
