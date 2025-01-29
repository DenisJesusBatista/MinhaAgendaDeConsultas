namespace MinhaAgendaDeConsultas.Domain.Repositorios
{
    public interface IUsuarioUpdateOnlyRepositorio
    {
        Task<Entidades.Usuario> RecuperarPorEmail(string email);

        // Torne o método Update assíncrono
        Task Update(Entidades.Usuario usuario);  // Atualização para usar Task


    }
}
