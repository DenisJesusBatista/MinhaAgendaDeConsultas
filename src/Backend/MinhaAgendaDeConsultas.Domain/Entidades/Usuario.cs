using System.ComponentModel.DataAnnotations;
using MinhaAgendaDeConsultas.Domain.Enumeradores;

namespace MinhaAgendaDeConsultas.Domain.Entidades
{
    public class Usuario : EntidadeBase
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Definindo o tamanho máximo para a senha usando Data Annotations
        [MaxLength(128)] // Defina o tamanho para o hash SHA512
        public string Senha { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public TipoUsuario Tipo { get; set; } // Usando o Enum
        public Guid Identificador { get; set; }

        public string IdentificadorString { get; set; } = string.Empty;

        // Adicionando a propriedade para armazenar o token gerado
        public string Token { get; set; } = string.Empty;

    }
}
