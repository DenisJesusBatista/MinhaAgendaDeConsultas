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

* ![image](https://github.com/user-attachments/assets/28cb8cc9-6c87-47ec-a77d-fc2b251b8e68)


## Criação de tabela

* Criando a tabela no banco com as colunas e ColunasPadrões, arquivo `Versao0000001` que se encontra em: MinhaAgendaDeContatos.Infraestrutura\Migrations\Versoes

* ![image](https://github.com/user-attachments/assets/bd4155e3-c84f-40e1-aa19-005ef395a3a3)


* Criando tabelas no banco

* ![image](https://github.com/user-attachments/assets/55a8b967-78a6-48b2-b226-f3db45b4fbc3)
* ![image](https://github.com/user-attachments/assets/8dad3bd6-7c4b-41de-82b7-74d3689b4cbd)
  


*  Migrations que faz a chamada UP da criação da tabela acima.
  
  ## Migrações do Banco de Dados

Para adicionar ou alterar tabelas no banco de dados, siga os passos abaixo:

1. **Altere as Entidades**:
   - Modifique as classes no projeto para refletir as alterações desejadas.

2. **Crie uma Nova Migração**:
   - Execute o seguinte comando no Package Manager Console:
     ```bash
     Add-Migration NomeDaMigracao
     ```
   - Substitua `NomeDaMigracao` por um nome descritivo (por exemplo, `AddCpfColumnToMedicoAndPaciente`).

3. **Aplique a Migração**:
   - Execute o comando abaixo para aplicar as alterações ao banco de dados:
     ```bash
     Update-Database
     ```

4. **Verifique o Banco de Dados**:
   - Confirme que as alterações foram aplicadas corretamente.

  
  
