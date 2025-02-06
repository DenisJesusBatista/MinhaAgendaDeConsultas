namespace MinhaAgendaDeConsultas.Domain.Repositorios.Agendamento
{
    public  interface IAgendaMedicaDeleteOnlyRepository
    {
        public Task Delete(long id);
    }
}
