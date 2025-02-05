using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Domain.Entidades
{
    public class HorarioDisponivel : EntidadeBase
    {
        public DateTime DataHora { get; set; }
        public bool Disponivel { get; set; }
        public HorarioDisponivel(DateTime dataHora)
        {
            DataHora = dataHora;
            Disponivel = true;
        }
    }
}
