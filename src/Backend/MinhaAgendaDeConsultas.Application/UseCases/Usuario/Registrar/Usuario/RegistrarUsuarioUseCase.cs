﻿using AutoMapper;
using MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar.Usuario;
using MinhaAgendaDeConsultas.Communication.Requisicoes.Usuario;
using MinhaAgendaDeConsultas.Communication.Resposta.Usuario;
using MinhaAgendaDeConsultas.Domain;
using MinhaAgendaDeConsultas.Domain.Enumeradores;
using MinhaAgendaDeConsultas.Domain.Repositorios;
using MinhaAgendaDeConsultas.Domain.Repositorios.Usuario;
using MinhaAgendaDeConsultas.Domain.Seguranca.Criptografia;
using MinhaAgendaDeConsultas.Domain.Seguranca.Token;
using MinhaAgendaDeConsultas.Exceptions;
using MinhaAgendaDeConsultas.Exceptions.ExceptionsBase;
using RespostaTokenJson = MinhaAgendaDeConsultas.Communication.Resposta.Usuario.RespostaTokenJson;

namespace MinhaAgendaDeConsultas.Application.UseCases.Usuario.Registrar
{
    public class RegistrarUsuarioUseCase : IRegistrarUsuarioUseCase
    {
        //Variavel readonly só pode ser atribuido valor nela, apenas no construtor da classe ( public RegistrarContatoUseCase(IContatoWriteOnlyRepositorio repositorio) )
        private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;
        private readonly IUsuarioWriteOnlyRepositorio _usuarioWriteOnlyRepositorio;
        private readonly IMapper _mapper;
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IGeradorTokenAcesso _geradorTokenAcesso;

        //Configurar a injeção de dependência atalho CTOR - Criar 
        //Construtor
        public RegistrarUsuarioUseCase(IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio, IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho,
              IUsuarioWriteOnlyRepositorio usuarioWriteOnlyRepositorio, IPasswordEncripter passwordEncripter, IGeradorTokenAcesso geradorTokenAcesso)
        {
            _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
            _mapper = mapper;
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _usuarioWriteOnlyRepositorio = usuarioWriteOnlyRepositorio;
            _passwordEncripter = passwordEncripter;
            _geradorTokenAcesso = geradorTokenAcesso;
        }


        public async Task<ResponseRegistrarUsuarioJson> Executar(RequisicaoRegistrarUsuarioJson requisicao)
        {
            await Validar(requisicao);

            //Conversão requisicao para entidade AutoMap
            //-Pluggin: AutoMapper na Application
            //-Pluggin: AutoMapper.Extensions.Microsoft.DependencyInjection na API para configurar para funcionar como injecao de dependencia

            var entidade = _mapper.Map<Domain.Entidades.Usuario>(requisicao);

            entidade.Tipo = TipoUsuario.Usuario;

            entidade.Cpf = "00000000000".ToString();

            // Criptografa a senha antes de salvar
            entidade.Senha = _passwordEncripter.Encrypt(requisicao.Senha);

            Guid identificadorGuid = Guid.NewGuid();

            entidade.Identificador = identificadorGuid;

            entidade.IdentificadorString = identificadorGuid.ToString();

            var acessoTokens = new RespostaTokenJson
            {
                AcessoToken = _geradorTokenAcesso.Gerar(identificadorGuid.ToString(), entidade.Email),
            };

            entidade.Token = acessoTokens.AcessoToken;


            await _usuarioWriteOnlyRepositorio.Adicionar(entidade);

            //Salvar no banco de dados.
            await _unidadeDeTrabalho.Commit();


            return new ResponseRegistrarUsuarioJson
            {   
                Nome = entidade.Nome,
                Email = entidade.Email,
                Tokens = acessoTokens                
            };
        }

        private async Task Validar(RequisicaoRegistrarUsuarioJson requisicao)
        {
            var validator = new RegistrarUsuarioValidator();
            var resultado = validator.Validate(requisicao);

            var existeContatoComEmail = await _usuarioReadOnlyRepositorio.ExisteUsuarioComEmail(requisicao.Email);

            if (existeContatoComEmail)
            {
                resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessagesExceptions.EMAIL_JA_REGISTRADO));
            }

            if (!resultado.IsValid)
            {
                var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrosDeValidacaoException(mensagensDeErro);
            }
        }
    }
}
