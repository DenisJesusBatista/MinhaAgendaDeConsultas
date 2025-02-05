namespace MinhaAgendaDeConsultas.Domain.Seguranca.Criptografia;
public interface IPasswordEncripter
{
    public string Encrypt(string password);
}
