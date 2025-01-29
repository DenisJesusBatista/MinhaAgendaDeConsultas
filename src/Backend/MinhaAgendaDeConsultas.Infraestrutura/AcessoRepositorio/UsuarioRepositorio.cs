using Microsoft.EntityFrameworkCore;
using MinhaAgendaDeConsultas.Domain.Entidades;
using MinhaAgendaDeConsultas.Domain.Repositorios;

namespace MinhaAgendaDeConsultas.Infraestrutura.AcessoRepositorio
{
    public class UsuarioRepositorio : IUsuarioWriteOnlyRepositorio, IUsuarioReadOnlyRepositorio, IUsuarioUpdateOnlyRepositorio
    {
        private readonly MinhaAgendaDeConsultasContext _contexto;

        public UsuarioRepositorio(MinhaAgendaDeConsultasContext contexto)
        {
            _contexto = contexto;
        }

        public async Task Adicionar(Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(usuario);
        }

        public async Task<Usuario?> RecuperarPorEmail(string email)
        {
            return await _contexto.Usuarios
                .Include(c => c.Email)
                .FirstOrDefaultAsync(c => c.Email.Equals(email));
        }

        public async Task<IEnumerable<Usuario>> RecuperarPorId(int id)
        {
            return await _contexto.Usuarios
                .Where(x => x.Id == id)
                .ToListAsync();
        }

        public async Task<bool> ExisteUsuarioComEmail(string email)
        {
            return await _contexto.Usuarios.AnyAsync(c => c.Email.Equals(email));
        }

        public void Update(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
        }

        async Task IUsuarioWriteOnlyRepositorio.Update(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
        }

    }
}
