using MinhaAgendaDeConsultas.Domain.Enumeradores;

namespace MinhaAgendaDeConsultas.Domain.Extension;
public static class PerfilUsuarioExtensions
{
    public static string ParaString(this PerfilUsuario perfil)
    {
        return ((int)perfil).ToString();
    }
}


