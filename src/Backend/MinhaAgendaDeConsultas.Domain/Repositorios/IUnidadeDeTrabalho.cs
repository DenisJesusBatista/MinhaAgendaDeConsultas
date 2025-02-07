namespace MinhaAgendaDeConsultas.Domain.Repositorios
{
    public interface IUnidadeDeTrabalho
    {
        Task Commit();

        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
        Task LockTableAsync(String tableName);
        Task<bool> TableIsLocked(String tableName);
    }
}
