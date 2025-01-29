using MinhaAgendaDeConsultas.Domain.Entidades;

namespace MinhaAgendaDeConsultas.Domain.Repositorios
{
    public interface IUsuarioReadOnlyRepositorio
    {
        Task<bool> ExisteUsuarioComEmail(string email);
        Task<IEnumerable<Usuario>> RecuperarPorId(int id);
        Task<Entidades.Usuario> RecuperarPorEmail(string email);
    }
}
