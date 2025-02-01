using Microsoft.EntityFrameworkCore;
using MinhaAgendaDeConsultas.Domain.Enumeradores;
using MinhaAgendaDeConsultas.Domain.Repositorios.Paciente;

namespace MinhaAgendaDeConsultas.Infraestrutura.AcessoRepositorio.Repositorio.Paciente
{
    public class PacienteRepositorio : IPacienteWriteOnlyRepositorio, IPacienteReadOnlyRepositorio, IPacienteUpdateOnlyRepositorio
    {
        private readonly MinhaAgendaDeConsultasContext _contexto;

        public PacienteRepositorio(MinhaAgendaDeConsultasContext contexto)
        {
            _contexto = contexto;
        }

        public async Task Adicionar(Domain.Entidades.Paciente paciente)
        {
            await _contexto.Pacientes.AddAsync(paciente);
        }

        public async Task<bool> ExistePacienteComCpf(string cpf)
        {
            return await _contexto.Pacientes
                .Where(c => c.Tipo == TipoUsuario.Paciente)
                .AnyAsync(c => c.Cpf.Equals(cpf));
        }

        public async Task<bool> ExistePacienteUsuarioComEmail(string email)
        {
            return await _contexto.Usuarios
                .Where(u => u.Tipo == TipoUsuario.Medico && u.Email == email)
                .AnyAsync();
        }

        public async Task<Domain.Entidades.Paciente> RecuperarPorCpf(string cpf)
        {
            return await _contexto.Pacientes
                .OfType<Domain.Entidades.Paciente>()  // Especifica que estamos buscando médicos
                .FirstOrDefaultAsync(c => c.Cpf.Equals(cpf) && c.Tipo == TipoUsuario.Paciente);
        }

        public async Task<Domain.Entidades.Paciente> RecuperarPorEmail(string email)
        {
            return await _contexto.Pacientes
                .OfType<Domain.Entidades.Paciente>()  // Especifica que estamos buscando médicos
                .FirstOrDefaultAsync(c => c.Email.Equals(email) && c.Tipo == TipoUsuario.Paciente);
        }

        public async Task<Domain.Entidades.Paciente> RecuperarPorId(int id)
        {
            return await _contexto.Pacientes
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task Update(Domain.Entidades.Paciente paciente)
        {
            _contexto.Pacientes.Update(paciente);
            await _contexto.SaveChangesAsync();  // Persistindo as alterações no banco
        }
    }
}
