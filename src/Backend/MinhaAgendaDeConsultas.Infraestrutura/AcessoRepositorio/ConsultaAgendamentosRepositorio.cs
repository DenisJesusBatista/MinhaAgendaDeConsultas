using Microsoft.EntityFrameworkCore;
using MinhaAgendaDeConsultas.Domain.Entidades;
using MinhaAgendaDeConsultas.Domain.Repositorios;

namespace MinhaAgendaDeConsultas.Infraestrutura.AcessoRepositorio
{
    public class ConsultaAgendamentosRepositorio : IAgendamentoConsultasConsultasOnlyRepositorio,
        IAgendamentoConsultasWriteOnlyRepositorio,
        IAgendamentoConsultasDeleteOnlyRepository,
        IAgendamentoConsultasUpdateOnlyRepositorio
    {
        private readonly MinhaAgendaDeConsultasContext _contexto;

        public ConsultaAgendamentosRepositorio(MinhaAgendaDeConsultasContext contexto)
        {
            this._contexto = contexto;
        }
        public async Task Add(AgendamentoConsultas agendamentoConsulta)
        {
            
            await _contexto.AgendamentoConsultas.AddAsync(agendamentoConsulta);
        }

        public async Task Delete(long id)
        {
            await _contexto.AgendamentoConsultas.Where(x => x.Id == id).ExecuteDeleteAsync();
            
        }
        public async Task Update(AgendamentoConsultas agendamentoConsulta)
        {
            _contexto.AgendamentoConsultas.Update(agendamentoConsulta);
        }

        public async Task<bool> GetDisponibilides(DateTime DataDeInicio,DateTime DataFim, int? medicoId = null)
        {
            //Verifica se o horário está ocupado médico desejado 

            IQueryable<AgendamentoConsultas> query = _contexto.AgendamentoConsultas;

            //Verifica se o horário está ocupado para o médico desejado
            query = query.Where(h=> DataDeInicio >= h.DataHoraInicio && DataDeInicio <= h.DataHoraFim);
            query = query.Where(h=> DataFim >= h.DataHoraInicio && DataFim <= h.DataHoraFim);
            query = query.Where(h => h.MedicoId == medicoId);


            return await query.CountAsync()>0;
        }

        public async Task<IList<AgendamentoConsultas>> GetAgendamentosMedico(int medicoId)
        {
            IQueryable<AgendamentoConsultas> query = _contexto.AgendamentoConsultas;
            query = query.Where(h => h.MedicoId == medicoId);
            return await query.ToListAsync();
        }

        public async Task<IList<AgendamentoConsultas>> GetAgendamentosPaciente(int pacienteId)
        {
            IQueryable<AgendamentoConsultas> query = _contexto.AgendamentoConsultas;
            query = query.Where(h => h.MedicoId == pacienteId);
            return await query.ToListAsync();
        }
    }
}
