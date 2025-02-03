using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinhaAgendaDeConsultas.Application.UseCases;
using MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Alterar;
using MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Consultar;
using MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Excluir;
using MinhaAgendaDeConsultas.Application.UseCases.AgendamentoConsultas.Registrar;
using MinhaAgendaDeConsultas.Application.UseCases.Login.FazerLogin;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Profile;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Medico;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Paciente;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Usuario;

namespace MinhaAgendaDeConsultas.Application
{
    public static class Bootstrapper
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AdicionarUseCases(services);

        }

        

        /*Registrar nas configurações de dependência.*/
        private static void AdicionarUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegistrarUsuarioUseCase, RegistrarUsuarioUseCase>();
            services.AddScoped<IFazerLoginUseCase, FazerLoginUseCase>();
            services.AddScoped<IRegistrarPacienteUseCase, RegistrarPacienteUseCase>();
            services.AddScoped<IRegistrarMedicoUseCase, RegistrarMedicoUseCase>();
            services.AddScoped<IObterUsuarioProfileUseCase, ObterUsuarioProfileUseCase>();
            services.AddScoped<IRegistrarAgendamentoConsultasUseCase, RegistrarAgendamentoConsultasUseCase>();
            services.AddScoped<IConsultarAgendamentoConsultasUseCase, ConsultarAgendamentoConsultasUseCase>();
            services.AddScoped<IAlterarConsultaAgendamentosUseCase, AlterarConsultaAgendamentoUseCase>();
            services.AddScoped<IExcluirConsultaAgendamentoUseCase, ExcluirConsultaAgendamentoUseCase>();

        }
    }
}
