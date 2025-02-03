using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinhaAgendaDeConsultas.Communication.Requisicoes;
using MinhaAgendaDeConsultas.Communication.Resposta;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Excluir
{
    public interface IExcluirConsultaAgendamentoUseCase
    {
        public Task<ResponseExcluirAgendamentoConsultas> Executar(long agendamentoID);
    }
}
