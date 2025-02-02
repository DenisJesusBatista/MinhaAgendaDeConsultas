namespace MinhaAgendaDeConsultas.Communication.Responses
{
    public class RespostaErroJson
    {
        public List<string> Mensagens { get; set; }
        public string Mensagem { get; set; }
        public bool TokenIsExpired { get; set; }
        public string Detalhes { get; set; }  // Adicionando a propriedade 'Detalhes'


        public RespostaErroJson(string mensagem)
        {
            Mensagens = new List<string>
            {
                mensagem
            };
        }
        

        public RespostaErroJson(List<string> mensagem)
        {
            Mensagens = mensagem;

        }
    }
}
