namespace MinhaAgendaDeConsultas.Domain.Repositorios.Agendamento
{
    public interface IAgendaMedicaConsultaOnlyRepository
    {
        public Task<bool> VerificarDisponibilidade(long? MedicoId, DateTime DataInicio, DateTime DataFim);
        
    }
}
