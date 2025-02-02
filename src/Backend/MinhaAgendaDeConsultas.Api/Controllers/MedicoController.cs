using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Medico;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Medico;

namespace MinhaAgendaDeConsultas.Api.Controllers
{
    public class MedicoController : MinhaAgendaDeConsultasBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(RequisicaoRegistrarMedicoJson), StatusCodes.Status201Created)]

        /// <summary>
        /// Cadastra um medico no sistema.
        /// </summary>
        /// <param name="request">Dados do medico a ser cadastrado.</param>
        /// <returns></returns>
        /// <response code="200">Sucesso no cadastro do medico.</response>
        /// <response code="400">Corpo da requisição diferente do esperado.</response>
        /// <response code="409">O medico informado já está cadastrado.</response>
        public async Task<IActionResult> RegistrarMedico(
              [FromServices] IRegistrarMedicoUseCase useCase,
              [FromQuery] RequisicaoRegistrarMedicoJson request)
        {
            await useCase.Executar(request);

            return Ok(request);
        }
    }
}
