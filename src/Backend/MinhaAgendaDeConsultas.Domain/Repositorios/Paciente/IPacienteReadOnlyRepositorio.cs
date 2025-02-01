namespace MinhaAgendaDeConsultas.Domain.Repositorios.Paciente
{
    public interface IPacienteReadOnlyRepositorio
    {
        Task<bool> ExistePacienteComCpf(string cpf);
        Task<Entidades.Paciente> RecuperarPorId(int id);
        Task<bool> ExistePacienteUsuarioComEmail(string email);
        Task<Entidades.Paciente> RecuperarPorCpf(string cpf);

    }
}
