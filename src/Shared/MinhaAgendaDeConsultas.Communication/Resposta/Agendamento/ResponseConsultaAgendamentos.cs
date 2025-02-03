using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Communication.Resposta.Agendamento
{
    public  class ResponseConsultaAgendamentos
    {
        public long PacienteId { get; set; }
        public long MedicoId { get; set; }  
        public DateTime DataInclusao { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
    }
}
