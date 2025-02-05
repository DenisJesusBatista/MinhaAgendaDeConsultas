using Microsoft.EntityFrameworkCore;
using MinhaAgendaDeConsultas.Domain.Entidades;
using MinhaAgendaDeConsultas.Domain.Repositorios.Agendamento;

namespace MinhaAgendaDeConsultas.Infraestrutura.AcessoRepositorio.Repositorio.Agendamentos
{
    public class AgendaMedicaRepositorio : IAgendaMedicaConsultaOnlyRepository,
        IAgendaMedicaWriteOnlyRepository,
        IAgendaMedicaUpdateOnlyRepository,
        IAgendaMedicaDeleteOnlyRepository
    {
        private readonly MinhaAgendaDeConsultasContext _contexto;

        public AgendaMedicaRepositorio(MinhaAgendaDeConsultasContext contexto)
        {
            this._contexto = contexto;
        }
        public async Task Delete(long id)
        {
            await _contexto.AgendaMedica.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task Add(AgendaMedica agenda)
        {
            await _contexto.AgendaMedica.AddAsync(agenda);
        }

        public async Task Update(AgendaMedica agenda)
        {
            _contexto.AgendaMedica.Update(agenda);
        }

        public async Task<bool> VerificarDisponibilidade(long? MedicoId, DateTime DataInicio, DateTime DataFim)
        {
            return await _contexto.AgendaMedica
                .Where(x => x.MedicoId == MedicoId)
                .Where(x => DataInicio >= x.DataInicio && DataFim <= x.DataFim)
                .AnyAsync();
        }

        public async Task<IList<AgendaMedica>> ObterAgendasMedicias(long? MedicoId, DateTime DataInicio, DateTime DataFim)
        {
            var query = _contexto.AgendaMedica
                .Where(x => x.MedicoId == MedicoId);
            var agendas = await query.ToListAsync();
            agendas = agendas.Where(x => x.DataInicio >= DataInicio && x.DataFim <= DataFim).ToList();
            return agendas;
        }


    }
}
