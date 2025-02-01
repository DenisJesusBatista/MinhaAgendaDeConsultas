using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;

namespace MinhaAgendaDeConsultas.Application.UseCases.Login.DoLogin
{
    public interface IFazerLoginUseCase
    {
        public Task<ResponseRegistrarUsuarioJson> Execute(RequisicaoLoginJson request);
    }
}
