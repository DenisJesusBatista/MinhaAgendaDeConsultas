namespace MinhaAgendaDeConsultas.Domain.Repositorios
{
    public interface IUsuarioUpdateOnlyRepositorio
    {
        Task<Entidades.Usuario> RecuperarPorEmail(string email);

        void Update(Entidades.Usuario usuario);
    }
}
