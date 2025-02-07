using MinhaAgendaDeConsultas.Domain.Entidades;

namespace MinhaAgendaDeConsultas.Domain
{
    public interface IUsuarioReadOnlyRepositorio
    {
        Task<bool> ExisteUsuarioComEmail(string email);
        Task<IEnumerable<Usuario>> RecuperarPorId(int id);
        Task<Entidades.Usuario?> RecuperarPorEmail(string email);
        Task<Entidades.Usuario?> RecuperarUsuarioPorEmaileSenha(string email, string senha);

        Task<bool> ExisteUsuarioComEmaileSenha(string email, string senha);

        Task<bool> ExisteUsarioAtivoComIdentificador(Guid usuarioIdentificador);

        Task<Usuario> RecuperarUsuarioPorIdentificador(Guid usuarioIdentificador);

        Task<IEnumerable<Entidades.Medico?>> RecuperarPorEspecialidade(string especialidade);

    }
}
