using MinhaAgendaDeConsultas.Domain.Entidades;

namespace MinhaAgendaDeConsultas.Domain.Servicos.UsuarioLogado
{
    public interface IUsuarioLogado
    {
        public Task<Usuario> Usuario();
    }
}
