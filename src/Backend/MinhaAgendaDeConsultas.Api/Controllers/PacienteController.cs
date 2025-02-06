using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Api.Atributos;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Paciente;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Paciente;
using MinhaAgendaDeConsultas.Domain.Enumeradores;


namespace MinhaAgendaDeConsultas.Api.Controllers
{
    public class PacienteController : MinhaAgendaDeConsultasBaseController
    {


        /// <summary>
        /// Cadastra um paciente no sistema.
        /// </summary>
        /// <param name="request">Dados do paciente a ser cadastrado.</param>
        /// <returns></returns>
        /// <response code="200">Sucesso no cadastro do paciente.</response>
        /// <response code="400">Corpo da requisição diferente do esperado.</response>
        /// <response code="409">O paciente informado já está cadastrado.</response>

        [HttpPost]
        [AtributoAutorizacaoUsuario(Roles = nameof(PerfilUsuario.Paciente))]

        [ProducesResponseType(typeof(RequisicaoRegistrarPacienteJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegistrarPaciente(
              [FromServices] IRegistrarPacienteUseCase useCase,
              [FromQuery] RequisicaoRegistrarPacienteJson request)
        {
            await useCase.Executar(request);

            return Ok(request);
        }
    }
}
