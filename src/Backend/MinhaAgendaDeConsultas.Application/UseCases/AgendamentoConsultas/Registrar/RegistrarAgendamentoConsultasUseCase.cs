using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Validadores;
using MinhaAgendaDeConsultas.Communication.Requisicoes;
using MinhaAgendaDeConsultas.Communication.Resposta;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Registrar
{
    public class RegistrarAgendamentoConsultasUseCase : IRegistrarAgendamentoConsultasUseCase
    {
        private readonly IAgendamentoConsultasConsultasOnlyRepositorio _agendamentoConsultas;
        private readonly IAgendamentoConsultasWriteOnlyRepositorio _agendamentoConsultasWriteOnlyRepositorio;
        private readonly IMapper _mapper;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;


        public RegistrarAgendamentoConsultasUseCase(IAgendamentoConsultasConsultasOnlyRepositorio agendamentoConsultas,
            IAgendamentoConsultasWriteOnlyRepositorio agendamentoConsultasWriteOnlyRepositorio,
            IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            this._agendamentoConsultas = agendamentoConsultas;
            this._agendamentoConsultasWriteOnlyRepositorio = agendamentoConsultasWriteOnlyRepositorio;
            _mapper = mapper;
            _unidadeDeTrabalho = unidadeDeTrabalho;
        }
        public async Task<ResponseRegistrarAgendamentoConsultas> Executar(RequisicaoAgendamentoConsultasJson agendamento)
        {
            await Validate(agendamento);
            var entidade = _mapper.Map<Domain.Entidades.AgendamentoConsultas>(agendamento);

            await _unidadeDeTrabalho.BeginTransaction();
            try
            {
                await _unidadeDeTrabalho.LockTableAsync<Domain.Entidades.AgendamentoConsultas>();

                await _agendamentoConsultasWriteOnlyRepositorio.Add(entidade);
                await _unidadeDeTrabalho.Commit();
                await _unidadeDeTrabalho.CommitTransaction();

                return new ResponseRegistrarAgendamentoConsultas
                {
                    Message = "Agendamento realizado com sucesso",
                    Success = true
                };
            }
            catch (Exception e)
            {

                throw ;
            }
      
          
        }

        private async Task Validate(RequisicaoAgendamentoConsultasJson agendamento)
        {
            var validador = new AgendamentoConsultasValidacao();
            var resultado = validador.Validate(agendamento);


            var ok = await _agendamentoConsultas.GetDisponibilides(agendamento.DataHoraInicio, agendamento.DataHoraFim, agendamento.MedicoId);

            if (!ok)
            {
                resultado.Errors.Add(new ValidationFailure("DataHoraInicio", "Horário indisponível"));
            }
            if (!resultado.IsValid)
            {
                var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrosDeValidacaoException(mensagensDeErro);
            }
        }
    }
}
