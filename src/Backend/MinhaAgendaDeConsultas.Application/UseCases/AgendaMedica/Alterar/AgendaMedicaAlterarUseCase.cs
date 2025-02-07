using AutoMapper;
using FluentValidation.Results;
using MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Validadores;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Agendamento;
using MinhaAgendaDeConsultas.Communication.Resposta.Agendamento;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain.Repositorios.Agendamento;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;

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

        public async Task<ResponseAgendaMedicaResult> Executar(RequisicaoAlteracaoAgendaMedicaJson agendaMedica)
        {
            while (await _unidadeDeTrabalho.TableIsLocked("AgendaMedica"))
            {
                await Task.Delay(1000);
            }
            await Validate(agendaMedica);

            var entidade = _mapper.Map<Domain.Entidades.AgendaMedica>(agendaMedica);
            var medico = await _usuarioReadOnlyRepositorio.RecuperarPorEmail(agendaMedica.MedicoEmail);
            var agendas = await _agendaMedicaConsultaOnlyRepository.ObterAgendasMedicias(medico.Id, agendaMedica.DataInicioAtual, agendaMedica.DataFimAtual);

            if (agendas.Count() == 0)
            {
                return new ResponseAgendaMedicaResult
                {
                    Message = "Agendamento não encontrado. Não pode ser alterado.",
                    Success = false
                };
            }

            var agendaAlterada = agendas.FirstOrDefault();
            agendaAlterada.DataInicio = agendaMedica.DataInicioNova;
            agendaAlterada.DataFim = agendaMedica.DataInicioNova;
            agendaAlterada.IsDisponivel = agendaMedica.IsDisponivel;

            await _unidadeDeTrabalho.BeginTransaction();
            try
            {
                entidade.MedicoId = medico.Id;
                await _unidadeDeTrabalho.LockTableAsync(nameof(Domain.Entidades.AgendaMedica));
                await _agendaMedicaUpdateOnlyRepository.Update(agendaAlterada);

                await _unidadeDeTrabalho.Commit();
                await _unidadeDeTrabalho.CommitTransaction();

                return new ResponseAgendaMedicaResult
                {
                    Message = "Agendamento alterado com sucesso",
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

        private async Task Validate(RequisicaoAlteracaoAgendaMedicaJson agendamento)
        {
            var validador = new AgendaMedicaValidacao();
            var resultado = validador.Validate(new RequisicaoAgendaMedicaJson
            {
                DataFim = agendamento.DataFimAtual,
                DataInicio = agendamento.DataInicioAtual,
                MedicoEmail = agendamento.MedicoEmail
            });

            var usuarioMedico = await _usuarioReadOnlyRepositorio.RecuperarPorEmail(agendamento.MedicoEmail);


            var ocupado = await _agendaMedicaConsultaOnlyRepository.VerificarDisponibilidade(usuarioMedico.Id, agendamento.DataInicioNova, agendamento.DataFimNova);

            //Se for mesmos horarios mas está com estatus de ativação não dá erro de conflito de horário 
            if (ocupado && !agendamento.IsDisponivel)
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
