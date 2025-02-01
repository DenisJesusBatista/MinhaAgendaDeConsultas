using MinhaAgendaDeConsultas.Communication.Requisicoes.Paciente;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Communication.Resposta.Paciente;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Paciente
{
    public interface IRegistrarPacienteUseCase
    {
        Task<ResponseRegistrarPacienteJson> Executar(RequisicaoRegistrarPacienteJson requisicao);
    }
}
