using Microsoft.EntityFrameworkCore;
using MinhaAgendaDeConsultas.Domain.Entidades;
using MinhaAgendaDeConsultas.Domain.Seguranca.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Infraestrutura.AcessoRepositorio.Repositorio.Token;
public class TokenRepository: ITokenRepository
{
    private readonly MinhaAgendaDeConsultasContext _context;

    public TokenRepository(MinhaAgendaDeConsultasContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> ObterToken(string refreshToken)
    {
        return await _context
            .RefreshToken
            .AsNoTracking()
            .Include(token => token.Usuario)
            .FirstOrDefaultAsync(token => token.Value.Equals(refreshToken));
    }

    public async Task SalvarNovoRefreshToken(RefreshToken refreshToken)
    {
        var tokens = _context.RefreshToken.Where(t => t.UsuarioId == refreshToken.UsuarioId);
        _context.RefreshToken.RemoveRange(tokens);
        await _context.RefreshToken.AddAsync(refreshToken);
    }
}
