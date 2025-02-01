using Microsoft.AspNetCore.Http;
using MinhaAgendaDeConsultas.Application.Services.Criptografia;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Login;
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
        private readonly IHttpContextAccessor httpContextAccessor;

        public FazerLoginUseCase(IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio, PasswordEncripter passwordEncripter, IGeradorTokenAcesso geradorTokenAcesso, IHttpContextAccessor httpContextAccessor)
        {
            _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
            _passwordEncripter = passwordEncripter;
            _geradorTokenAcesso = geradorTokenAcesso;
            this.httpContextAccessor = httpContextAccessor;
        }

        public FazerLoginUseCase(IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio, PasswordEncripter passwordEncripter)
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

            //var userId = Convert.ToInt32(tokenService.GetIdentifierFromClaimsPrincipal(user));

        }

        public async Task<ResponseRegistrarUsuarioJson> Execute(RequisicaoLoginJson request)
        {
            var encriptedPassword = _passwordEncripter.Encrypt(request.Senha);
            
            //var cpf = "00000000000".ToString();

            var cpf = "2d3655d5-4b64-49c3-8f61-db1d2f9d16d8";

            var entidade = await _usuarioReadOnlyRepositorio.RecuperarUsuarioPorEmaileSenha(request.Email, encriptedPassword.ToString()) ?? throw new LoginInvalidoException();

            Guid identificadorGuid = Guid.NewGuid();

            entidade.Identificador = identificadorGuid;

            return new ResponseRegistrarUsuarioJson
            {
                Nome = entidade.Nome,
                Tokens = new RespostaTokenJson
                {
                    AcessoToken = _geradorTokenAcesso.Gerar(identificadorGuid.ToString()),                    
                }
            };
        }
    }
}
