using MinhaAgendaDeConsultas.Domain.Seguranca.Token;

namespace MinhaAgendaDeConsultas.Infraestrutura.Seguranca.Token.RefreshToken;
public class RefreshTokenGenerator: IRefreshTokenGenerator
{
    public string GerarToken() => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    
}
