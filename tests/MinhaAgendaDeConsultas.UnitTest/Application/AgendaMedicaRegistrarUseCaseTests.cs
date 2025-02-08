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
using MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Registrar;
using AutoBogus;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Agendamento;
using MinhaAgendaDeConsultas.Domain.Entidades;
using FluentAssertions;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;

namespace MinhaAgendaDeConsultas.UnitTest.Application
{
    public class AgendaMedicaRegistrarUseCaseTests
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IUnidadeDeTrabalho> _unidadeDeTrabalho;
        private readonly Mock<IAgendaMedicaWriteOnlyRepository> _agendaMedicaWriteOnlyRepository;
        private readonly Mock<IAgendaMedicaConsultaOnlyRepository> _agendaMedicaConsultaOnlyRepository;
        private readonly Mock<IUsuarioReadOnlyRepositorio> _usuarioReadOnlyRepositorio;
        private readonly AgendaMedicaRegistrarUseCase _agendaMedicaRegistrarUseCase;
        public AgendaMedicaRegistrarUseCaseTests()
        {
            _mapper = new();
            _unidadeDeTrabalho = new();
            _agendaMedicaWriteOnlyRepository = new();
            _agendaMedicaConsultaOnlyRepository = new();
            _usuarioReadOnlyRepositorio = new();
            _agendaMedicaRegistrarUseCase = new(_mapper.Object, _unidadeDeTrabalho.Object, _agendaMedicaWriteOnlyRepository.Object, _agendaMedicaConsultaOnlyRepository.Object, _usuarioReadOnlyRepositorio.Object);
        }

        [Fact]
        public async Task Should_Return_Correct_Object_When_Request_Succeeds()
        {
            //Arrange
            var request = new AutoFaker<RequisicaoAgendaMedicaJson>().Generate();
            var mapperResult = new AutoFaker<AgendaMedica>().Generate();
            var userResult = new AutoFaker<Usuario>().Generate();

            _mapper.Setup(x => x.Map<AgendaMedica>(It.IsAny<RequisicaoAgendaMedicaJson>())).Returns(mapperResult);

            _usuarioReadOnlyRepositorio.Setup(x => x.RecuperarPorEmail(It.IsAny<string>())).ReturnsAsync(userResult);

            //Act
            var result = await _agendaMedicaRegistrarUseCase.Executar(request);

            //Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Agendamento realizado com sucesso");
        }

        [Fact]
        public async Task Should_Throw_When_Email_Not_Filled()
        {
            //Arrange
            var request = new AutoFaker<RequisicaoAgendaMedicaJson>().RuleFor(x => x.MedicoEmail, string.Empty).Generate();

            //Act
            var action = () => _agendaMedicaRegistrarUseCase.Executar(request);

            //Assert
            action.Should().ThrowAsync<ErrosDeValidacaoException>();
        }

    }
}
