using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Api.Filtros;
using MinhaAgendaDeConsultas.Domain.Enumeradores;

namespace MinhaAgendaDeConsultas.Api.Atributos
{
    public class AtributoAutorizacaoUsuario : TypeFilterAttribute
    {
        public string Roles { get; set; }

        public AtributoAutorizacaoUsuario() : base(typeof(AutenticandoUsuarioFiltro))
        {
            this.Roles = "";
        }

        public AtributoAutorizacaoUsuario(params PerfilUsuario[] perfil) : base(typeof(AutenticandoUsuarioFiltro))
        {
            this.Roles = string.Join(",", perfil.Select(i => ((int)i).ToString()).ToArray());
        }
    }


}
