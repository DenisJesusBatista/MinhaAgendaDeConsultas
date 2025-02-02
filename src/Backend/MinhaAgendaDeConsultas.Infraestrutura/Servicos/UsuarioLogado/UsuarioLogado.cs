using Microsoft.EntityFrameworkCore;
using MinhaAgendaDeConsultas.Domain.Entidades;
using MinhaAgendaDeConsultas.Domain.Seguranca.Token;
using MinhaAgendaDeConsultas.Domain.Servicos.UsuarioLogado;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MinhaAgendaDeConsultas.Infraestrutura.Servicos.UsuarioLogado
{
    public class UsuarioLogado: IUsuarioLogado  
    {
        private readonly MinhaAgendaDeConsultasContext _context;
        private readonly ITokenProvider _tokenProvider;

        public UsuarioLogado(MinhaAgendaDeConsultasContext context, ITokenProvider tokenProvider)
        {
            _context = context;
            _tokenProvider = tokenProvider;
        }

        public async Task<Usuario> Usuario()
        {
            var token = _tokenProvider.Value();
            
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            //var identificador = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            //var identificador = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);

            var identificador = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

            // Obtém o e-mail do token JWT
            var email = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;



            var usuarioIdentificador = Guid.Parse(identificador);

            var identificadorConvertido = usuarioIdentificador.ToString();
            //var usuarioIdentificador = Guid.Parse(identificador.Value);


            return await _context
                .Usuarios
                .AsNoTracking() 
                .FirstAsync(user => user.Email == email);
            
        }   
    }
}
