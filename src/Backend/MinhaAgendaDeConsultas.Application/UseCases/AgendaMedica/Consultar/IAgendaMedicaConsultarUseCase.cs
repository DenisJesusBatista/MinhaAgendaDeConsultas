namespace MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Consultar
{
    public interface IAgendaMedicaConsultarUseCase
    {
        public Task<bool> ConsultarDisponibilidade( DateTime DataInicio, DateTime DataFim ,string medicoEmail);
    }
}
