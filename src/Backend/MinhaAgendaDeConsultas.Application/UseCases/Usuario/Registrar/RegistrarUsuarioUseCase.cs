using MinhaAgendaDeConsultas.Application.Services.Criptografia;
using MinhaAgendaDeConsultas.Communication.Request;
using MinhaAgendaDeConsultas.Communication.Responses;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar
{
    public class RegistrarUsuarioUseCase
    {
        public ResponseRegistrarUsuarioJson Execute(RequestRegistrarUsuarioJson request)
        {
            //Criptografar a senha  
            var criptografiaDeSenha = new PasswordEncripter();


            var autoMapper = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RequestRegistrarUsuarioJson, Domain.Entidades.Usuario>();
            }).CreateMapper();


            //Validar a request
            Validar(request); //Validar a request  

            //mapear a request em uma entidade
            var usuario = autoMapper.Map<Domain.Entidades.Usuario>(request); //mapear a request em uma entidade 


            usuario.Senha = criptografiaDeSenha.Encrypt(request.Senha); //Criptografar a senha    
       

            //Salvar no banco de dados   
            return new ResponseRegistrarUsuarioJson
            {
                Nome = request.Nome
            };
        }

        private void Validar(RequestRegistrarUsuarioJson request)
        {
           var validator = new RegistrarUsuarioValidator(); 

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new System.Exception("Erro de validação");
            }   

        }

    }
}
