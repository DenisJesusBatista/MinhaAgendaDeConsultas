using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Domain.Entidades
{
    public class Usuario : EntidadeBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Definindo o tamanho máximo para a senha usando Data Annotations
        [MaxLength(128)] // Defina o tamanho para o hash SHA512
        public string Senha { get; set; } = string.Empty;

    }
}
