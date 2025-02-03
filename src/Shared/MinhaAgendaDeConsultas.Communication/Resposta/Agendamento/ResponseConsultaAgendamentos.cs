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
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }  
        public DateTime DataInclusao { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
    }
}
