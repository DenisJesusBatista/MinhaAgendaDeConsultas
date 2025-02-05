﻿using FluentValidation;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Agendamento;

namespace MinhaAgendaDeConsultas.Application.UseCases.AgendaMedica.Validadores
{
    internal class AgendaMedicaValidacao :AbstractValidator<RequisicaoAgendaMedicaJson>
    {
        public AgendaMedicaValidacao()
        {
            RuleFor(x => x.MedicoEmail).NotEmpty().WithMessage("O email do médico é obrigatório.");
            RuleFor(x => x.DataPretendidaInicio).NotEmpty().WithMessage("A data de início é obrigatória.");
            RuleFor(x => x.DataPretendidaFim).NotEmpty().WithMessage("A data de fim é obrigatória.");
        }
    }
}
