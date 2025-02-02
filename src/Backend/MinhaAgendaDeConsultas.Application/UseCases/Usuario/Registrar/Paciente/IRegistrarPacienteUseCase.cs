using MinhaAgendaDeConsultas.Communication.Requisicoes.Paciente;
using MinhaAgendaDeConsultas.Communication.Resposta.Paciente;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Paciente
{
    public interface IRegistrarPacienteUseCase
    {
        Task<ResponseRegistrarPacienteJson> Executar(RequisicaoRegistrarPacienteJson requisicao);
    }
}
