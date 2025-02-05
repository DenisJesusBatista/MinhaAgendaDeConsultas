using MinhaAgendaDeConsultas.Domain.Entidades;

namespace MinhaAgendaDeConsultas.Domain.Repositorios.Agendamento
{
    public interface IAgendaMedicaUpdateOnlyRepository
    {
        public Task Update(AgendaMedica agenda);
    }
}
