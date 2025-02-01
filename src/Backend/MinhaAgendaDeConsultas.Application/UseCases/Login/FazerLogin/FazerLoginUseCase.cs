using MinhaAgendaDeConsultas.Application.Services.Criptografia;
using MinhaAgendaDeConsultas.Application.UseCases.Login.DoLogin;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Communication.Resposta.Token;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Seguranca.Token;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;

namespace MinhaAgendaDeConsultas.Application.UseCases.Login.FazerLogin
{
    public class FazerLoginUseCase : IFazerLoginUseCase
    {
        private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;
        private readonly PasswordEncripter _passwordEncripter;
        private readonly IGeradorTokenAcesso _geradorTokenAcesso;

        public FazerLoginUseCase(IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio, PasswordEncripter passwordEncripter, IGeradorTokenAcesso geradorTokenAcesso)
        {
            _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
            _passwordEncripter = passwordEncripter;
            _geradorTokenAcesso = geradorTokenAcesso;
        }

        public FazerLoginUseCase(IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio, PasswordEncripter passwordEncripter)
        {
            _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
            _passwordEncripter = passwordEncripter;
        }


        public async Task<ResponseRegistrarUsuarioJson> Execute(RequisicaoLoginJson request)
        {
            var encriptedPassword = _passwordEncripter.Encrypt(request.Senha);

            var entidade = await _usuarioReadOnlyRepositorio.RecuperarUsuarioPorEmaileSenha(request.Email, encriptedPassword) ?? throw new LoginInvalidoException();           

            return new ResponseRegistrarUsuarioJson
            {
                Nome = entidade.Nome,
                Tokens = new RespostaTokenJson
                {
                    AcessoToken = _geradorTokenAcesso.Gerar(entidade.Identificador),                    
                }
            };
        }
    }
}
