using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MinhaAgendaDeConsultas.Infraestrutura.Seguranca.Token.Acesso
{
    public abstract class JwtTokenHandler
    {
        // Funcao para conversao da chave de assinatura em um objeto do tipo SymmetricSecurityKey   
        protected SymmetricSecurityKey GetSymmetricSecurityKey(string chaveAssinatura)
        {
            var bytes = Encoding.UTF8.GetBytes(chaveAssinatura);
            return new SymmetricSecurityKey(bytes);
        }
    }
}
