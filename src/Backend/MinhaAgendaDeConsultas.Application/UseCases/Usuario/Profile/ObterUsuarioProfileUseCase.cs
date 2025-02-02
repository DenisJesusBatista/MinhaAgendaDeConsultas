using AutoMapper;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;
using MinhaAgendaDeConsultas.Domain.Servicos.UsuarioLogado;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Profile
{
    public class ObterUsuarioProfileUseCase : IObterUsuarioProfileUseCase
    {
        private readonly IUsuarioLogado _usuarioLogado;
        private readonly IMapper _mapper;

        public ObterUsuarioProfileUseCase(IUsuarioLogado usuarioLogado, IMapper mapper)
        {
            _usuarioLogado = usuarioLogado;
            _mapper = mapper;   
        }

        public async Task<RespostaUsuarioProfileJson> Executar()
        {
            var user = await _usuarioLogado.Usuario();

            return _mapper.Map<RespostaUsuarioProfileJson>(user);
        }
    }
}
