﻿using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;

namespace MinhaAgendaDeConsultas.Communication.Resposta.Paciente
{
    public class ResponseRegistrarPacienteJson
    {
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public RespostaTokenJson Tokens { get; set; }
    }
  

}
