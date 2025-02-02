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
