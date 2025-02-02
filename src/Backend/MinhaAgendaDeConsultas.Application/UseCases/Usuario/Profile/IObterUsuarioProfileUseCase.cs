using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Profile
{
    public interface IObterUsuarioProfileUseCase
    {
        public Task<RespostaUsuarioProfileJson> Executar();
    }
}
