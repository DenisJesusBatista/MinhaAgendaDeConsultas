# Sobre o projeto MinhaAgendaDeConsultas

Este projeto é uma API implementada em .NET Core que foi construída utilizando a arquitetura Domain Drive Design.

# Features

- Registrar usuarios;
- Fazer login por e-mail e senha;

# Requisitos
  
* Visual Studio 2022
* PostGres 16.2-1 ou mais

  # Instalação
  
## Pacotes ( Plugins )

### Projeto InfraEstrutura
  * Dapper
  * NpgSql (7.0.6)
  * NpgSql.EntityFrameworkCore
  * PostgresSql
  * Fluent MIgrator
  * FluentMigrator.Runner.Postgres
  * Microsoft.AspNetCore.Http.Abstractions
  * Microsoft.Extensions.Logging
  * System.identitymodel.tokens.jwt\8.3.1\

### Projeto API 
  * AutoMapper.Extensions.Microsoft.DependencyInjection
  * ConfigurationManager

### Projeto Application
  * AutoMapper
  * FluentValidation

# Configuração
* Ao realizar a instalação do postgres será necessário definir o usuário e senha, conforme arquivo `appsettings.Development.json` na propriedade `ConnectionStrings`.
* Foram configurados os EndPoints `Login` na LoginController.

## Configuração de Injeção de Dependência
* Configuração feita na `Program.cs`

* Os registros da injeção de dependência para os useCase estão sendo feitas na classe `Bootstrapper.cs`, conforme imagem abaixo:
* Registro de injeção de dependência para o banco de dados e repositório.

## Criação de tabela

* Criando a tabela no banco com as colunas e ColunasPadrões, arquivo `Versao0000001` que se encontra em: MinhaAgendaDeContatos.Infraestrutura\Migrations\Versoes
*  Migrations que faz a chamada UP da criação da tabela acima.

  
  
