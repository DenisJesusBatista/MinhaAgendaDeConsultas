using AutoMapper;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Seguranca.Token;
using MinhaAgendaDeConsultas.Domain.Servicos.UsuarioLogado;

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

        //Task<RespostaUsuarioProfileJson> Executar(string email);

        public async Task<RespostaUsuarioProfileJson> Executar(string email)
        {
            // Busca o usuário diretamente pelo e-mail fornecido
            var usuario = await _usuarioReadOnlyRepositorio.RecuperarPorEmail(email);

            // Garante que o usuário foi encontrado antes de acessar suas propriedades
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

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



        //public async Task<RespostaUsuarioProfileJson> Executar(RequisicaoObterUsuarioJson requisicao)
        //{
        //    var user = await _usuarioLogado.Usuario();

        //    var usuario = await _usuarioReadOnlyRepositorio.RecuperarPorEmail(user.Email);

        //    user.Token = usuario.Token;

        //    //return _mapper.Map<RespostaUsuarioProfileJson>(user.Email);

        //    // Cria o objeto RespostaTokenJson com o token
        //    var respostaToken = new RespostaTokenJson
        //    {
        //        AcessoToken = user.Token // Ou qualquer outra propriedade que você tenha para o token
        //    };


        //    return new RespostaUsuarioProfileJson
        //    {
        //        Nome = user.Nome,
        //        Email = user.Email, // Certifique-se de incluir o email, se necessário
        //        Tokens = respostaToken // Atribui o objeto RespostaTokenJson à propriedade Tokens
        //    };
        //}
    }
}
