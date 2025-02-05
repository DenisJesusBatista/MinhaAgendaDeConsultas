using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Alterar;
using MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Consultar;
using MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Excluir;
using MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Registrar;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Agendamento;

namespace MinhaAgendaDeConsultas.Api.Controllers
{
    public class AgendaMedicaController : MinhaAgendaDeConsultasBaseController
    {
        /// <summary>
        /// Cria uma nova agenda médica no sistema
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="agendaMedica"></param>
        /// <returns></returns>
        [HttpPost("/CriarAgendaMedica")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarAgendaMedica([FromServices] IAgendaMedicaRegistrarUseCase
            useCase, [FromBody] RequisicaoAgendaMedicaJson agendaMedica)
        {
            var response = await useCase.Executar(agendaMedica);
            return Ok(response);
        }

        /// <summary>
        /// Altera uma agenda médica no sistema
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="agendaMedica"></param>
        /// <returns></returns>
        [HttpPut("/AlterarAgendaMedica")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AlterarAgendaMedica([FromServices] IAgendaMedicaAlterarUseCase
            useCase, [FromBody] RequisicaoAgendaMedicaJson agendaMedica)
        {
            var response = await useCase.Executar(agendaMedica);
            return Ok(response);
        }

        /// <summary>
        /// Exclui uma agenda médica no sistema
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/ExcluirAgendaMedica/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExcluirAgendaMedica([FromServices] IAgendaMedicaExcluirUseCase
         useCase, [FromQuery] long id)
        {
            var response = await useCase.Executar(id);
            return Ok(response);
        }

        /// <summary>
        /// Consulta as agendas médicas no sistema
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="requisicaoAgendaMedicaJson"></param>
        /// <returns></returns>
        [HttpPost("/ConsultarAgendaMedica")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConsultaAgendaMedica([FromServices] IAgendaMedicaConsultarUseCase useCase,
            [FromBody] RequisicaoAgendaMedicaJson requisicaoAgendaMedicaJson)
        {
            return Ok(await useCase.ObterAgendasMedicias(requisicaoAgendaMedicaJson.DataInicio, requisicaoAgendaMedicaJson.DataFim, requisicaoAgendaMedicaJson.MedicoEmail));
        }
    }
}
