namespace MinhaAgendaDeConsultas.Domain.Seguranca.Token
{
    public interface IValidadorTokenAcesso
    {
        public Guid ValidarEObterIdentificadorUsario(string token);
    }
}
