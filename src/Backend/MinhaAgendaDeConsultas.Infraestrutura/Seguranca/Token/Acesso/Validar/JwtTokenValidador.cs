using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MinhaAgendaDeConsultas.Domain.Seguranca.Token;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MinhaAgendaDeConsultas.Infraestrutura.Seguranca.Token.Acesso.Validar
{
    public class JwtTokenValidador : JwtTokenHandler, IValidadorTokenAcesso
    {
        private const string CLAIM_TYPE_ID = "Identificador";
        private const string CONFIG_KEY_SECRET = "SecretJwt";
        private const string CONFIG_KEY_CRYPTO = "ChaveCrypto";

        //private readonly IConfiguration configuration;
        //private readonly ICryptoService cryptoService;
        //private readonly ICacheService cacheService;

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

        public string GetIdentifierFromClaimsPrincipal(ClaimsPrincipal user)
        {
            //var chaveCriptografia = configuration.GetValue<string>(CONFIG_KEY_CRYPTO)!;

            var idEncryptado = user.FindFirst(CLAIM_TYPE_ID)!.Value;

            //var id = cryptoService.DescriptografarString(idEncryptado, chaveCriptografia);

            //return id;

            return idEncryptado;
        }
    }
}
