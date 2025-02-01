using MinhaAgendaDeConsultas.Communication.Resposta.Token;

namespace MinhaAgendaDeConsultas.Communication.Resposta.Usuario
{
    public class ResponseRegistrarUsuarioJson
    {
        public string Nome { get; set; } = string.Empty;
        public RespostaTokenJson Tokens { get; set; } = default!;
    }
}
