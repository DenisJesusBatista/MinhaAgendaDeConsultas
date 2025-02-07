using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Communication.Resposta
{
    public class TableLockInfo
    {
        public string Relname { get; set; } // Nome da tabela
        public string Locktype { get; set; } // Tipo de bloqueio
        public string Mode { get; set; } // Modo de bloqueio
        public bool Granted { get; set; } // Se o bloqueio foi concedido
    }
}
