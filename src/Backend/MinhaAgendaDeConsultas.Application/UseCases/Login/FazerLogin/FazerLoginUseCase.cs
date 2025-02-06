using AutoMapper;
using Microsoft.AspNetCore.Http;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Login;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Repositorios;
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
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

        public FazerLoginUseCase(IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio, IPasswordEncripter passwordEncripter, IGeradorTokenAcesso geradorTokenAcesso,
            IHttpContextAccessor httpContextAccessor, IMapper mapper, IRefreshTokenGenerator refreshTokenGenerato, ITokenRepository tokenRepository, IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
            _passwordEncripter = passwordEncripter;
            _geradorTokenAcesso = geradorTokenAcesso;
            this.httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _refreshTokenGenerator = refreshTokenGenerato;
            _tokenRepository = tokenRepository;
            _unidadeDeTrabalho = unidadeDeTrabalho;
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

            var refreshToken = await CriarESalvarRefreshToken(entidadeUsuario);

            Guid identificadorGuid = Guid.NewGuid();

            return new ResponseRegistrarUsuarioJson
            {
                Nome = entidadeUsuario!.Nome,
                Tokens = new RespostaTokenJson
                {
                    AcessoToken = _geradorTokenAcesso.Gerar(identificadorGuid.ToString(), entidadeUsuario.Email),
                    RefreshToken = refreshToken
                }
            };
        }

        public async Task<string> CriarESalvarRefreshToken(Domain.Entidades.Usuario usuario)
        {
            var refreshToken = new Domain.Entidades.RefreshToken
            {
                UsuarioId = usuario.Id, // Define o usuário associado ao refresh token
                Value = _refreshTokenGenerator.GerarToken(), // Gera o valor do refresh token
                CriadoEm = DateTime.UtcNow, // Define a data de criação do refresh token
                DataExpiracao = DateTime.UtcNow.AddMonths(6) // Define a data de expiração do refresh token
            };

            await _tokenRepository.SalvarNovoRefreshToken(refreshToken);
            await _unidadeDeTrabalho.Commit();

            return refreshToken.Value; // Retorna o valor gerado para o refresh token
        }
    }
}
