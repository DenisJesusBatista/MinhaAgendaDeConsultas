using AutoMapper;
using FluentValidation.Results;
using MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Validadores;
using MinhaAgendaDeConsultas.Communication.Requisicoes;
using MinhaAgendaDeConsultas.Communication.Resposta;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain.Repositorios.Agendamento;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Registrar
{
    public class RegistrarAgendamentoConsultasUseCase : IRegistrarAgendamentoConsultasUseCase
    {
        private readonly IAgendamentoConsultasConsultasOnlyRepositorio _agendamentoConsultas;
        private readonly IAgendamentoConsultasWriteOnlyRepositorio _agendamentoConsultasWriteOnlyRepositorio;
        private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;
        private readonly IMapper _mapper;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IAgendaMedicaConsultaOnlyRepository _agendaMedicaConsultaOnlyRepository;

        public RegistrarAgendamentoConsultasUseCase(IAgendamentoConsultasConsultasOnlyRepositorio agendamentoConsultas,
            IAgendamentoConsultasWriteOnlyRepositorio agendamentoConsultasWriteOnlyRepositorio,
            IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio,
              IAgendaMedicaConsultaOnlyRepository agendaMedicaConsultaOnlyRepository,
            IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho
            )
        {
            this._agendamentoConsultas = agendamentoConsultas;
            this._agendamentoConsultasWriteOnlyRepositorio = agendamentoConsultasWriteOnlyRepositorio;
            this._usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
            _mapper = mapper;
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _agendaMedicaConsultaOnlyRepository = agendaMedicaConsultaOnlyRepository;
        }
        public async Task<ResponseRegistrarAgendamentoConsultas> Executar(RequisicaoAgendamentoConsultasJson agendamento)
        {
            await Validate(agendamento);
            var entidade = _mapper.Map<Domain.Entidades.AgendamentoConsultas>(agendamento);

            await _unidadeDeTrabalho.BeginTransaction();
            try
            {

                var medicoUsuario = await _usuarioReadOnlyRepositorio.RecuperarPorEmail(agendamento.MedicoEmail);
                var pacienteUsuario = await _usuarioReadOnlyRepositorio.RecuperarPorEmail(agendamento.PacienteEmail);

                entidade.DataInclusao = entidade.DataInclusao.ToUniversalTime();
                entidade.DataHoraInicio = entidade.DataHoraInicio.ToUniversalTime();
                entidade.DataHoraFim = entidade.DataHoraFim.ToUniversalTime();
                entidade.MedicoId = medicoUsuario.Id;
                entidade.PacienteId = pacienteUsuario.Id;


                await _unidadeDeTrabalho.LockTableAsync(nameof(AgendamentoConsultas));

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

                throw e;
            }


        }

        private async Task Validate(RequisicaoAgendamentoConsultasJson agendamento)
        {
            var validador = new AgendamentoConsultasValidacao();
            var resultado = validador.Validate(agendamento);

            var usuarioMedico = await _usuarioReadOnlyRepositorio.RecuperarPorEmail(agendamento.MedicoEmail);
            List<string>? mensagensDeErro = new List<string>();
            
            if(usuarioMedico == null)
            {
                resultado.Errors.Add(new ValidationFailure("MedicoEmail", "Médico não encontrado ou não cadastrado"));
                mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrosDeValidacaoException(mensagensDeErro);
            }


            var disponibilidade = await _agendamentoConsultas.GetDisponibilides(agendamento.DataHoraInicio, agendamento.DataHoraFim, usuarioMedico.Id);


            //verifica se a agenda dele está aberta para o dia da consulta
            var agendaAberta = await _agendaMedicaConsultaOnlyRepository.VerificarDisponibilidade(usuarioMedico.Id, agendamento.DataHoraInicio, agendamento.DataHoraFim);
            
            if (usuarioMedico == null)
            {
                resultado.Errors.Add(new ValidationFailure("MedicoEmail", "Médico não encontrado ou não cadastrado"));
            }
            if (!agendaAberta)
            {
                resultado.Errors.Add(new ValidationFailure("DataHoraInicio", "Agenda do médico não está aberta para o dia da consulta"));
            }

            if (!disponibilidade)
            {
                resultado.Errors.Add(new ValidationFailure("DataHoraInicio", "Horário indisponível"));
            }
            if (!resultado.IsValid)
            {
                mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrosDeValidacaoException(mensagensDeErro);
            }
        }
    }
}
