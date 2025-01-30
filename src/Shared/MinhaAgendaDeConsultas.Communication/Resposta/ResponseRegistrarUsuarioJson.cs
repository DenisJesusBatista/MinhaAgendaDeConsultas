using MinhaAgendaDeConsultas.Communication.Resposta;

namespace MinhaAgendaDeConsultas.Communication.Responses
{
    public class ResponseRegistrarUsuarioJson
    {
        public string Nome { get; set; } = string.Empty;
        public RespostaTokenJson Tokens { get; set; } = default!;
    }
}
