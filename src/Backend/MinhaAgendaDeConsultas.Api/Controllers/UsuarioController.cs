using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Profile;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Usuario;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;
using MinhaAgendaDeConsultas.Exceptions;

namespace MinhaAgendaDeConsultas.Api.Controllers
{
    public class UsuarioController : MinhaAgendaDeConsultasBaseController
    {
        /// <summary>
        /// Cadastra um novo usuário no sistema
        /// </summary>
        /// <param name="request">Dados para cadastrar um usuário.</param>
        /// <returns></returns>
        /// <response code="200">Sucesso no cadastro do usuário.</response>
        /// <response code="400">Corpo da requisição diferente do esperado.</response>
        /// <response code="409">O login informado não está disponível.</response>       
        [HttpPost]
        [ProducesResponseType(typeof(RequisicaoRegistrarUsuarioJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegistrarContato(
                [FromServices] IRegistrarUsuarioUseCase useCase,
                [FromQuery] RequisicaoRegistrarUsuarioJson request)
        {
            var response = await useCase.Executar(request);

            return CreatedAtAction(nameof(RegistrarContato), new { response.Nome, response.Email, response.Tokens.AcessoToken });
        }


        /// <summary>
        /// Recuperar usuário por e-mail.
        /// </summary>
        /// <param name="request">Dados para cadastrar um usuário.</param>
        /// <returns></returns>
        /// <response code="200">Sucesso no cadastro do usuário.</response>
        /// <response code="400">Corpo da requisição diferente do esperado.</response>

        /// <response code="409">O login informado não está disponível.</response
  
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
