using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Communication.Resposta;
public class RespostaSucessoJson
{
    // Propriedade para armazenar mensagens de sucesso
    public List<string> Mensagens { get; set; }

    // Construtor para inicializar a lista de mensagens
    public RespostaSucessoJson(string mensagem)
    {
        Mensagens = new List<string> { mensagem };
    }

    // Método adicional para adicionar mais mensagens, caso necessário
    public void AdicionarMensagem(string mensagem)
    {
        Mensagens.Add(mensagem);
    }
}

