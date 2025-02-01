using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinhaAgendaDeConsultas.Application.UseCases;
using MinhaAgendaDeConsultas.Application.UseCases.Login.FazerLogin;
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

        }
    }
}
