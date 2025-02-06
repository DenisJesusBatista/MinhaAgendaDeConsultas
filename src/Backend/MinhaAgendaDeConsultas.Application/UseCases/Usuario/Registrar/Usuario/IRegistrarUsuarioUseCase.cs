using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Usuario
{
    public interface IRegistrarUsuarioUseCase
    {
        Task<ResponseRegistrarUsuarioJson> Executar(RequisicaoRegistrarUsuarioJson requisicao);
        Task<string> CriarESalvarRefreshToken(Domain.Entidades.Usuario usuario);
    }
}
