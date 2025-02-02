using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Api.Filtros;

namespace MinhaAgendaDeConsultas.Api.Atributos
{
    public class AtributoAutorizacaoUsuario : TypeFilterAttribute
    {
        public AtributoAutorizacaoUsuario() : base(typeof(AutenticandoUsuarioFiltro))
        {
        }
    }

}
