using AutoMapper;
using MinhaAgendaDeConsultas.Domain.Repositorios.Agendamento;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Alterar;
using Moq;
using AutoBogus;
using MinhaAgendaDeConsultas.Communication.Requisicoes;

namespace MinhaAgendaDeConsultas.UnitTest.Application
{
    public class AlterarAgendamentoUseCaseTests
    {
        private readonly Mock<IAgendamentoConsultasConsultasOnlyRepositorio> _agendamentoConsultas;
        private readonly Mock<IAgendamentoConsultasUpdateOnlyRepositorio> _agendamentoConsultasUpdateOnlyRepositorio;
        private readonly Mock<IUsuarioReadOnlyRepositorio> _usuarioReadOnlyRepositorio;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IUnidadeDeTrabalho> _unidadeDeTrabalho;
        private readonly Mock<IAgendaMedicaConsultaOnlyRepository> _agendaMedicaConsultaOnlyRepository;
        private readonly AlterarConsultaAgendamentoUseCase _alterarConsulta;
        public AlterarAgendamentoUseCaseTests()
        {
            _agendamentoConsultas = new();
            _agendamentoConsultasUpdateOnlyRepositorio = new();
            _usuarioReadOnlyRepositorio = new();
            _mapper = new();
            _unidadeDeTrabalho = new();
            _agendaMedicaConsultaOnlyRepository = new();
            _alterarConsulta = new(_agendamentoConsultas.Object, _agendamentoConsultasUpdateOnlyRepositorio.Object, _usuarioReadOnlyRepositorio.Object, _agendaMedicaConsultaOnlyRepository.Object, _mapper.Object, _unidadeDeTrabalho.Object);

        }

        [Fact]
        public async Task Should_Return_Correct_Object_When_Succeeds()
        {
            //Arrange
            var request = new AutoFaker<RequisicaoAgendamentoConsultasJson>().Generate();

            //Act
            //Assert
        }
    }
}
