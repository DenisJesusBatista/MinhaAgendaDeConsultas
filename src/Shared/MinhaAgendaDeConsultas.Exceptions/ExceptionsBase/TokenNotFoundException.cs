using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Exceptions.ExceptionsBase
{
    public class TokenNotFoundException : Exception
    {
        public TokenNotFoundException(string message) : base(message) { }
    }

    public class TokenExpiredException : Exception
    {
        public TokenExpiredException(string message) : base(message) { }
    }

}
