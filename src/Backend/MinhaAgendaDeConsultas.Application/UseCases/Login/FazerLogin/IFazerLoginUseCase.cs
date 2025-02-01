using MinhaAgendaDeConsultas.Communication.Requisicoes.Login;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;

namespace MinhaAgendaDeConsultas.Application.UseCases.Login.FazerLogin
{
    public interface IFazerLoginUseCase
    {
        public Task<ResponseRegistrarUsuarioJson> Execute(RequisicaoLoginJson request);
        public Task<ResponseRegistrarUsuarioJson> ObterUsarioLogado();
    }
}
