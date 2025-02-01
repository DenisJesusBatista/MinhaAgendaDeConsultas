using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Usuario;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;

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
