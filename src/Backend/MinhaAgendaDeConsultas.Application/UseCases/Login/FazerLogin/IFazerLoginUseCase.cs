using MinhaAgendaDeConsultas.Communication.Requisicoes;
using MinhaAgendaDeConsultas.Communication.Responses;

namespace MinhaAgendaDeConsultas.Application.UseCases.Login.DoLogin
{
    public interface IFazerLoginUseCase
    {
        public Task<ResponseRegistrarUsuarioJson> Execute(RequisicaoLoginJson request);
    }
}
