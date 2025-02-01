namespace MinhaAgendaDeConsultas.Domain.Repositorios.Medico
{
    public interface IMedicoWriteOnlyRepositorio
    {
        /*Write: Escrita*/
        /*Task: Adicição assicrona 
         Await: Para chamar
         */
        Task Adicionar(Entidades.Medico medico);

        Task Update(Entidades.Medico medico);
    }
}
