using System.Text;
using Microsoft.IdentityModel.Tokens;

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
