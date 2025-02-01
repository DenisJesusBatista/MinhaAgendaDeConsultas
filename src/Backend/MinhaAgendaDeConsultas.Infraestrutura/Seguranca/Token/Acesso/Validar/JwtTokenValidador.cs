using Microsoft.IdentityModel.Tokens;
using MinhaAgendaDeConsultas.Domain.Seguranca.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MinhaAgendaDeConsultas.Infraestrutura.Seguranca.Token.Acesso.Validar
{
    public class JwtTokenValidador : JwtTokenHandler, IValidadorTokenAcesso
    {
        private readonly string _chaveAssinatura;

        public JwtTokenValidador(string chaveAssinatura)
        {
            _chaveAssinatura = chaveAssinatura;
        }
        public Guid ValidarEObterIdentificadorUsario(string token)
        {
            var validacaoParametro = new TokenValidationParameters
            {
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = GetSymmetricSecurityKey(_chaveAssinatura),
                ValidateIssuer = false,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, validacaoParametro, out _);

            var identificadorUsuario = principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return Guid.Parse(identificadorUsuario);
        }
    }
}
