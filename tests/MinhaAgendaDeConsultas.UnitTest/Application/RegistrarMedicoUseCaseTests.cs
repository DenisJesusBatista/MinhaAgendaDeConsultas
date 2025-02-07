using AutoMapper;
using MinhaAgendaDeConsultas.Domain.Repositorios.Medico;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Medico;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Medico;
using AutoBogus;
using MinhaAgendaDeConsultas.Domain.Entidades;
using FluentAssertions;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;
using MinhaAgendaDeConsultas.Domain.Seguranca.Token;

namespace MinhaAgendaDeConsultas.UnitTest.Application
{
    public class RegistrarMedicoUseCaseTests
    {
        private readonly Mock<IMedicoReadOnlyRepositorio> _medicoReadOnlyRepositorio;
        private readonly Mock<IMedicoWriteOnlyRepositorio> _medicoWriteOnlyRepositorio;
        private readonly Mock<IUsuarioReadOnlyRepositorio> _usuarioReadOnlyRepositorio;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IUnidadeDeTrabalho> _unidadeDeTrabalho;
        private readonly Mock<IGeradorTokenAcesso> _geradorTokenAcesso;
        private readonly RegistrarMedicoUseCase _registrarMedicoUseCase;

        public RegistrarMedicoUseCaseTests()
        {
            _medicoReadOnlyRepositorio = new();
            _medicoWriteOnlyRepositorio = new();
            _usuarioReadOnlyRepositorio = new();
            _mapper = new();
            _unidadeDeTrabalho = new();
            _geradorTokenAcesso = new Mock<IGeradorTokenAcesso>();
            _registrarMedicoUseCase = new(_medicoReadOnlyRepositorio.Object, _mapper.Object, _unidadeDeTrabalho.Object, _medicoWriteOnlyRepositorio.Object, _usuarioReadOnlyRepositorio.Object, _geradorTokenAcesso.Object);
        }

        [Fact]
        public async Task Should_Return_ResponseRegistrarMedicoJson()
        {
            //Arrange
            var request = new AutoFaker<RequisicaoRegistrarMedicoJson>().Generate();

            var userRepositoryResponse = new AutoFaker<Usuario>().Generate();

            var mapperResult = new AutoFaker<Medico>().RuleFor(x => x.Nome, request.Nome).Generate();

            _medicoReadOnlyRepositorio.Setup(x => x.ExisteMedicoComCpf(It.IsAny<string>())).ReturnsAsync(false);
            _medicoReadOnlyRepositorio.Setup(x => x.ExisteMedicoComCrm(It.IsAny<string>())).ReturnsAsync(false);
            _medicoReadOnlyRepositorio.Setup(x => x.ExisteMedicoUsuarioComEmail(It.IsAny<string>())).ReturnsAsync(true);

            _usuarioReadOnlyRepositorio.Setup(x => x.RecuperarPorEmail(It.IsAny<string>())).ReturnsAsync(userRepositoryResponse);

            _mapper.Setup(x => x.Map<Medico>(It.IsAny<RequisicaoRegistrarMedicoJson>())).Returns(mapperResult);

            //Act
            var result = await _registrarMedicoUseCase.Executar(request);

            //Assert
            result.Nome.Should().Be(request.Nome);
        }

        [Fact]
        public async Task Should_Throw_When_Request_Cpf_Is_Null()
        {
            //Arrange
            var request = new AutoFaker<RequisicaoRegistrarMedicoJson>().RuleFor(x => x.Cpf, string.Empty).Generate();

            //Act
            var act = () => _registrarMedicoUseCase.Executar(request);

            //Assert
            await act.Should().ThrowAsync<ErrosDeValidacaoException>();
        }

        [Fact]
        public async Task Should_Throw_When_Request_Crm_Is_Null()
        {
            //Arrange
            var request = new AutoFaker<RequisicaoRegistrarMedicoJson>().RuleFor(x => x.Crm, string.Empty).Generate();

            //Act
            var act = () => _registrarMedicoUseCase.Executar(request);

            //Assert
            await act.Should().ThrowAsync<ErrosDeValidacaoException>();
        }

        [Fact]
        public async Task Should_Throw_When_Request_Nome_Is_Null()
        {
            //Arrange
            var request = new AutoFaker<RequisicaoRegistrarMedicoJson>().RuleFor(x => x.Nome, string.Empty).Generate();

            //Act
            var act = () => _registrarMedicoUseCase.Executar(request);

            //Assert
            await act.Should().ThrowAsync<ErrosDeValidacaoException>();
        }
    }
}
