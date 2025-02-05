using MinhaAgendaDeConsultas.Domain.Entidades;

namespace MinhaAgendaDeConsultas.Domain.Repositorios.Agendamento
{
    public interface IAgendaMedicaWriteOnlyRepository
    {
        public Task Add(AgendaMedica);
    }
}
