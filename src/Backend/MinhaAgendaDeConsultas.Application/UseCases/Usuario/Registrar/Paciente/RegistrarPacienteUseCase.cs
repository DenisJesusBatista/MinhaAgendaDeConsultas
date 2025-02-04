using AutoMapper;
using FluentValidation.Results;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Paciente;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Paciente;
using MinhaAgendaDeConsultas.Communication.Resposta.Paciente;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Enumeradores;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain.Repositorios.Paciente;
using MinhaAgendaDeConsultas.Exceptions;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;


namespace MinhaAgendaDeConsultas.Application.UseCases
{
    public class RegistrarPacienteUseCase : IRegistrarPacienteUseCase
    {
        //Variavel readonly só pode ser atribuido valor nela, apenas no construtor da classe ( public RegistrarContatoUseCase(IContatoWriteOnlyRepositorio repositorio) )
        private readonly IPacienteReadOnlyRepositorio _pacienteReadOnlyRepositorio;
        private readonly IPacienteWriteOnlyRepositorio _pacienteWriteOnlyRepositorio;
        private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;
        private readonly IMapper _mapper;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;


        //Configurar a injeção de dependência atalho CTOR - Criar 
        //Construtor
        public RegistrarPacienteUseCase(IPacienteReadOnlyRepositorio pacienteReadOnlyRepositorio, IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho,
              IPacienteWriteOnlyRepositorio pacienteWriteOnlyRepositorio, IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio)
        {
            _pacienteReadOnlyRepositorio = pacienteReadOnlyRepositorio;
            _mapper = mapper;
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _pacienteWriteOnlyRepositorio = pacienteWriteOnlyRepositorio;
            _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;   
        }

        public async Task<ResponseRegistrarPacienteJson> Executar(RequisicaoRegistrarPacienteJson requisicao)
        {
            await Validar(requisicao);

            //Conversão requisicao para entidade AutoMap
            //-Pluggin: AutoMapper na Application
            //-Pluggin: AutoMapper.Extensions.Microsoft.DependencyInjection na API para configurar para funcionar como injecao de dependencia

            var usuario = await _usuarioReadOnlyRepositorio.RecuperarPorEmail(requisicao.Email);


            var entidade = _mapper.Map<Domain.Entidades.Paciente>(requisicao);
            entidade.UsuarioId = usuario.Id;   

            
            entidade.Usuario.Identificador = usuario.Identificador;
            entidade.Usuario.IdentificadorString = usuario.IdentificadorString;
            entidade.Usuario.Senha = usuario.Senha;
            entidade.Usuario.Token = usuario.Token;

          
            //entidade.


            entidade.Tipo = TipoUsuario.Paciente;
            //var descricaoTipo = ConverteEnumerador.ObterDescricaoEnum(typeof(TipoUsuario), TipoUsuario.Paciente);
            //entidade.Tipo = descricaoTipo;

            await _pacienteWriteOnlyRepositorio.Adicionar(entidade);

            //Salvar no banco de dados.
            await _unidadeDeTrabalho.Commit();
            

            return new ResponseRegistrarPacienteJson
            {
                Nome = entidade.Nome,
            };
        }

        private async Task Validar(RequisicaoRegistrarPacienteJson requisicao)
        {
            var validator = new RegistrarPacienteValidator();
            var resultado = validator.Validate(requisicao);

            var existePacienteComCpf = await _pacienteReadOnlyRepositorio.ExistePacienteComCpf(requisicao.Cpf);
            var usuarioPaciente = await _pacienteReadOnlyRepositorio.ExistePacienteUsuarioComEmail(requisicao.Email);

            if (existePacienteComCpf)
            {
                resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("cpf", ResourceMessagesExceptions.CPF_JA_REGISTRADO));
            }

            // Agora usamos a classe ValidarAssociadosException
            ValidarAssociadosException.ValidarUsuarioAssociado(usuarioPaciente, TipoUsuario.Paciente, resultado.Errors);

            if (!resultado.IsValid)
            {
                var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrosDeValidacaoException(mensagensDeErro);
            }

            if (!resultado.IsValid)
            {
                var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrosDeValidacaoException(mensagensDeErro);
            }
        }

        private void ValidarUsuarioAssociado(bool usuarioExiste, TipoUsuario tipoUsuario, List<ValidationFailure> errors)
        {
            if (!usuarioExiste)
            {
                var mensagemErro = tipoUsuario == TipoUsuario.Medico
                    ? "Usuário não encontrado. Registre um usuário antes de criar um médico."
                    : "Usuário não encontrado. Registre um usuário antes de criar um paciente.";

                errors.Add(new ValidationFailure("cpf", mensagemErro));
            }
        }

    }
}
