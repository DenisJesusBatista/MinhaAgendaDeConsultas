namespace MinhaAgendaDeConsultas.Communication.Requisicoes.Agendamento
{
    public class RequisicaoAlteracaoAgendaMedicaJson
    {
        public string MedicoEmail { get; set; }
        public DateTime DataInicioAtual { get; set; }
        public DateTime DataFimAtual { get; set; }
        public DateTime DataInicioNova { get; set; }
        public DateTime DataFimNova { get; set; }
        public bool IsDisponivel { get; set; }
    }
}
