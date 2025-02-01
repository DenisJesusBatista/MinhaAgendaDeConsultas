namespace MinhaAgendaDeConsultas.Domain.Repositorios.Medico
{
    public interface IMedicoUpdateOnlyRepositorio
    {
        Task<Entidades.Medico> RecuperarPorEmail(string email);
        Task<Entidades.Medico> RecuperarPorCpf(string cpf);
        Task<Entidades.Medico> RecuperarPorCrm(string crm);

        // Torne o método Update assíncrono
        Task Update(Entidades.Medico medico);  // Atualização para usar Task
    }
}
