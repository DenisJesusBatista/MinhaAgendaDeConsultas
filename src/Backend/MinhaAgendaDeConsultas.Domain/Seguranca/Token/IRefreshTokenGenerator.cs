using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Domain.Seguranca.Token;
public interface IRefreshTokenGenerator
{
    public string GerarToken();
}
