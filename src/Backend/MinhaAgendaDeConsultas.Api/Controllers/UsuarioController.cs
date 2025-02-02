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
            await useCase.Executar(request);

            return Ok(request);
        }

        [HttpGet]
        [AtributoAutorizacaoUsuario]
        [ProducesResponseType(typeof(RespostaUsuarioProfileJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> ObterUsuarioProfile(
                [FromServices] IObterUsuarioProfileUseCase useCase)
        {
           var result = await useCase.Executar();

            return Ok(result);
        }

    }
}
