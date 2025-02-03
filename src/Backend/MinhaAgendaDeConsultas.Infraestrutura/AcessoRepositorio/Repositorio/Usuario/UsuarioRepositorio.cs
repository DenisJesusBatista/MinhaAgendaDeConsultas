using Microsoft.EntityFrameworkCore;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Entidades;
using MinhaAgendaDeConsultas.Domain.Repositorios.Usuario;

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

       

        public async Task<IEnumerable<Usuario>> RecuperarPorId(int id)
        {
            return await _contexto.Usuarios
                .Where(x => x.Id == id)
                .ToListAsync();
        }



        public async Task Update(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
            await _contexto.SaveChangesAsync();  // Não se esqueça de chamar o SaveChangesAsync para persistir a alteração.
        }

        async Task IUsuarioWriteOnlyRepositorio.Update(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
        }

        public async Task<bool> ExisteUsuarioComEmaileSenha(string email, string senha)
        {
            return await _contexto.Usuarios.AnyAsync(user => user.Email == email && user.Senha == senha);
        }


        public async Task<Usuario?> RecuperarUsuarioPorIdentificador(Guid usuarioIdentificador)
        {
            return await _contexto
                .Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Identificador.Equals(usuarioIdentificador) && user.Ativo == true);
        }

        public async Task<Usuario?> RecuperarPorEmail(string email)
        {
            IQueryable<Usuario> query = _contexto.Usuarios
                //.Include(c => c.Email)
                .AsNoTracking();
                return await query.FirstOrDefaultAsync(c => c.Email.Equals(email));
        }


        public async Task<Usuario?> RecuperarUsuarioPorEmaileSenha(string email, string senha)
        {
            return await _contexto
                .Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Senha.Equals(senha) && user.Ativo == true);            
        }
      

        public async Task<bool> ExisteUsuarioComEmail(string email) => await _contexto.Usuarios.AnyAsync(usuario => usuario.Email.Equals(email));
        public async Task<bool> ExisteUsarioAtivoComIdentificador(Guid usuarioIdentificador) => await _contexto.Usuarios.AnyAsync(usuario => usuario.Identificador.Equals(usuarioIdentificador));
        
    }
}
