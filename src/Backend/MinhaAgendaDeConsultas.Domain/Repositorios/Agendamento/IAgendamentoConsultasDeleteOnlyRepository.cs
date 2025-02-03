namespace MinhaAgendaDeConsultas.Domain.Repositorios
{
    public interface IAgendamentoConsultasDeleteOnlyRepository
    {
        public Task Delete(long id);
    }
}
