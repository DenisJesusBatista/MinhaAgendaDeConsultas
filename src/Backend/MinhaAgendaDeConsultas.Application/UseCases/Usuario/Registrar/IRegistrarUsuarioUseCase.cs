using MinhaAgendaDeConsultas.Communication.Request;
using MinhaAgendaDeConsultas.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar
{
    public interface IRegistrarUsuarioUseCase
    {
        Task<ResponseRegistrarUsuarioJson> Executar(RequisicaoRegistrarUsuarioJson requisicao);
    }
}
