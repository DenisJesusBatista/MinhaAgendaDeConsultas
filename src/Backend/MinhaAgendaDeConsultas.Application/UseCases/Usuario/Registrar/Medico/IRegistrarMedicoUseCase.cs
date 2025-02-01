using MinhaAgendaDeConsultas.Communication.Requisicoes.Medico;
using MinhaAgendaDeConsultas.Communication.Resposta.Medico;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Medico
{
    public interface IRegistrarMedicoUseCase
    {
        Task<ResponseRegistrarMedicoJson> Executar(RequisicaoRegistrarMedicoJson requisicao);
    }
}
