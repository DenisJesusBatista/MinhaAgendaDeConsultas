using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Profile;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Medico;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Medico;
using MinhaAgendaDeConsultas.Exceptions;

namespace MinhaAgendaDeConsultas.Api.Controllers
{
    public class MedicoController : MinhaAgendaDeConsultasBaseController
    {


        /// <summary>
        /// Cadastra um medico no sistema.
        /// </summary>
        /// <param name="request">Dados do medico a ser cadastrado.</param>
        /// <returns></returns>
        /// <response code="200">Sucesso no cadastro do medico.</response>
        /// <response code="400">Corpo da requisição diferente do esperado.</response>
        /// <response code="409">O medico informado já está cadastrado.</response>

        [HttpPost]
        [ProducesResponseType(typeof(RequisicaoRegistrarMedicoJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegistrarMedico(
              [FromServices] IRegistrarMedicoUseCase useCase,
              [FromQuery] RequisicaoRegistrarMedicoJson request)
        {
           var result =  await useCase.Executar(request);

            return Ok(result);
        }

        /// <summary>
        /// Recupera médicos com base na especialidade informada.
        /// </summary>
        /// <param name="request">Dados para buscar médicos por especialidade.</param>
        /// <returns>Lista de médicos encontrados com a especialidade informada.</returns>
        /// <response code="200">Lista de médicos retornada com sucesso.</response>
        /// <response code="400">Requisição inválida.</response>
        /// <response code="404">Nenhum médico encontrado para a especialidade informada.</response>
        [HttpGet("por-especialidade")]
        [ProducesResponseType(typeof(RequisicaoRegistrarMedicoJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterMedicoPorEspecialidade([FromQuery] RequisicaoMedicoPorEspecialidadeJson request,[FromServices] IObterUsuarioProfileUseCase useCase)
        {
            var result = await useCase.Executar(request);

            if (result == null)
                return NotFound(ResourceMessagesExceptions.MEDICO_NAO_ENCONTRADO_ESPECIALIDADE);

            return Ok(result);
        }
    }
}
