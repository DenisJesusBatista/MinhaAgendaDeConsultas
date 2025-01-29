using MinhaAgendaDeConsultas.Application.Services.Criptografia;
using MinhaAgendaDeConsultas.Communication.Requisicoes;
using MinhaAgendaDeConsultas.Communication.Responses;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Application.UseCases.Login.DoLogin
{
    public class FazerLoginUseCase : IFazerLoginUseCase
    {
        private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;
        private readonly PasswordEncripter _passwordEncripter;

        public FazerLoginUseCase(IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio, PasswordEncripter passwordEncripter)
        {
            _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
            _passwordEncripter = passwordEncripter;
        }


        public async Task<ResponseRegistrarUsuarioJson> Execute(RequisicaoLoginJson request)
        {
            var encriptedPassword = _passwordEncripter.Encrypt(request.Senha);

            var user = await _usuarioReadOnlyRepositorio.RecuperarUsuarioPorEmaileSenha(request.Email, encriptedPassword) ?? throw new LoginInvalidoException();           

            return new ResponseRegistrarUsuarioJson
            {
                Nome = user?.Nome
            };
        }
    }
}
