using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Api.Atributos;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Profile;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Usuario;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;

namespace MinhaAgendaDeConsultas.Api.Controllers
{
    public class UsuarioController : MinhaAgendaDeConsultasBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(RequisicaoRegistrarUsuarioJson), StatusCodes.Status201Created)]

        public async Task<IActionResult> RegistrarContato(
                [FromServices] IRegistrarUsuarioUseCase useCase,
                [FromBody] RequisicaoRegistrarUsuarioJson request)
        {
            var response = await useCase.Executar(request);
            
            return CreatedAtAction(nameof(RegistrarContato), new { response.Nome, response.Email });
        }

        [HttpGet("por-email")]
        [AtributoAutorizacaoUsuario]
        [ProducesResponseType(typeof(RespostaUsuarioProfileJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterUsuarioPorEmail(
                  [FromQuery] string email,
                [FromServices] IObterUsuarioProfileUseCase useCase)
        {
            var result = await useCase.Executar(email);

            if (result == null)
                return NotFound("Usuário não encontrado.");

            return Ok(result);
        }

    }
}
