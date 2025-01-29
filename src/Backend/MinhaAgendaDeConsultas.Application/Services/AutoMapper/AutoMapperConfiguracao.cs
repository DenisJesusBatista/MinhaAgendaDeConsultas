using AutoMapper;
using MinhaAgendaDeConsultas.Communication.Request;
using MinhaAgendaDeConsultas.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MinhaAgendaDeConsultas.Communication.Resposta.RespostaUsuarioJson;

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
