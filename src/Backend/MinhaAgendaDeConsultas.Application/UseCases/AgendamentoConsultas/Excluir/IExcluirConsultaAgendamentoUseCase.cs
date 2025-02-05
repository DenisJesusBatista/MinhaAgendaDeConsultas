using MinhaAgendaDeConsultas.Communication.Resposta;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Excluir
{
    public interface IExcluirConsultaAgendamentoUseCase
    {
        public Task<ResponseExcluirAgendamentoConsultas> Executar(long agendamentoID);
    }
}
