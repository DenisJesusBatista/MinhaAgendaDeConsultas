using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Application.UseCases.Login.FazerLogin;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Login;
using MinhaAgendaDeConsultas.Communication.Responses;
using MinhaAgendaDeConsultas.Communication.Resposta;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;
using MinhaAgendaDeConsultas.Domain.Servicos.UsuarioLogado;
using MinhaAgendaDeConsultas.Exceptions;
using MinhaAgendaDeConsultas.Infraestrutura.Servicos.UsuarioLogado;

namespace MinhaAgendaDeConsultas.Api.Controllers
{
    public class LoginController : MinhaAgendaDeConsultasBaseController
    {
        private readonly IUsuarioLogado _usuarioLogado;

        public LoginController(IUsuarioLogado usuarioLogado)
        {
            _usuarioLogado = usuarioLogado;

        }

      

        /// <summary>
        /// Gerar uma token para fazer a autenticação do usuário.
        /// </summary>
        /// <param name="request">Dados para gerar uma token.</param>
        /// <returns></returns>
        /// <response code="200">Sucesso na geração da token.</response>
        /// <response code="400">Corpo da requisição diferente do esperado.</response>
        /// <response code="401">Não foi possível autenticar o usuário com os dados fornecidos.</response>

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegistrarUsuarioJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespostaErroJson), StatusCodes.Status401Unauthorized)]
        //[Authorize]

        public async Task<IActionResult> Login(
                        [FromServices] IFazerLoginUseCase useCase,
                        [FromQuery] RequisicaoLoginJson request
)
        {
            try
            {
                // Verificando se o usuário está logado antes de prosseguir com o login
                var usuarioLogado = await _usuarioLogado.Usuario();

                if (usuarioLogado == null)
                {
                    // Retorna uma resposta com o erro caso o usuário não esteja logado
                    return Unauthorized(new RespostaErroJson(ResourceMessagesExceptions.USUARIO_NAO_LOGADO));
                }


                var response = await useCase.Execute(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Caso haja algum erro, retornamos um erro genérico
                return BadRequest(new RespostaErroJson("Erro ao processar login: " + ex.Message));
            }
        }

    }
}
