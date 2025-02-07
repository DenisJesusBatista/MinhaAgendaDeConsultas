using AutoMapper;
using MinhaAgendaDeConsultas.Domain.Repositorios.Paciente;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MinhaAgendaDeConsultas.Application.UseCases;
using AutoBogus;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Medico;
using MinhaAgendaDeConsultas.Domain.Entidades;
using MinhaAgendaDeConsultas.Domain.Repositorios.Medico;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Paciente;
using FluentAssertions;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;

namespace MinhaAgendaDeConsultas.UnitTest.Application
{
    public class RegistrarPacienteUseCaseTests
    {
        private readonly Mock<IPacienteReadOnlyRepositorio> _pacienteReadOnlyRepositorio;
        private readonly Mock<IPacienteWriteOnlyRepositorio> _pacienteWriteOnlyRepositorio;
        private readonly Mock<IUsuarioReadOnlyRepositorio> _usuarioReadOnlyRepositorio;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IUnidadeDeTrabalho> _unidadeDeTrabalho;
        private readonly RegistrarPacienteUseCase _registrarPacienteUseCase;
        public RegistrarPacienteUseCaseTests()
        {
            _pacienteReadOnlyRepositorio = new();
            _pacienteWriteOnlyRepositorio = new();
            _unidadeDeTrabalho = new();
            _mapper = new();
            _usuarioReadOnlyRepositorio = new();
            _registrarPacienteUseCase = new(_pacienteReadOnlyRepositorio.Object, _mapper.Object, _unidadeDeTrabalho.Object, _pacienteWriteOnlyRepositorio.Object, _usuarioReadOnlyRepositorio.Object);
        }

        [Fact]
        public async Task Should_Return_ResponseRegistrarPacienteJson()
        {
            //Arrange
            var request = new AutoFaker<RequisicaoRegistrarPacienteJson>().Generate();
            var userRepositoryResponse = new AutoFaker<Usuario>().Generate();

            var mapperResult = new AutoFaker<Paciente>().RuleFor(x => x.Nome, request.Nome).Generate();

            _pacienteReadOnlyRepositorio.Setup(x => x.ExistePacienteComCpf(It.IsAny<string>())).ReturnsAsync(false);
            _pacienteReadOnlyRepositorio.Setup(x => x.ExistePacienteUsuarioComEmail(It.IsAny<string>())).ReturnsAsync(true);

            _usuarioReadOnlyRepositorio.Setup(x => x.RecuperarPorEmail(It.IsAny<string>())).ReturnsAsync(userRepositoryResponse);

            _mapper.Setup(x => x.Map<Paciente>(It.IsAny<RequisicaoRegistrarPacienteJson>())).Returns(mapperResult);

            //Act
            var result = await _registrarPacienteUseCase.Executar(request);

            //Assert
            result.Nome.Should().Be(request.Nome);
        }

        [Fact]
        public async Task Should_Throw_When_Request_Cpf_Is_Null()
        {
            //Arrange
            var request = new AutoFaker<RequisicaoRegistrarPacienteJson>().RuleFor(x => x.Cpf, string.Empty).Generate();

            //Act
            var act = () => _registrarPacienteUseCase.Executar(request);

            //Assert
            await act.Should().ThrowAsync<ErrosDeValidacaoException>();
        }

        [Fact]
        public async Task Should_Throw_When_Request_Nome_Is_Null()
        {
            //Arrange
            var request = new AutoFaker<RequisicaoRegistrarPacienteJson>().RuleFor(x => x.Nome, string.Empty).Generate();

            //Act
            var act = () => _registrarPacienteUseCase.Executar(request);

            //Assert
            await act.Should().ThrowAsync<ErrosDeValidacaoException>();
        }
    }
}
