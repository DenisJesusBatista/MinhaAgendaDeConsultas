using AutoMapper;
using FluentValidation.Results;
using MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Validadores;
using MinhaAgendaDeConsultas.Communication.Requisicoes;
using MinhaAgendaDeConsultas.Communication.Resposta;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain.Repositorios.Agendamento;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Alterar
{
    public class AlterarConsultaAgendamentoUseCase : IAlterarConsultaAgendamentosUseCase
    {
        private readonly IAgendamentoConsultasConsultasOnlyRepositorio _agendamentoConsultas;
        private readonly IAgendamentoConsultasUpdateOnlyRepositorio _agendamentoConsultasUpdateOnlyRepositorio;
        private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;
        private readonly IMapper _mapper;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IAgendaMedicaConsultaOnlyRepository _agendaMedicaConsultaOnlyRepository;

        public AlterarConsultaAgendamentoUseCase(IAgendamentoConsultasConsultasOnlyRepositorio agendamentoConsultas,
            IAgendamentoConsultasUpdateOnlyRepositorio agendamentoConsultasUpdateOnlyRepositorio,
            IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio,
            IAgendaMedicaConsultaOnlyRepository agendaMedicaConsultaOnlyRepository,
            IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho)
        {

            this._agendamentoConsultas = agendamentoConsultas;
            this._agendamentoConsultasUpdateOnlyRepositorio = agendamentoConsultasUpdateOnlyRepositorio;
            this._usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
            _mapper = mapper;
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _agendaMedicaConsultaOnlyRepository = agendaMedicaConsultaOnlyRepository;
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
                await _unidadeDeTrabalho.RollbackTransaction();
                return new ResponseAlterarAgendamentoConsultas
                {
                    Message = "Erro: " + e.Message,
                    Success = false
                };
            }
        }

        private async Task Validate(RequisicaoAgendamentoConsultasJson agendamento)
        {
            var validador = new AgendamentoConsultasValidacao();
            var resultado = validador.Validate(agendamento);

            var usuarioMedico = await _usuarioReadOnlyRepositorio.RecuperarPorEmail(agendamento.MedicoEmail);

            //verifica se a agenda dele está aberta para o dia da consulta
            var agendaAberta = await _agendaMedicaConsultaOnlyRepository.VerificarDisponibilidade(usuarioMedico.Id, agendamento.DataHoraFim, agendamento.DataHoraFim);

         
            //Verifica se há horário livre com o médico 
            var disponivel = await _agendamentoConsultas.GetDisponibilides(agendamento.DataHoraInicio, agendamento.DataHoraFim, usuarioMedico.Id);
            if (usuarioMedico == null)
            {
                resultado.Errors.Add(new ValidationFailure("MedicoEmail", "Médico não encontrado ou não cadastrado"));
            }
            if (!agendaAberta)
            {
                resultado.Errors.Add(new ValidationFailure("DataHoraInicio", "Agenda do médico não está aberta para o dia da consulta"));
            }

            if (!disponivel)
            {
                resultado.Errors.Add(new ValidationFailure("DataHoraInicio", "Horário indisponível para esse médico"));
            }
            if (!resultado.IsValid)
            {
                var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrosDeValidacaoException(mensagensDeErro);
            }
        }
    }
}
