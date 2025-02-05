using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Communication.Requisicoes.Agendamento
{
    public class RequisicaoAgendaMedicaJson
    {
        public string MedicoEmail { get; set; }
        public DateTime DataPretendidaInicio { get; set; }
        public DateTime DataPretendidaFim { get; set; }
    }
}
