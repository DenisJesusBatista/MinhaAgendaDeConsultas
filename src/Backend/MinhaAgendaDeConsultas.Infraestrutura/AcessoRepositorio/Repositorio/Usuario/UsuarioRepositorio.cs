using Microsoft.EntityFrameworkCore;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Repositorios.Usuario;


namespace MinhaAgendaDeConsultas.Infraestrutura.AcessoRepositorio.Repositorio.Usuario
{
    public class UsuarioRepositorio : IUsuarioWriteOnlyRepositorio, IUsuarioReadOnlyRepositorio, IUsuarioUpdateOnlyRepositorio
    {
        private readonly MinhaAgendaDeConsultasContext _contexto;

        public UsuarioRepositorio(MinhaAgendaDeConsultasContext contexto)
        {
            _contexto = contexto;
        }

        public async Task Adicionar(Domain.Entidades.Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(usuario);
        }



        public async Task<IEnumerable<Domain.Entidades.Usuario>> RecuperarPorId(int id)
        {
            return await _contexto.Usuarios
                .Where(x => x.Id == id)
                .ToListAsync();
        }



        public async Task Update(Domain.Entidades.Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
            await _contexto.SaveChangesAsync();  // Não se esqueça de chamar o SaveChangesAsync para persistir a alteração.
        }

        async Task IUsuarioWriteOnlyRepositorio.Update(Domain.Entidades.Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
        }

        public async Task<bool> ExisteUsuarioComEmaileSenha(string email, string senha)
        {
            return await _contexto.Usuarios.AnyAsync(user => user.Email == email && user.Senha == senha);
        }


        public async Task<Domain.Entidades.Usuario?> RecuperarUsuarioPorIdentificador(Guid usuarioIdentificador)
        {
            return await _contexto
                .Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Identificador.Equals(usuarioIdentificador) && user.Ativo == true);
        }

        public async Task<Domain.Entidades.Usuario?> RecuperarPorEmail(string email)
        {
            IQueryable<Domain.Entidades.Usuario> query = _contexto.Usuarios.AsNoTracking();
            query = query.Where(c => c.Email == email);

            return await query.FirstOrDefaultAsync();
        }


        public async Task<Domain.Entidades.Usuario?> RecuperarUsuarioPorEmaileSenha(string email, string senha)
        {
            return await _contexto
                .Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Senha.Equals(senha) && user.Ativo == true);
        }


        public async Task<bool> ExisteUsuarioComEmail(string email) => await _contexto.Usuarios.AnyAsync(usuario => usuario.Email.Equals(email));
        public async Task<bool> ExisteUsarioAtivoComIdentificador(Guid usuarioIdentificador) => await _contexto.Usuarios.AnyAsync(usuario => usuario.Identificador.Equals(usuarioIdentificador));

        public async Task<IEnumerable<Domain.Entidades.Medico?>> RecuperarPorEspecialidade(string especialidade)
        {
            IQueryable<Domain.Entidades.Medico> query = _contexto.Medicos.AsNoTracking();
            query = query.Where(c => c.Especialidade == especialidade);

            return await query.ToListAsync();
        }
    }
}
