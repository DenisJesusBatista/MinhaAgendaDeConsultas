namespace MinhaAgendaDeConsultas.Domain.Repositorios.Paciente
{
    public interface IPacienteWriteOnlyRepositorio
    {
        /*Write: Escrita*/
        /*Task: Adicição assicrona 
         Await: Para chamar
         */
        Task Adicionar(Entidades.Paciente paciente);

        Task Update(Entidades.Paciente paciente);
    }
}
