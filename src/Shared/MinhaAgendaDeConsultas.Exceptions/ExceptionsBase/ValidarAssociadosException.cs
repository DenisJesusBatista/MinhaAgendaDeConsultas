using FluentValidation.Results;
using MinhaAgendaDeConsultas.Domain.Enumeradores;


namespace MinhaAgendaDeConsultas.Exceptions.ExceptionsBase
{
    public static class ValidarAssociadosException
    {
        public static void ValidarUsuarioAssociado(bool usuarioExiste, TipoUsuario tipoUsuario, List<ValidationFailure> errors)
        {
            if (!usuarioExiste)
            {
                var mensagemErro = tipoUsuario == TipoUsuario.Medico
                    ? "Usuário não encontrado. Registre um usuário antes de criar um médico."
                    : "Usuário não encontrado. Registre um usuário antes de criar um paciente.";

                errors.Add(new ValidationFailure("cpf", mensagemErro));
            }
        }
    }
}
