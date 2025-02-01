using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Domain.Enumeradores
{
    public enum TipoUsuario
    {
        [Description("Usuario")]
        Usuario = 0,
        [Description("Paciente")]
        Paciente = 1,
        [Description("Medico")]
        Medico = 2

    }
}
