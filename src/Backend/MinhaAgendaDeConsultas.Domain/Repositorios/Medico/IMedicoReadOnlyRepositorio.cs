using MinhaAgendaDeConsultas.Domain.Enumeradores;

namespace MinhaAgendaDeConsultas.Domain.Repositorios.Medico
{
    public interface IMedicoReadOnlyRepositorio
    {
        Task<bool> ExisteMedicoComCpf(string cpf);
        Task<bool> ExisteMedicoComCrm(string cpf);        
        Task<bool> ExisteMedicoUsuarioComEmail(string email);
        Task<Entidades.Medico> RecuperarPorId(int id);
        Task<Entidades.Medico> RecuperarPorCpf(string cpf);
        Task<Entidades.Medico> RecuperarPorCrm(string crm);
    }
}
