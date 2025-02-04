using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAgendaDeConsultas.Communication.Responses;
using MinhaAgendaDeConsultas.Domain.Entidades;
using MinhaAgendaDeConsultas.Domain.Seguranca.Token;
using MinhaAgendaDeConsultas.Domain.Servicos.UsuarioLogado;
using MinhaAgendaDeConsultas.Exceptions;
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
            try
            {
                var token = _tokenProvider.Value();  // Obtendo o token              

                var tokenHandler = new JwtSecurityTokenHandler();

                // Lendo o token JWT
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

                // Obtendo o identificador do usuário (caso precise)
                //var identificador = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


                var identificador = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

                // Obtendo o e-mail do token JWT
                var email = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    throw new UnauthorizedAccessException("E-mail não encontrado no token.");
                }

                // Verifica se o identificador foi obtido corretamente
                if (string.IsNullOrEmpty(identificador))
                {
                    throw new UnauthorizedAccessException("Identificador do usuário não encontrado no token.");
                }

                // Convertendo o identificador para Guid
                var usuarioIdentificador = Guid.Parse(identificador);

                // Buscando o usuário no banco de dados com base no e-mail
                return await _context
                    .Usuarios
                    .AsNoTracking()
                    .FirstOrDefaultAsync(user => user.Email == email && user.Ativo); // Verifica se o e-mail e o usuário estão ativos
            }
            catch (Exception ex)
            {

                throw new UnauthorizedAccessException(ResourceMessagesExceptions.TOKEN_NAO_VALIDADO);
            
            }
       

      
        }

    }
}
