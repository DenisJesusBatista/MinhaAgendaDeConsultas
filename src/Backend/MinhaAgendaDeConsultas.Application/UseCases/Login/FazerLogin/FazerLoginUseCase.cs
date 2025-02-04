﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Login;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Seguranca.Criptografia;
using MinhaAgendaDeConsultas.Domain.Seguranca.Token;
using RespostaTokenJson = MinhaAgendaDeConsultas.Communication.Resposta.Usuario.RespostaTokenJson;

namespace MinhaAgendaDeConsultas.Application.UseCases.Login.FazerLogin
{
    public class FazerLoginUseCase : IFazerLoginUseCase
    {
        private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IGeradorTokenAcesso _geradorTokenAcesso;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper _mapper;

        public FazerLoginUseCase(IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio, IPasswordEncripter passwordEncripter, IGeradorTokenAcesso geradorTokenAcesso, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
            _passwordEncripter = passwordEncripter;
            _geradorTokenAcesso = geradorTokenAcesso;
            this.httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public FazerLoginUseCase(IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio, IPasswordEncripter passwordEncripter)
        {
            _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
            _passwordEncripter = passwordEncripter;
        }

        public async Task<ResponseRegistrarUsuarioJson> ObterUsarioLogado()
        {
            var user = httpContextAccessor.HttpContext.User;
            
            if (user.Identity is { IsAuthenticated: false })
            {
                return null;
            }

            return null;
            

        }

        public async Task<ResponseRegistrarUsuarioJson> Execute(RequisicaoLoginJson request)
        {
            var encriptedPassword = _passwordEncripter.Encrypt(request.Senha);

            var existeUsuario = await _usuarioReadOnlyRepositorio.ExisteUsuarioComEmaileSenha(request.Email, encriptedPassword);
                 

            var entidadeUsuario = await _usuarioReadOnlyRepositorio.RecuperarPorEmail(request.Email);

            Guid identificadorGuid = Guid.NewGuid();

            return new ResponseRegistrarUsuarioJson
            {
                Nome = entidadeUsuario!.Nome,
                Tokens = new RespostaTokenJson
                {
                    AcessoToken = _geradorTokenAcesso.Gerar(identificadorGuid.ToString(), entidadeUsuario.Email),
                }
            };
        }
    }
}
