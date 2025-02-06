using MinhaAgendaDeConsultas.Communication.Requisicoes.Token;
using MinhaAgendaDeConsultas.Communication.Resposta.Token;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain.Seguranca.Token;
using MinhaAgendaDeConsultas.Exceptions;

namespace MinhaAgendaDeConsultas.Application.UseCases.RefreshToken;
public class UseRefreshTokenUseCase : IUseRefreshTokenUseCase
{
    private readonly ITokenRepository _tokenRepository;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IGeradorTokenAcesso _geradorTokenAcesso;

    public UseRefreshTokenUseCase(ITokenRepository tokenRepository, IUnidadeDeTrabalho unidadeDeTrabalho, IRefreshTokenGenerator refreshTokenGenerator, IGeradorTokenAcesso geradorTokenAcesso)
    {
        _tokenRepository = tokenRepository;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _refreshTokenGenerator = refreshTokenGenerator;
        _geradorTokenAcesso = geradorTokenAcesso;
    }

    public Task<RespostaTokenJson> Executar(RequisicaoNovoTokenJson requisicao)
    {
        return ExecuteAsync(requisicao);
    }

    public async Task<RespostaTokenJson> ExecuteAsync(RequisicaoNovoTokenJson request)
    {
        // Recupera o refresh token do repositório
        var refreshToken = await _tokenRepository.ObterToken(request.RequestToken);

        // Se o refresh token for nulo, lança uma exceção
        if (refreshToken is null)
        {
            //t / Mensagem indicando que o token é inválido
            return new RespostaTokenJson
            {
                MensagemErro = ResourceMessagesExceptions.TOKEN_NAO_VALIDADO
            };
        }

        // Verifica se o refresh token ainda é válido (com base na data de criação)
        var refreshTokenValido = refreshToken.CriadoEm.AddDays(7); // Adiciona 7 dias à data de criação

        // Se o token expirou (comparação com a data atual), lança uma exceção
        if (DateTime.Compare(refreshTokenValido, DateTime.UtcNow) < 0)
        {
            //t / Mensagem indicando que o token é inválido
            return new RespostaTokenJson
            {
                MensagemErro = ResourceMessagesExceptions.TOKEN_EXPIRADO
            };
        }

        // Cria um novo refresh token
        var novoRefreshToken = new Domain.Entidades.RefreshToken
        {
            Value = _refreshTokenGenerator.GerarToken(), // Gera o novo token
            UsuarioId = refreshToken.UsuarioId, // Mantém o mesmo ID de usuário
            CriadoEm = DateTime.UtcNow, // Define a data de criação do novo token
            DataExpiracao = DateTime.UtcNow.AddMonths(6) // Define a data de expiração do novo token

        };

        // Salva o novo refresh token no repositório
        await _tokenRepository.SalvarNovoRefreshToken(novoRefreshToken);

        // Commit nas alterações no repositório
        await _unidadeDeTrabalho.Commit();

        //// Gera o novo token de acesso
        //var novoToken = _refreshTokenGenerator.GerarToken();

        // Retorna o resultado com os novos tokens
        return new RespostaTokenJson
        {
            AcessoToken = _geradorTokenAcesso.Gerar(refreshToken.Usuario.Identificador.ToString(), refreshToken.Usuario.Email),
            RefreshToken = novoRefreshToken.Value
        };
    }

}
