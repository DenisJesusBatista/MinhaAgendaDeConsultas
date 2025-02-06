namespace MinhaAgendaDeConsultas.Domain.Entidades;
public class RefreshToken: EntidadeBase
{
    public required string Value { get; set; } = string.Empty;   
    public required long UsuarioId { get; set; }
    public required DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public required DateTime DataExpiracao { get; set; } = DateTime.UtcNow.AddMonths(6);    
    public Usuario Usuario { get; set; } = default!;
}   

