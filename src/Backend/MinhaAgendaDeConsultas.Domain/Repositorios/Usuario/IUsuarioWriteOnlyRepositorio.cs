namespace MinhaAgendaDeConsultas.Domain.Repositorios.Usuario
{
    public interface IUsuarioWriteOnlyRepositorio
    {
        /*Write: Escrita*/
        /*Task: Adicição assicrona 
         Await: Para chamar
         */
        Task Adicionar(Entidades.Usuario usuario);

        Task Update(Entidades.Usuario usuario);

    }

}
