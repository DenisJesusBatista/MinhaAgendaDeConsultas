﻿
namespace MinhaAgendaDeConsultas.Domain.Seguranca.Token
{
    public interface IGeradorTokenAcesso
    {
        public string Gerar(string identificadorUsuario, string email, string? tipoUsuario = null);
    }
}
