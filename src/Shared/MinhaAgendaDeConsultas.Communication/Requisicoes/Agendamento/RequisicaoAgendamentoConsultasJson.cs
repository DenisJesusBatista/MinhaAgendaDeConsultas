namespace MinhaAgendaDeConsultas.Communication.Requisicoes
{
    public class RequisicaoAgendamentoConsultasJson
    {
        public string PacienteEmail { get; set; }
        public string MedicoEmail { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }


    }


}
