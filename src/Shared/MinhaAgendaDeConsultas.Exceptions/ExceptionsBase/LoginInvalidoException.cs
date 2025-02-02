namespace MinhaAgendaDeConsultas.Exceptions.ExceptionsBase
{
    public class LoginInvalidoException : MinhaAgendaDeContatosExceptions
    {
        public LoginInvalidoException() : base(ResourceMessagesExceptions.EMAIL_E_OU_SENHA_INVALIDA)
        {
        }
    }
}
