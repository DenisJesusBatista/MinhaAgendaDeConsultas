namespace MinhaAgendaDeConsultas.Domain.Repositorios.Paciente
{
    public interface IPacienteUpdateOnlyRepositorio
    {
        Task<Entidades.Paciente> RecuperarPorEmail(string email);
        Task<Entidades.Paciente> RecuperarPorCpf(string cpf);

        // Torne o método Update assíncrono
        Task Update(Entidades.Paciente paciente);  // Atualização para usar Task
    }
}
