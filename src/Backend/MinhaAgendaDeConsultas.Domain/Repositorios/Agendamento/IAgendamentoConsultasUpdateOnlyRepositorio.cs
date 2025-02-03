namespace MinhaAgendaDeConsultas.Domain.Repositorios
{
    public interface IAgendamentoConsultasUpdateOnlyRepositorio
    {
        public Task Update(Entidades.AgendamentoConsultas agendamentoConsulta);
    }
}
