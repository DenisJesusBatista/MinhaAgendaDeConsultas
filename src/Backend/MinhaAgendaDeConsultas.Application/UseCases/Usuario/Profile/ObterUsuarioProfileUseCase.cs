using AutoMapper;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.ObterUsuario;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Medico;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Servicos.UsuarioLogado;
using MinhaAgendaDeConsultas.Exceptions;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Profile
{
    public class ObterUsuarioProfileUseCase : IObterUsuarioProfileUseCase
    {
        private readonly IUsuarioLogado _usuarioLogado;
        private readonly IMapper _mapper;
        private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;

        public ObterUsuarioProfileUseCase(IUsuarioLogado usuarioLogado, IMapper mapper, IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio)
        {
            _usuarioLogado = usuarioLogado;
            _mapper = mapper;
            _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;


        }

        public async Task<RespostaUsuarioProfileJson> Executar(RequisicaoObterUsuarioJson requisicao)
        {
            await Validar(requisicao);

            // Busca o usuário diretamente pelo e-mail fornecido
            var usuario = await _usuarioReadOnlyRepositorio.RecuperarPorEmail(requisicao.Email);


            // Cria a resposta com o token correto
            var respostaToken = new RespostaTokenJson
            {
                AcessoToken = usuario.Token // Usa diretamente o token do usuário recuperado
            };

            return new RespostaUsuarioProfileJson
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Tokens = respostaToken
            };
        }

        public async Task<IEnumerable<RequisicaoRegistrarMedicoJson>> Executar(RequisicaoMedicoPorEspecialidadeJson requisicao)
        {
            var lstReturn = new List<RequisicaoRegistrarMedicoJson>();

            // Busca o usuário diretamente pelo especialidade fornecida
            var usuario = await _usuarioReadOnlyRepositorio.RecuperarPorEspecialidade(requisicao.Especialidade);

            foreach (var item in usuario)
            {
                lstReturn.Add(new RequisicaoRegistrarMedicoJson { Nome = item.Nome, Email = item.Email });                
            }

            return lstReturn;
        }

        private async Task Validar(RequisicaoObterUsuarioJson requisicao)
        {
            var validator = new ObterUsuarioValidator();
            var resultado = validator.Validate(requisicao);

            var existeContatoComEmail = await _usuarioReadOnlyRepositorio.ExisteUsuarioComEmail(requisicao.Email);

            if (!existeContatoComEmail)
            {
                resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessagesExceptions.USUARIO_NAO_ENCONTRADO_EMAIL));
            }

            if (!resultado.IsValid)
            {
                var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrosDeValidacaoException(mensagensDeErro);
            }
        }
    }
}
