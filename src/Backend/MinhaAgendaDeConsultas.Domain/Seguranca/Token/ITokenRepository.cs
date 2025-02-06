using MinhaAgendaDeConsultas.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Domain.Seguranca.Token;
public interface ITokenRepository
{
    Task<RefreshToken?> ObterToken(string refreshToken);
    Task SalvarNovoRefreshToken(RefreshToken refreshToken);
}
