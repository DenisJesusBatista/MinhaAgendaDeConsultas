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

![image](https://private-user-images.githubusercontent.com/52789764/320707774-4cacdb4e-10dd-475c-baf3-a7b356be78f8.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3MzgyMDEwMTksIm5iZiI6MTczODIwMDcxOSwicGF0aCI6Ii81Mjc4OTc2NC8zMjA3MDc3NzQtNGNhY2RiNGUtMTBkZC00NzVjLWJhZjMtYTdiMzU2YmU3OGY4LnBuZz9YLUFtei1BbGdvcml0aG09QVdTNC1ITUFDLVNIQTI1NiZYLUFtei1DcmVkZW50aWFsPUFLSUFWQ09EWUxTQTUzUFFLNFpBJTJGMjAyNTAxMzAlMkZ1cy1lYXN0LTElMkZzMyUyRmF3czRfcmVxdWVzdCZYLUFtei1EYXRlPTIwMjUwMTMwVDAxMzE1OVomWC1BbXotRXhwaXJlcz0zMDAmWC1BbXotU2lnbmF0dXJlPWU2NWUwNWQxZmM4Y2M1YTk2MDU2ZGFmMTE2MzlhZTNjNjE0ODc5MzNjNzQwNjY4MDhiOTk3OTFlYjBlNTQ5NDQmWC1BbXotU2lnbmVkSGVhZGVycz1ob3N0In0._vWyzvCEm-bzDPQtCo8XorixL1tx0mORFpCqp6cmlRE)

* Os registros da injeção de dependência para os useCase estão sendo feitas na classe `Bootstrapper.cs`, conforme imagem abaixo:
  
Imagem pendente
  
* Registro de injeção de dependência para o banco de dados e repositório.

## Criação de tabela

* Criando a tabela no banco com as colunas e ColunasPadrões, arquivo `Versao0000001` que se encontra em: MinhaAgendaDeContatos.Infraestrutura\Migrations\Versoes
*  Migrations que faz a chamada UP da criação da tabela acima.

  
  
