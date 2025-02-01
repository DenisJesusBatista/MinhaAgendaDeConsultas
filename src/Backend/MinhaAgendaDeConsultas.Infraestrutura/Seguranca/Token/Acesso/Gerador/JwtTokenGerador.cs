using Microsoft.IdentityModel.Tokens;
using MinhaAgendaDeConsultas.Domain.Seguranca.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MinhaAgendaDeConsultas.Infraestrutura.Seguranca.Token.Acesso.Gerador
{
    public class JwtTokenGerador : JwtTokenHandler, IGeradorTokenAcesso
    {
        //Atributos da classe JwtTokenGerador
        private readonly uint _tempoExpiracaoMinutos;
        private readonly string _chaveAssinatura;

        // Construtor da classe JwtTokenGerador
        public JwtTokenGerador(uint tempoExpiracaoMinutos, string chaveAssinatura)
        {
            _tempoExpiracaoMinutos = tempoExpiracaoMinutos;
            _chaveAssinatura = chaveAssinatura;
        }

        // Funcao para gerar um token de acesso
        public string Gerar(string identificadorUsuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, identificadorUsuario)
            };

            // Definir o "NotBefore" como a hora atual, mas garantir que o "Expires" seja posterior
            var agora = DateTime.UtcNow;
            var expires = agora.AddMinutes(_tempoExpiracaoMinutos);

            // Definir uma pequena diferença entre NotBefore e Expires, se necessário
            if (expires <= agora)
            {
                expires = agora.AddSeconds(10);  // Garante que a expiração é após o "NotBefore"
            }


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = agora,  // Define explicitamente o "NotBefore" como o tempo atual
                Expires = expires,  // Define "Expires" como a data futura
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(_chaveAssinatura), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
