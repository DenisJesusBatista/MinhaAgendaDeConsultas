using AutoBogus;
using AutoMapper;
using Bogus;
using FluentAssertions;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Profile;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Entidades;
using MinhaAgendaDeConsultas.Domain.Servicos.UsuarioLogado;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;
using Moq;

namespace MinhaAgendaDeConsultas.UnitTest.Application
{
    public class ObterUsuarioProfiseUseCaseTests
    {
        private readonly Mock<IUsuarioLogado> _usuarioLogado;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IUsuarioReadOnlyRepositorio> _usuarioReadOnlyRepositorio;
        private readonly ObterUsuarioProfileUseCase _useCase;

        public ObterUsuarioProfiseUseCaseTests()
        {
            _usuarioLogado = new Mock<IUsuarioLogado>();
            _mapper = new Mock<IMapper>();
            _usuarioReadOnlyRepositorio = new Mock<IUsuarioReadOnlyRepositorio>();
            _useCase = new(_usuarioLogado.Object, _mapper.Object, _usuarioReadOnlyRepositorio.Object);
        }

        [Fact]
        public async Task Should_Return_User_When_Request_Succeeds()
        {
            //Arrange
            string email = new Faker().Random.String();

            Usuario user = new AutoFaker<Usuario>().RuleFor(x => x.Email, email).Generate();

            _usuarioReadOnlyRepositorio.Setup(x => x.ExisteUsuarioComEmail(It.IsAny<string>())).ReturnsAsync(true);

            _usuarioReadOnlyRepositorio.Setup(x => x.RecuperarPorEmail(It.IsAny<string>())).ReturnsAsync(user);

            //Act
            RespostaUsuarioProfileJson response = await _useCase.Executar(new RequisicaoObterUsuarioJson() { Email = email });

            //Assert
            response.Should().NotBeNull();
            response.Email.Should().Be(email);
            response.Nome.Should().Be(user.Nome);
        }

        [Fact]
        public async Task Should_Throw_When_Email_Is_NullOrEmpty()
        {
            //Arrange

            //Act
            Func<Task> request = async () => await _useCase.Executar(new RequisicaoObterUsuarioJson());

            //Assert
            await request.Should().ThrowAsync<ErrosDeValidacaoException>();
        }

    }
}
