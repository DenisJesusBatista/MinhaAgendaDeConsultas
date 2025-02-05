using AutoMapper;
using FluentValidation.Results;
using MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Validadores;
using MinhaAgendaDeConsultas.Communication.Requisicoes;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Agendamento;
using MinhaAgendaDeConsultas.Communication.Resposta.Agendamento;
using MinhaAgendaDeConsultas.Domain.Entidades;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain.Repositorios.Agendamento;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;
using MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Validadores;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Alterar
{
    public class AgendaMedicaAlterarUseCase : IAgendaMedicaAlterarUseCase
    {
        private readonly IAgendaMedicaUpdateOnlyRepository _agendaMedicaUpdateOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IAgendaMedicaConsultaOnlyRepository _agendaMedicaConsultaOnlyRepository;
        private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;

        public AgendaMedicaAlterarUseCase(IAgendaMedicaUpdateOnlyRepository agendaMedicaUpdateOnlyRepository,
            IAgendaMedicaConsultaOnlyRepository agendaMedicaConsultaOnlyRepository,
             IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio,
             IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            this._agendaMedicaUpdateOnlyRepository = agendaMedicaUpdateOnlyRepository;
            _mapper = mapper;
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _agendaMedicaConsultaOnlyRepository = agendaMedicaConsultaOnlyRepository;
            _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
        }

        public async Task<ResponseAgendaMedica> Executar(RequisicaoAgendaMedicaJson agendaMedica)
        {
            await Validate(agendaMedica);

            var entidade = _mapper.Map<Domain.Entidades.AgendaMedica>(agendaMedica);
            var usuário = _usuarioReadOnlyRepositorio.RecuperarPorEmail(agendaMedica.MedicoEmail);

            await _unidadeDeTrabalho.BeginTransaction();
            try
            {
                entidade.MedicoId = usuário.Id;

                await _agendaMedicaUpdateOnlyRepository.Update(entidade);

                await _unidadeDeTrabalho.Commit();
                await _unidadeDeTrabalho.CommitTransaction();

                return new ResponseAgendaMedica
                {
                    Message = "Agendamento alterado com sucesso",
                    Success = true
                };
            }
            catch (Exception e)
            {
                await _unidadeDeTrabalho.RollbackTransaction();
                return new ResponseAgendaMedica
                {
                    Message = "Erro: " + e.Message,
                    Success = false
                };
            }
        }

        private async Task Validate(RequisicaoAgendaMedicaJson agendamento)
        {
            var validador = new AgendaMedicaValidacao();
            var resultado = validador.Validate(agendamento);

            var usuarioMedico = await _usuarioReadOnlyRepositorio.RecuperarPorEmail(agendamento.MedicoEmail);


            var ok = await _agendaMedicaConsultaOnlyRepository.VerificarDisponibilidade(usuarioMedico.Id, agendamento.DataPretendidaInicio, agendamento.DataPretendidaFim)


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
