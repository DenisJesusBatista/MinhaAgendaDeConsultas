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
using MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Consultar;
using AutoBogus;
using MinhaAgendaDeConsultas.Domain.Entidades;
using Bogus;
using FluentAssertions;
using MinhaAgendaDeConsultas.Communication.Resposta.Agendamento;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;

namespace MinhaAgendaDeConsultas.UnitTest.Application
{
    public class AgendaMedicaConsultarUseCaseTests
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IUnidadeDeTrabalho> _unidadeDeTrabalho;
        private readonly Mock<IAgendaMedicaConsultaOnlyRepository> _agendaMedicaConsultaOnlyRepository;
        private readonly Mock<IUsuarioReadOnlyRepositorio> _usuarioReadOnlyRepositorio;
        private readonly AgendaMedicaConsultarUseCase _agendaMedicaConsultarUseCase;

        public AgendaMedicaConsultarUseCaseTests()
        {
            _mapper = new();
            _unidadeDeTrabalho = new();
            _agendaMedicaConsultaOnlyRepository = new();
            _usuarioReadOnlyRepositorio = new();
            _agendaMedicaConsultarUseCase = new(_mapper.Object, _unidadeDeTrabalho.Object, _usuarioReadOnlyRepositorio.Object, _agendaMedicaConsultaOnlyRepository.Object);
        }

        [Fact]
        public async Task Should_Return_IList_ResponseAgendaMedica_When_Email_Exists()
        {
            //Arrange
            var usuario = new AutoFaker<Usuario>().Generate();

            var appointments = new AutoFaker<AgendaMedica>().Generate(new Faker().Random.Int(1, 10));

            _usuarioReadOnlyRepositorio.Setup(x => x.RecuperarPorEmail(It.IsAny<string>())).ReturnsAsync(usuario);

            _agendaMedicaConsultaOnlyRepository
                .Setup(x => x.ObterAgendasMedicias(It.IsAny<long>(),It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(appointments);

            //Act
            var result = await _agendaMedicaConsultarUseCase.ObterAgendasMedicias(DateTime.Now, DateTime.Now, string.Empty);

            //Assert
            result.Should().BeOfType<List<ResponseAgendaMedica>>();
            result.Should().NotBeNull();
            result.Count.Should().Be(appointments.Count);
        }

        [Fact]
        public async Task Should_Throw_When_Email_Not_Found()
        {
            //Arrange

            //Act
            var act = () => _agendaMedicaConsultarUseCase.ObterAgendasMedicias(DateTime.Now, DateTime.Now, string.Empty);

            //Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Médico não encontrado");
        }

        [Fact]
        public async Task Should_Return_Empty_List_When_No_Appointments_Exist()
        {
            //Arrange
            var usuario = new AutoFaker<Usuario>().Generate();

            var appointments = new AutoFaker<AgendaMedica>().Generate(0);

            _usuarioReadOnlyRepositorio.Setup(x => x.RecuperarPorEmail(It.IsAny<string>())).ReturnsAsync(usuario);

            _agendaMedicaConsultaOnlyRepository
                .Setup(x => x.ObterAgendasMedicias(It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(appointments);

            //Act
            var result = await _agendaMedicaConsultarUseCase.ObterAgendasMedicias(DateTime.Now, DateTime.Now, string.Empty);

            //Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(appointments.Count);
        }
    }
}
