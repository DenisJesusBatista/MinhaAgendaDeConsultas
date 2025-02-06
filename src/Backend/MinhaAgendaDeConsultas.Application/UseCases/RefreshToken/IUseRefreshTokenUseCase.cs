using MinhaAgendaDeConsultas.Communication.Requisicoes.Paciente;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Token;
using MinhaAgendaDeConsultas.Communication.Resposta.Paciente;
using MinhaAgendaDeConsultas.Communication.Resposta.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Application.UseCases.RefreshToken;
public interface IUseRefreshTokenUseCase
{
    Task<RespostaTokenJson> Executar(RequisicaoNovoTokenJson requisicao);
    Task<RespostaTokenJson> ExecuteAsync(RequisicaoNovoTokenJson request);
}
