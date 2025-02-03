using Microsoft.EntityFrameworkCore;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using System.Diagnostics.CodeAnalysis;

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

        public async Task LockTableAsync<T>()
        {
            await _contexto.Database.ExecuteSqlRawAsync($"SELECT 1 FROM {nameof(T)} WITH (TABLOCKX)");
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
    }
}
