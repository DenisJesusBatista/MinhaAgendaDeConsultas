using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Consultar;
using AutoBogus;
using MinhaAgendaDeConsultas.Domain.Entidades;
using FluentAssertions;
using MinhaAgendaDeConsultas.Communication.Resposta.Agendamento;

namespace MinhaAgendaDeConsultas.UnitTest.Application
{
    public class ConsultarAgendamentoUseCaseTests
    {
        private readonly Mock<IAgendamentoConsultasConsultasOnlyRepositorio> _agendamentoConsultas;
        private readonly Mock<IUsuarioReadOnlyRepositorio> _usuarioRepository;
        private readonly ConsultarAgendamentoConsultasUseCase _usuarioConsultasUseCase;

        public ConsultarAgendamentoUseCaseTests()
        {
            _agendamentoConsultas = new();
            _usuarioRepository = new();
            _usuarioConsultasUseCase = new(_agendamentoConsultas.Object, _usuarioRepository.Object);
        }

        [Fact]
        public async Task GetAgendamentosMedico_Should_Return_On_Success()
        {
            //Arrange
            var usuario = new AutoFaker<Usuario>().Generate();
            var agendamentos = new AutoFaker<AgendamentoConsultas>().Generate(10);

            _usuarioRepository.Setup(x => x.RecuperarPorEmail(It.IsAny<string>())).ReturnsAsync(usuario);
            _agendamentoConsultas.Setup(x => x.GetAgendamentosMedico(It.IsAny<long>())).ReturnsAsync(agendamentos);

            //Act
            var result = await _usuarioConsultasUseCase.GetAgendamentosMedico(usuario.Email);

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeOfType<List<ResponseConsultaAgendamentos>>();
        }
    }
}
