using AutoMapper;
using MinhaAgendaDeConsultas.Communication.Resposta;
using MinhaAgendaDeConsultas.Domain.Repositorios;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Excluir
{
    public class ExcluirConsultaAgendamentoUseCase : IExcluirConsultaAgendamentoUseCase
    {
        private readonly IAgendamentoConsultasConsultasOnlyRepositorio _agendamentoConsultas;
        private readonly IAgendamentoConsultasDeleteOnlyRepository _agendamentoConsultasDeleteOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;


        public ExcluirConsultaAgendamentoUseCase(IAgendamentoConsultasConsultasOnlyRepositorio agendamentoConsultas,
            IAgendamentoConsultasDeleteOnlyRepository agendamentoConsultasDeleteOnlyRepository,
            IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho)
        {

            this._agendamentoConsultas = agendamentoConsultas;
            this._agendamentoConsultasDeleteOnlyRepository = agendamentoConsultasDeleteOnlyRepository;
            _mapper = mapper;
            _unidadeDeTrabalho = unidadeDeTrabalho;
        }
        public async Task<ResponseExcluirAgendamentoConsultas> Executar(long agendamentoID)
        {


            await _unidadeDeTrabalho.BeginTransaction();
            try
            {
                await _unidadeDeTrabalho.LockTableAsync(nameof(AgendamentoConsultas));

                await _agendamentoConsultasDeleteOnlyRepository.Delete(agendamentoID);
                await _unidadeDeTrabalho.Commit();
                await _unidadeDeTrabalho.CommitTransaction();

                return new ResponseExcluirAgendamentoConsultas
                {
                    Message = "Agendamento excluído com sucesso",
                    Success = true
                };
            }
            catch (Exception e)
            {
                await _unidadeDeTrabalho.RollbackTransaction();
                throw e;
            }
        }


    }
}
