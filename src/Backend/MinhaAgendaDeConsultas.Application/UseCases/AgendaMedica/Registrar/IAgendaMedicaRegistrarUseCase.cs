﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Agendamento;
using MinhaAgendaDeConsultas.Communication.Resposta.Agendamento;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Registrar
{
    public interface IAgendaMedicaRegistrarUseCase
    {
        public Task<ResponseAgendaMedicaResult> Executar(RequisicaoAgendaMedicaJson agendaMedica);
    }
}
