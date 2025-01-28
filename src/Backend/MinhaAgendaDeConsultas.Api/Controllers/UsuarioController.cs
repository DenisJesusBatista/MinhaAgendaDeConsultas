using Microsoft.AspNetCore.Mvc;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar;
using MinhaAgendaDeConsultas.Communication.Request;
using MinhaAgendaDeConsultas.Communication.Responses;

namespace MinhaAgendaDeConsultas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegistrarUsuarioJson), StatusCodes.Status201Created)]
        public IActionResult Registrar(RequestRegistrarUsuarioJson request)
        {
            var useCase = new RegistrarUsuarioUseCase(); 
            
            var result = useCase.Execute(request);

            return Created(string.Empty,result);   
        }
    }
}
