namespace MinhaAgendaDeConsultas.Domain.Repositorios
{
    public interface IUnidadeDeTrabalho
    {
        Task Commit();

        Task BeginTransaction();
        Task CommitTransaction();
        Task LockTableAsync<T>();
    }
}
