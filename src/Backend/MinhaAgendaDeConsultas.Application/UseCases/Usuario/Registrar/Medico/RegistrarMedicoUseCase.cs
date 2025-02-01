using AutoMapper;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Medico;
using MinhaAgendaDeConsultas.Communication.Resposta.Medico;
using MinhaAgendaDeConsultas.Domain.Enumeradores;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain.Repositorios.Medico;
using MinhaAgendaDeConsultas.Exceptions;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Medico
{
    public class RegistrarMedicoUseCase : IRegistrarMedicoUseCase
    {
        //Variavel readonly só pode ser atribuido valor nela, apenas no construtor da classe ( public RegistrarContatoUseCase(IContatoWriteOnlyRepositorio repositorio) )
        private readonly IMedicoReadOnlyRepositorio _medicoReadOnlyRepositorio;
        private readonly IMedicoWriteOnlyRepositorio _medicoWriteOnlyRepositorio;
        private readonly IMapper _mapper;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

        //Configurar a injeção de dependência atalho CTOR - Criar 
        //Construtor
        public RegistrarMedicoUseCase(IMedicoReadOnlyRepositorio medicoReadOnlyRepositorio, IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho,
              IMedicoWriteOnlyRepositorio medicoWriteOnlyRepositorio)
        {
            _medicoReadOnlyRepositorio = medicoReadOnlyRepositorio;
            _mapper = mapper;
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _medicoWriteOnlyRepositorio = medicoWriteOnlyRepositorio;
        }

        public async Task<ResponseRegistrarMedicoJson> Executar(RequisicaoRegistrarMedicoJson requisicao)
        {
            await Validar(requisicao);

            //Conversão requisicao para entidade AutoMap
            //-Pluggin: AutoMapper na Application
            //-Pluggin: AutoMapper.Extensions.Microsoft.DependencyInjection na API para configurar para funcionar como injecao de dependencia

            var entidade = _mapper.Map<Domain.Entidades.Medico>(requisicao);


            var descricaoTipo = ConverteEnumerador.ObterDescricaoEnum(typeof(TipoUsuario), TipoUsuario.Medico);

            entidade.Tipo = TipoUsuario.Medico;



            await _medicoWriteOnlyRepositorio.Adicionar(entidade);

            //Salvar no banco de dados.
            await _unidadeDeTrabalho.Commit();

            return new ResponseRegistrarMedicoJson
            {
                Nome = entidade.Nome,
            };
        }

        private async Task Validar(RequisicaoRegistrarMedicoJson requisicao)
        {
            var validator = new RegistrarMedicoValidator();
            var resultado = validator.Validate(requisicao);

            var existeMedicoComCpf = await _medicoReadOnlyRepositorio.ExisteMedicoComCpf(requisicao.Cpf);
            var existeMedicoComCrm = await _medicoReadOnlyRepositorio.ExisteMedicoComCrm(requisicao.Crm);
            

            if (existeMedicoComCpf)
            {
                resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("cpf", ResourceMessagesExceptions.CPF_JA_REGISTRADO));
            }

            if (existeMedicoComCrm)
            {
                resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("crm", ResourceMessagesExceptions.CRM_JA_REGISTRADO));
            }

            if (!resultado.IsValid)
            {
                var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrosDeValidacaoException(mensagensDeErro);
            }
        }
    }
}
