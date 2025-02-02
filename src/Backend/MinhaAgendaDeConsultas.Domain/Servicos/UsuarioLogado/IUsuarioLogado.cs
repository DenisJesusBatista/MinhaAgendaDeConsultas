using MinhaAgendaDeConsultas.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Domain.Servicos.UsuarioLogado
{
    public interface IUsuarioLogado
    {
        public Task<Usuario> Usuario();
    }
}
