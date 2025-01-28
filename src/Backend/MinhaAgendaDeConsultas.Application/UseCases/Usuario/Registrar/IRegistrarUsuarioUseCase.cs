using MinhaAgendaDeConsultas.Communication.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar
{
    public interface IRegistrarUsuarioUseCase
    {
        Task Executar(RequestRegistrarUsuarioJson requisicao);
    }
}
