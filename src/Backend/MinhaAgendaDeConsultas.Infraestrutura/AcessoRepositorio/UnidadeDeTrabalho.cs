using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MinhaAgendaDeConsultas.Domain.Repositorios;

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
    }
}
