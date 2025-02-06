namespace MinhaAgendaDeConsultas.Communication.Resposta.Usuario
{
    public class RespostaUsuarioProfileJson
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public RespostaTokenJson Tokens { get; set; }
    }
}
