using Microsoft.IdentityModel.Tokens;
using MinhaAgendaDeConsultas.Domain.Seguranca.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MinhaAgendaDeConsultas.Infraestrutura.Seguranca.Token.Acesso.Gerador
{
    public class JwtTokenGerador : IGeradorTokenAcesso
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
        public string Gerar(Guid identificadorUsuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, identificadorUsuario.ToString())
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
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha512Signature)
            };  

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        // Funcao para conversao da chave de assinatura em um objeto do tipo SymmetricSecurityKey   
        private SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            var bytes = Encoding.UTF8.GetBytes(_chaveAssinatura);
            return new SymmetricSecurityKey(bytes);
        }
        //private SymmetricSecurityKey GetSymmetricSecurityKey()
        //{
        //    // Usar uma chave mais longa ou derivar uma chave de 512 bits
        //    var chaveDerivada = Encoding.UTF8.GetBytes(_chaveAssinatura);

        //    // Se a chave for muito curta, podemos gerar um hash para garantir o tamanho adequado
        //    if (chaveDerivada.Length < 64)
        //    {
        //        // Gerar um hash SHA256 da chave para garantir 64 bytes (512 bits)
        //        chaveDerivada = SHA256.Create().ComputeHash(chaveDerivada);
        //    }

        //    return new SymmetricSecurityKey(chaveDerivada);
        //}
    }
}
