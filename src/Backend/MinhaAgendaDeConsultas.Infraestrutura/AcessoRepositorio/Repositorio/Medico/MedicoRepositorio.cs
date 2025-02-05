using Microsoft.EntityFrameworkCore;
using MinhaAgendaDeConsultas.Domain.Enumeradores;
using MinhaAgendaDeConsultas.Domain.Repositorios.Medico;

namespace MinhaAgendaDeConsultas.Infraestrutura.AcessoRepositorio.Repositorio.Medico
{
    public class MedicoRepositorio : IMedicoWriteOnlyRepositorio, IMedicoReadOnlyRepositorio, IMedicoUpdateOnlyRepositorio
    {
        private readonly MinhaAgendaDeConsultasContext _contexto;

        public MedicoRepositorio(MinhaAgendaDeConsultasContext contexto)
        {
            _contexto = contexto;
        }

        public async Task Adicionar(Domain.Entidades.Medico medico)
        {
            await _contexto.Medicos.AddAsync(medico);
        }


        public async Task<bool> ExisteMedicoComCpf(string cpf) => await _contexto.Medicos.AnyAsync(medico => medico.Cpf.Equals(cpf));
        public async Task<bool> ExisteMedicoComCrm(string crm) => await _contexto.Medicos.AnyAsync(medico => medico.Crm.Equals(crm));

        public async Task<bool> ExisteMedicoUsuarioComEmail(string email) => await _contexto.Usuarios.AnyAsync(user => user.Email.Equals(email));


        public async Task<Domain.Entidades.Medico> RecuperarPorCpf(string cpf)
        {
            return await _contexto.Usuarios
                .OfType<Domain.Entidades.Medico>()  // Especifica que estamos buscando médicos
                .Where(c => c.Cpf.Equals(cpf) && c.Tipo == TipoUsuario.Medico) // Converte Tipo para int
                .FirstOrDefaultAsync();
        }



        public async Task<Domain.Entidades.Medico> RecuperarPorCrm(string crm)
        {
            return await _contexto.Usuarios
                .OfType<Domain.Entidades.Medico>()  // Especifica que estamos buscando médicos
                .FirstOrDefaultAsync(c => c.Crm.Equals(crm) && c.Tipo == TipoUsuario.Medico);
        }


        public async Task<Domain.Entidades.Medico> RecuperarPorEmail(string email)
        {
            return await _contexto.Usuarios
                .OfType<Domain.Entidades.Medico>()  // Especifica que estamos buscando médicos
                .FirstOrDefaultAsync(c => c.Email.Equals(email) && c.Tipo == TipoUsuario.Medico);
        }


        public async Task<Domain.Entidades.Medico> RecuperarPorId(int id)
        {
            return await _contexto.Medicos
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Update(Domain.Entidades.Medico medico)
        {
            _contexto.Medicos.Update(medico);
            await _contexto.SaveChangesAsync();  // Persistindo as alterações no banco
        }
    }
}
