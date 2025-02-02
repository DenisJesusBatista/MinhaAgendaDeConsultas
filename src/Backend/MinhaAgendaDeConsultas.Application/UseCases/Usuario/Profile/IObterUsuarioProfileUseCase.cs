using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Profile
{
    public interface IObterUsuarioProfileUseCase
    {
        Task<RespostaUsuarioProfileJson> Executar(RequisicaoObterUsuarioJson requisicao);

    }
}
