using AutoMapper;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Agendamento;
using MinhaAgendaDeConsultas.Communication.Resposta.Agendamento;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain.Repositorios.Agendamento;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Excluir
{
    public class AgendaMedicaExcluirUseCase : IAgendaMedicaExcluirUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IAgendaMedicaDeleteOnlyRepository _agendaMedicaDeleteOnlyRepository;
        private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;

        public AgendaMedicaExcluirUseCase(IAgendaMedicaDeleteOnlyRepository agendaMedicaDeleteOnlyRepository,
            IAgendaMedicaConsultaOnlyRepository agendaMedicaConsultaOnlyRepository,
             IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio,
             IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            _mapper = mapper;
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _agendaMedicaDeleteOnlyRepository = agendaMedicaDeleteOnlyRepository;
            _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
        }

        public async Task<ResponseAgendaMedicaResult> Executar(long id)
        {
            await _unidadeDeTrabalho.BeginTransaction();
            try
            {
                await _unidadeDeTrabalho.LockTableAsync(nameof(Domain.Entidades.AgendaMedica));
                await _agendaMedicaDeleteOnlyRepository.Delete(id);
                await _unidadeDeTrabalho.Commit();
                await _unidadeDeTrabalho.CommitTransaction();
                return new ResponseAgendaMedicaResult
                {
                    Message = "Agendamento excluído com sucesso",
                    Success = true
                };
            }
            catch (Exception e)
            {
                await _unidadeDeTrabalho.RollbackTransaction();
                return new ResponseAgendaMedicaResult
                {
                    Message = "Erro: " + e.Message,
                    Success = false
                };
            }
        }
    }
}
