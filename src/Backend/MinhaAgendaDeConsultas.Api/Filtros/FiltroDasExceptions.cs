using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;
using MinhaAgendaDeConsultas.Communication.Responses;
using MinhaAgendaDeConsultas.Exceptions;

namespace MinhaAgendaDeConsultas.Api.Filtros
{
    public class FiltroDasExceptions : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is MinhaAgendaDeContatosExceptions)
            {
                TratarMinhaAgendaContatoException(context);
            }
            else
            {

            }
        }

        private void TratarMinhaAgendaContatoException(ExceptionContext context)
        {
            if (context.Exception is ErrosDeValidacaoException)
            {
                TratarErroDeValidacaoException(context);
            }
        }

        private void TratarErroDeValidacaoException(ExceptionContext context)
        {
            var erroDeValidacaoException = context.Exception as ErrosDeValidacaoException;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult(new RespostaErroJson(erroDeValidacaoException.MensagensDeErro));

        }

        private void LancarErroDesconhecido(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new RespostaErroJson(ResourceMessagesExceptions.ERRO_DESCONHECIDO));
        }
    }
}
