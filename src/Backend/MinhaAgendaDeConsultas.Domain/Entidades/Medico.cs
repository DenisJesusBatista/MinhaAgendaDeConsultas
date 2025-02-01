namespace MinhaAgendaDeConsultas.Domain.Entidades
{
    public class Medico : Usuario
    {
        public string Cpf { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;
        public string? Nome { get; set; }
    }
}
