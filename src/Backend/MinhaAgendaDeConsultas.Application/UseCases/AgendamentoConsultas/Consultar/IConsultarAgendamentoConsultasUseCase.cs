using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinhaAgendaDeConsultas.Communication.Resposta.Agendamento;
using MinhaAgendaDeConsultas.Domain.Entidades;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Consultar
{
    public interface IConsultarAgendamentoConsultasUseCase
    {
        public Task<IList<ResponseConsultaAgendamentos>> GetAgendamentosPaciente(int pacienteId);
        public Task<IList<ResponseConsultaAgendamentos>> GetAgendamentosMedico(int medicoId);
    }
}
