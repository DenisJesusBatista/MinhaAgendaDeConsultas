using AutoMapper;
using FluentValidation.Results;
using MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Validadores;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Agendamento;
using MinhaAgendaDeConsultas.Communication.Resposta.Agendamento;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain.Repositorios.Agendamento;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Registrar
{
    public class AgendaMedicaRegistrarUseCase : IAgendaMedicaRegistrarUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IAgendaMedicaWriteOnlyRepository _agendaMedicaWriteOnlyRepository;
        private readonly IAgendaMedicaConsultaOnlyRepository _agendaMedicaConsultaOnlyRepository;
        private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;

        public AgendaMedicaRegistrarUseCase(IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho,
            IAgendaMedicaWriteOnlyRepository agendaMedicaWriteOnlyRepository,
            IAgendaMedicaConsultaOnlyRepository agendaMedicaConsultaOnlyRepository,
            IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio)
        {
            _mapper = mapper;
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _agendaMedicaWriteOnlyRepository = agendaMedicaWriteOnlyRepository;
            _agendaMedicaConsultaOnlyRepository = agendaMedicaConsultaOnlyRepository;
            _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
        }

        public async Task<ResponseAgendaMedicaResult> Executar(RequisicaoAgendaMedicaJson agendaMedica)
        {
            await Validate(agendaMedica);

            var entidade = _mapper.Map<Domain.Entidades.AgendaMedica>(agendaMedica);
            var usuário = _usuarioReadOnlyRepositorio.RecuperarPorEmail(agendaMedica.MedicoEmail);

            await _unidadeDeTrabalho.BeginTransaction();
            try
            {
                entidade.MedicoId = usuário.Id;

                await _agendaMedicaWriteOnlyRepository.Add(entidade);

                await _unidadeDeTrabalho.Commit();
                await _unidadeDeTrabalho.CommitTransaction();

                return new ResponseAgendaMedicaResult
                {
                    Message = "Agendamento realizado com sucesso",
                    Success = true
                };
            }
            catch (Exception e)
            {
                await _unidadeDeTrabalho.RollbackTransaction();
                return new ResponseAgendaMedicaResult
                {
                    Message = "Erro: "+e.Message,
                    Success = false
                };
            }

        }

        private async Task Validate(RequisicaoAgendaMedicaJson agendamento)
        {
            var validador = new AgendaMedicaValidacao();
            var resultado = validador.Validate(agendamento);

            var usuarioMedico = await _usuarioReadOnlyRepositorio.RecuperarPorEmail(agendamento.MedicoEmail);


            var ok = await _agendaMedicaConsultaOnlyRepository.VerificarDisponibilidade(usuarioMedico.Id, agendamento.DataInicio, agendamento.DataFim);


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
