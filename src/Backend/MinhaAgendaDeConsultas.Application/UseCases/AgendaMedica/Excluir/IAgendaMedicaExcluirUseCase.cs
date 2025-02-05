using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Agendamento;
using MinhaAgendaDeConsultas.Communication.Resposta.Agendamento;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Excluir
{
    public interface IAgendaMedicaExcluirUseCase
    {
        public Task<ResponseAgendaMedica> Executar(long Id);
    }
}

