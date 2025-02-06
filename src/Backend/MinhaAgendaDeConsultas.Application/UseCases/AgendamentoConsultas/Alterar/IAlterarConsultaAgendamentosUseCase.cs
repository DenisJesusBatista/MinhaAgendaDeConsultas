using MinhaAgendaDeConsultas.Communication.Requisicoes;
using MinhaAgendaDeConsultas.Communication.Resposta;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Alterar
{
    public interface IAlterarConsultaAgendamentosUseCase
    {
        public Task<ResponseAlterarAgendamentoConsultas> Executar(RequisicaoAgendamentoConsultasJson agendamento);
    }
}
