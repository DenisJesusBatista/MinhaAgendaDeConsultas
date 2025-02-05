using AutoMapper;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain.Repositorios.Agendamento;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Consultar
{
    public class AgendaMedicaConsultarUseCase : IAgendaMedicaConsultarUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IAgendaMedicaConsultaOnlyRepository _agendaMedicaConsultaOnlyRepository;
        private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;

        public AgendaMedicaConsultarUseCase(
             IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho,
              IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio,
              IAgendaMedicaConsultaOnlyRepository agendaMedicaConsultaOnlyRepository)
        {
            _mapper = mapper;
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _agendaMedicaConsultaOnlyRepository = agendaMedicaConsultaOnlyRepository;
            _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
        }
        public async Task<bool> ConsultarDisponibilidade(DateTime DataInicio, DateTime DataFim, string medicoEmail)
        {
            var medico = await _usuarioReadOnlyRepositorio.RecuperarPorEmail(medicoEmail);

            return await _agendaMedicaConsultaOnlyRepository.VerificarDisponibilidade(medico.Id,DataInicio, DataFim);
        }
    }
}
