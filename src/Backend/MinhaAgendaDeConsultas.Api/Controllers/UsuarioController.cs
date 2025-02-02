using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Api.Atributos;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Profile;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Usuario;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Communication.Responses;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;
using MinhaAgendaDeConsultas.Exceptions;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;

namespace MinhaAgendaDeConsultas.Api.Controllers
{
    public class UsuarioController : MinhaAgendaDeConsultasBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(RequisicaoRegistrarUsuarioJson), StatusCodes.Status201Created)]

        public async Task<IActionResult> RegistrarContato(
                [FromServices] IRegistrarUsuarioUseCase useCase,
                [FromQuery] RequisicaoRegistrarUsuarioJson request)
        {
            var response = await useCase.Executar(request);
            
            return CreatedAtAction(nameof(RegistrarContato), new { response.Nome, response.Email, response.Tokens.AcessoToken });
        }

        [HttpGet("por-email")]        
        [ProducesResponseType(typeof(RespostaUsuarioProfileJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterUsuarioPorEmail(
                  //[FromQuery] string email,
                  [FromQuery] RequisicaoObterUsuarioJson request,
                [FromServices] IObterUsuarioProfileUseCase useCase)
        {
            var result = await useCase.Executar(request);

            if (result == null)
                return NotFound(ResourceMessagesExceptions.USUARIO_NAO_ENCONTRADO_EMAIL);           

            return Ok(result);
        }

    }
}
