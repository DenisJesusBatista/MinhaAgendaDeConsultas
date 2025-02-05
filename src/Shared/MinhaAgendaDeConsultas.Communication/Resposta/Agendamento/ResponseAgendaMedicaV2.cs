using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Communication.Resposta.Agendamento
{
    public class ResponseAgendaMedica
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool IsDisponivel { get; set; }
    }
}
