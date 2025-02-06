namespace MinhaAgendaDeConsultas.Communication.Resposta.Token
{
    public class RespostaTokenJson
    {
        public string AcessoToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string MensagemErro { get; set; } = string.Empty;
    }
}
