using AutoMapper;
using FluentValidation.Results;
using MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Validadores;
using MinhaAgendaDeConsultas.Communication.Requisicoes;
using MinhaAgendaDeConsultas.Communication.Resposta;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Alterar
{
    public class AlterarConsultaAgendamentoUseCase : IAlterarConsultaAgendamentosUseCase
    {
        private readonly IAgendamentoConsultasConsultasOnlyRepositorio _agendamentoConsultas;
        private readonly IAgendamentoConsultasUpdateOnlyRepositorio _agendamentoConsultasUpdateOnlyRepositorio;
        private readonly IMapper _mapper;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;


        public AlterarConsultaAgendamentoUseCase(IAgendamentoConsultasConsultasOnlyRepositorio agendamentoConsultas,
            IAgendamentoConsultasUpdateOnlyRepositorio agendamentoConsultasUpdateOnlyRepositorio,
            IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho)
        {

            this._agendamentoConsultas = agendamentoConsultas;
            this._agendamentoConsultasUpdateOnlyRepositorio = agendamentoConsultasUpdateOnlyRepositorio;
            _mapper = mapper;
            _unidadeDeTrabalho = unidadeDeTrabalho;
        }
        public async Task<ResponseAlterarAgendamentoConsultas> Executar(RequisicaoAgendamentoConsultasJson agendamento)
        {
            await Validate(agendamento);
            var entidade = _mapper.Map<Domain.Entidades.AgendamentoConsultas>(agendamento);

            await _unidadeDeTrabalho.BeginTransaction();
            try
            {
                await _unidadeDeTrabalho.LockTableAsync(nameof(AgendamentoConsultas));

                await _agendamentoConsultasUpdateOnlyRepositorio.Update(entidade);
                await _unidadeDeTrabalho.Commit();
                await _unidadeDeTrabalho.CommitTransaction();

                return new ResponseAlterarAgendamentoConsultas
                {
                    Message = "Agendamento alterado realizado com sucesso",
                    Success = true
                };
            }
            catch (Exception e)
            {

                throw;
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
