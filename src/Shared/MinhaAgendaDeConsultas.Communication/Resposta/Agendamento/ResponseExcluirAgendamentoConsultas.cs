using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Communication.Resposta
{
    public class ResponseExcluirAgendamentoConsultas
    {

        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
    }
}
