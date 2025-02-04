﻿using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Application.UseCases.Login.FazerLogin;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Login;
using MinhaAgendaDeConsultas.Communication.Responses;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;
using MinhaAgendaDeConsultas.Domain.Servicos.UsuarioLogado;
using MinhaAgendaDeConsultas.Infraestrutura.Servicos.UsuarioLogado;

namespace MinhaAgendaDeConsultas.Api.Controllers
{
    public class LoginController : MinhaAgendaDeConsultasBaseController
    {
<<<<<<< HEAD
        private readonly IUsuarioLogado _usuarioLogado;

        public LoginController(IUsuarioLogado usuarioLogado)
        {
            _usuarioLogado = usuarioLogado;

        }


        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegistrarUsuarioJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespostaErroJson), StatusCodes.Status401Unauthorized)]
        //[Authorize]
=======
>>>>>>> b0247459925ac07848d86133cf9306b94afd16c3

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
            _usuarioLogado.Usuario();


            var response = await useCase.Execute(request);
            return Ok(response);
        }
    }
}
