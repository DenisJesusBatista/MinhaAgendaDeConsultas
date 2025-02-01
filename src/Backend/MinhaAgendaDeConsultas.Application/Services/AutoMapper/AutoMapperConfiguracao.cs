using AutoMapper;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;


namespace MinhaAgendaDeConsultas.Application.Services.AutoMapper
{
    public class AutoMapperConfiguracao : Profile
    {
        public AutoMapperConfiguracao()
        {
            RequestToDomain();
        }

        private void RequestToDomain()
        {
            // Corrigido: Remover o mapeamento redundante
            CreateMap<RequisicaoRegistrarUsuarioJson, Domain.Entidades.Usuario>()
                .ForMember(dest => dest.Senha, opt => opt.Ignore()); // Ignora a senha no mapeamento
        }
    }

}
