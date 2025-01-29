using Microsoft.EntityFrameworkCore;
using MinhaAgendaDeConsultas.Domain.Entidades;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Infraestrutura.AcessoRepositorio.Repositorio
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

        public async Task Update(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
            await _contexto.SaveChangesAsync();  // Persistindo as alterações no banco
        }

        async Task IUsuarioWriteOnlyRepositorio.Update(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
        }

        public async Task<Usuario?> RecuperarUsuarioPorEmaileSenha(string email, string senha)
        {
            return await _contexto.Usuarios
                .AsNoTracking()
                .Include(user => user.Email)
                .FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Senha.Equals(senha));  
        }
    }
}
