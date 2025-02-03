using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinhaAgendaDeConsultas.Domain.Entidades;

namespace MinhaAgendaDeConsultas.Communication.Requisicoes
{
    public class RequisicaoAgendamentoConsultasJson
    {
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }     

        
    }


}
