using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Exceptions.ExceptionsBase
{
    public class LoginInvalidoException : MinhaAgendaDeContatosExceptions
    {
        public LoginInvalidoException() : base(ResourceMessagesExceptions.EMAIL_E_OU_SENHA_INVALIDA)
        {
        }
    }
}
