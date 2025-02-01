using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Usuario
{
    public interface IRegistrarUsuarioUseCase
    {
        Task<ResponseRegistrarUsuarioJson> Executar(RequisicaoRegistrarUsuarioJson requisicao);
    }
}
