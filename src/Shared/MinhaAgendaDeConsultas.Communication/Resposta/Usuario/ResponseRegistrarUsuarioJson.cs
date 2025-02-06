using MinhaAgendaDeConsultas.Communication.Resposta.Token;

namespace MinhaAgendaDeConsultas.Communication.Resposta.Usuario
{
    public class ResponseRegistrarUsuarioJson
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; }
        public RespostaTokenJson Tokens { get; set; }
       
    }

    public class RespostaTokenJson
    {
        public string AcessoToken { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }

}
