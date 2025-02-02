using AutoMapper;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Medico;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Paciente;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;
using MinhaAgendaDeConsultas.Domain.Entidades;
using MinhaAgendaDeConsultas.Domain.Enumeradores;

namespace MinhaAgendaDeConsultas.Application.Services
{
    public class AutoMapperConfiguracao : Profile
    {
        public AutoMapperConfiguracao()
        {
            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            // Mapeamento de RequisicaoRegistrarUsuarioJson para Usuario (incluindo Ignorar Senha)
            CreateMap<RequisicaoRegistrarUsuarioJson, Domain.Entidades.Usuario>()
                .ForMember(dest => dest.Senha, opt => opt.Ignore()) // Ignorar a senha no mapeamento
                .ForMember(dest => dest.Identificador, opt => opt.MapFrom(src => Guid.NewGuid())) // Exemplo de mapeamento para Identificador
                .ForMember(dest => dest.Token, opt => opt.Ignore()) // Ignorar a propriedade Token no mapeamento (não vem da requisição)
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => TipoUsuario.Paciente)); // Assume Tipo como 'Medico'

            // Mapeamento de RequisicaoRegistrarPacienteJson para Paciente
            CreateMap<RequisicaoRegistrarPacienteJson, Paciente>()
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => TipoUsuario.Paciente))
                .ForMember(dest => dest.UsuarioId, opt => opt.Ignore()) // Será atribuído manualmente no serviço
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => new Usuario
                {
                    Nome = src.Nome,
                    Email = src.Email,
                    Cpf = src.Cpf,
                    Tipo = TipoUsuario.Paciente
                }));

            // Mapeamento de RequisicaoRegistrarMedicoJson para Medico
            CreateMap<RequisicaoRegistrarMedicoJson, Medico>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf))
                .ForMember(dest => dest.Crm, opt => opt.MapFrom(src => src.Crm))
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => TipoUsuario.Medico))
                .ForMember(dest => dest.UsuarioId, opt => opt.Ignore()) // Será atribuído manualmente no serviço
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => new Usuario
                {
                    Nome = src.Nome,
                    Email = src.Email,
                    Cpf = src.Cpf,
                    Tipo = TipoUsuario.Medico
                }));

        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entidades.Usuario, RespostaUsuarioProfileJson>();

        }

    }

}
