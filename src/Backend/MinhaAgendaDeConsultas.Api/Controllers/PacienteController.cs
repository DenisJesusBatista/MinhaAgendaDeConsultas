using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Paciente;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Paciente;

namespace MinhaAgendaDeConsultas.Api.Controllers
{
    public class PacienteController : MinhaAgendaDeConsultasBaseController
    {
        [HttpPost]
        public async Task<IActionResult> RegistrarPaciente(
              [FromServices] IRegistrarPacienteUseCase useCase,
              [FromBody] RequisicaoRegistrarPacienteJson request)
        {
            await useCase.Executar(request);

            return Ok(request);
        }
    }
}
