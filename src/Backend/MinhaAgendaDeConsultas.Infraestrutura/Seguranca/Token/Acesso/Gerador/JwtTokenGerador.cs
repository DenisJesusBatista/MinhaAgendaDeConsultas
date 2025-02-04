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

        #region GerarTokenAntigo

        public string Gerar(string identificadorUsuario, string email)
        {
            // Definindo os claims que serão incluídos no token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, identificadorUsuario),  // Identificador do usuário
                new Claim(ClaimTypes.Email, email)  // Adicionando o e-mail do usuário
            };

            // Obtendo o tempo atual (em UTC)
            var agora = DateTime.UtcNow;

            // Definindo o tempo de expiração (pode ser configurado dinamicamente)
            var expires = agora.AddMinutes(_tempoExpiracaoMinutos);

            // Garantir que a expiração é após o "NotBefore"
            if (expires <= agora)
            {
                expires = agora.AddSeconds(10);  // Garante que a expiração seja posterior ao "NotBefore"
            }

            // Criando o SecurityTokenDescriptor com os dados necessários
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),  // Definindo o Subject com os Claims
                NotBefore = agora,  // O token não será válido antes de agora
                Expires = expires,  // O token expirará após o tempo definido
                SigningCredentials = new SigningCredentials(
                    GetSymmetricSecurityKey(_chaveAssinatura),  // Definindo a chave de assinatura
                    SecurityAlgorithms.HmacSha256Signature)  // Algoritmo de assinatura
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            // Criando o token JWT
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            // Retornando o token gerado como uma string
            return tokenHandler.WriteToken(securityToken);
        }

        #endregion       

    }
}
