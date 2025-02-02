using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Medico;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Medico;

namespace MinhaAgendaDeConsultas.Api.Controllers
{
    public class MedicoController : MinhaAgendaDeConsultasBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(RequisicaoRegistrarMedicoJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegistrarMedico(
              [FromServices] IRegistrarMedicoUseCase useCase,
              [FromBody] RequisicaoRegistrarMedicoJson request)
        {
            await useCase.Executar(request);

            return Ok(request);
        }
    }
}
