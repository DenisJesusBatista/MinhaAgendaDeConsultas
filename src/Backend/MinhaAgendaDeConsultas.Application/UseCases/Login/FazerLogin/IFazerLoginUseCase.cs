using MinhaAgendaDeConsultas.Communication.Requisicoes;
using MinhaAgendaDeConsultas.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Application.UseCases.Login.DoLogin
{
    public interface IFazerLoginUseCase
    {
        public Task<ResponseRegistrarUsuarioJson> Execute(RequisicaoLoginJson request);
    }
}
