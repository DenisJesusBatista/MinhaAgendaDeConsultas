using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Alterar;
using MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Consultar;
using MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Excluir;
using MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Registrar;
using MinhaAgendaDeConsultas.Communication.Requisicoes;

namespace MinhaAgendaDeConsultas.Api.Controllers
{
    public class AgendamentoController : MinhaAgendaDeConsultasBaseController
    {
       
        /// <summary>
        /// Cria um novo agendamento no sistema 
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="agendamento"></param>
        /// <returns></returns>
        [HttpPost("/CriarAgendamento")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //  [Authorize]
        public async Task<IActionResult> CriarAgendamento(
           [FromServices] IRegistrarAgendamentoConsultasUseCase useCase,
           [FromBody] RequisicaoAgendamentoConsultasJson agendamento)
        {
            var response = await useCase.Executar(agendamento);
            return Ok(response);
        }

        /// <summary>
        /// Altera um agendamento no sistema
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="agendamento"></param>
        /// <returns></returns>
        [HttpPut("/AlterarAgendamento")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    [Authorize]
        public async Task<IActionResult> AlterarAgendamento(
           [FromServices] IAlterarConsultaAgendamentosUseCase useCase,
           [FromBody] RequisicaoAgendamentoConsultasJson agendamento)
        {
            var response = await useCase.Executar(agendamento);
            return Ok(response);
        }

        /// <summary>
        /// Exclui um agendamento no sistema
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/ExcluiAgendamento/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //    [Authorize]
        public async Task<IActionResult> ExcluiAgendamento(
            [FromServices] IExcluirConsultaAgendamentoUseCase useCase,
            [FromQuery] long id)
        {
            var result = await useCase.Executar(id);
            return Ok(result);
        }

        /// <summary>
        /// Consulta os agendamentos de um médico
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/ConsultarAgendamentoMedico")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //  [Authorize]
        public async Task<IActionResult> ConsultarAgendamentoMedico(
            [FromServices] IConsultarAgendamentoConsultasUseCase useCase,
            [FromQuery] string emailMedico)
        {
            

            var result = await useCase.GetAgendamentosMedico(emailMedico);
            return Ok(result);
        }

        /// <summary>
        /// Consulta os agendamentos de um paciente
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/ConsultaAgendamentoPaciente")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [Authorize]
        public async Task<IActionResult> ConsultaAgendamentoPaciente(
            [FromServices] IConsultarAgendamentoConsultasUseCase useCase,
            [FromQuery] string emailPaciente)
        {
            var result = await useCase.GetAgendamentosPaciente(emailPaciente);
            return Ok(result);
        }



    }
}
