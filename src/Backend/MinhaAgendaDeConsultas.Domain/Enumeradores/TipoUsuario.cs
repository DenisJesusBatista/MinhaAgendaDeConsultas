using System.ComponentModel;

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
