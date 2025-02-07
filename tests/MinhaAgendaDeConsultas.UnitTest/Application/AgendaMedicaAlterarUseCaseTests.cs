using AutoMapper;
using MinhaAgendaDeConsultas.Domain.Repositorios.Agendamento;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Alterar;
using AutoBogus;
using MinhaAgendaDeConsultas.Domain.Entidades;
using Bogus;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Agendamento;
using FluentAssertions;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;

namespace MinhaAgendaDeConsultas.UnitTest.Application
{
    public class AgendaMedicaAlterarUseCaseTests
    {
        private readonly Mock<IAgendaMedicaUpdateOnlyRepository> _agendaMedicaUpdateOnlyRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IUnidadeDeTrabalho> _unidadeDeTrabalho;
        private readonly Mock<IAgendaMedicaConsultaOnlyRepository> _agendaMedicaConsultaOnlyRepository;
        private readonly Mock<IUsuarioReadOnlyRepositorio> _usuarioReadOnlyRepositorio;
        private readonly AgendaMedicaAlterarUseCase _agendaMedicaAlterarUseCase;

        public AgendaMedicaAlterarUseCaseTests()
        {
            _agendaMedicaUpdateOnlyRepository = new();
            _unidadeDeTrabalho = new();
            _mapper = new();
            _agendaMedicaConsultaOnlyRepository = new();
            _usuarioReadOnlyRepositorio = new();
            _agendaMedicaAlterarUseCase = new(_agendaMedicaUpdateOnlyRepository.Object, _agendaMedicaConsultaOnlyRepository.Object, _usuarioReadOnlyRepositorio.Object, _mapper.Object, _unidadeDeTrabalho.Object);
        }

        [Fact]
        public async Task Should_Return_Success_Result_When_Request_Succeeds()
        {
            //Arrange
            var request = new AutoFaker<RequisicaoAlteracaoAgendaMedicaJson>().Generate();

            var usuario = new AutoFaker<Usuario>().Generate();

            var agendas = new AutoFaker<AgendaMedica>().Generate(new Faker().Random.Int(1, 10));

            var entity = new AutoFaker<AgendaMedica>().Generate();

            _usuarioReadOnlyRepositorio.Setup(x => x.RecuperarPorEmail(It.IsAny<string>())).ReturnsAsync(usuario);

            _agendaMedicaConsultaOnlyRepository
                .Setup(x => x.VerificarDisponibilidade(It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(false);

            _agendaMedicaConsultaOnlyRepository
                .Setup(x => x.ObterAgendasMedicias(It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(agendas);

            _mapper.Setup(x => x.Map<AgendaMedica>(It.IsAny<RequisicaoAlteracaoAgendaMedicaJson>())).Returns(entity);

            //Act
            var result = await _agendaMedicaAlterarUseCase.Executar(request);

            //Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Agendamento alterado com sucesso");
        }

        [Fact]
        public async Task Should_Throw_When_Email_Is_Empty()
        {
            //Arrange
            var request = new AutoFaker<RequisicaoAlteracaoAgendaMedicaJson>().RuleFor(x => x.MedicoEmail, string.Empty).Generate();

            var usuario = new AutoFaker<Usuario>().Generate();

            _agendaMedicaConsultaOnlyRepository
                .Setup(x => x.VerificarDisponibilidade(It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(false);

            _usuarioReadOnlyRepositorio.Setup(x => x.RecuperarPorEmail(It.IsAny<string>())).ReturnsAsync(usuario);

            //Act
            var act = () => _agendaMedicaAlterarUseCase.Executar(request);

            //Assert
            await act.Should().ThrowAsync<ErrosDeValidacaoException>();
        }

    }
}
