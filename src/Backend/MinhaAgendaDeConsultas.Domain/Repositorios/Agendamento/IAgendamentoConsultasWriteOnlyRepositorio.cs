namespace MinhaAgendaDeConsultas.Domain.Repositorios
{
    public interface IAgendamentoConsultasWriteOnlyRepositorio
    {
        public Task Add(Entidades.AgendamentoConsultas agendamentoConsulta);
    }
}
