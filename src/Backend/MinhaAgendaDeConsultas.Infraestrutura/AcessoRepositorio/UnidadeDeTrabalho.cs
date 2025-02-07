using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MinhaAgendaDeConsultas.Communication.Resposta;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using Npgsql;

namespace MinhaAgendaDeConsultas.Infraestrutura.AcessoRepositorio
{
    [ExcludeFromCodeCoverage]
    public sealed class UnidadeDeTrabalho : IDisposable, IUnidadeDeTrabalho
    {
        //Classe que libera a memoria
        private readonly MinhaAgendaDeConsultasContext _contexto;
        private bool _disposed;

        public UnidadeDeTrabalho(MinhaAgendaDeConsultasContext contexto)
        {
            _contexto = contexto;

        }

        public async Task BeginTransaction()
        {
            await _contexto.Database.BeginTransactionAsync();
        }

        public async Task LockTableAsync(String tableName)
        {
            await _contexto.Database.ExecuteSqlRawAsync($"LOCK TABLE public.\"{tableName.Trim()}\" IN EXCLUSIVE MODE;");
        }

        public async Task Commit()
        {
            await _contexto.SaveChangesAsync();
        }

        public async Task CommitTransaction()
        {
            try
            {
                await _contexto.Database.CommitTransactionAsync();
            }
            catch
            {
                await _contexto.Database.RollbackTransactionAsync();
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                //Faz a liberacao de memoria
                _contexto.Dispose();
            }
            _disposed = true;

        }

        public async Task RollbackTransaction()
        {
            await _contexto.Database.RollbackTransactionAsync();
        }

        public async Task<bool> TableIsLocked(string tableName)
        {
            var sql = @"
                            SELECT                                
                                pg_locks.granted
                            FROM 
                                pg_locks
                            JOIN 
                                pg_class ON pg_locks.relation = pg_class.oid
                            WHERE 
                                pg_class.relname = @tableName and pg_locks.granted = false;";

            var parameters = new[] { new NpgsqlParameter("@tableName", tableName) };
            var locks = await _contexto.Database.ExecuteSqlRawAsync(sql, parameters);

            return locks > 1;
        }
    }
}
