# Sobre o projeto MinhaAgendaDeConsultas

Este projeto é uma API implementada em .NET Core que foi construída utilizando a arquitetura Domain Drive Design.

# Features

- Registrar usuarios;
- Registrar Medico
- Registrar Paciente
  
- Fazer login por e-mail e senha;

- Gerar Refresh Token
- Obter medico por especialidade

- Criar agenda medica
- Alterar agenda medica
- Excluir agenda medica
- Consultar agenda medica

- Criar agendamento
- Altera agendamento
- Excluir agendamento
- Cosultar agendamentos medico
- Consultar agendamentos paciente
  

# Requisitos
  
* Visual Studio 2022
* PostGres 16.2-1 ou mais

## Instalação
  
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

![image](https://github.com/user-attachments/assets/28cb8cc9-6c87-47ec-a77d-fc2b251b8e68)


* Os registros da injeção de dependência para os useCase estão sendo feitas na classe `Bootstrapper.cs`, conforme imagem abaixo:

![image](https://github.com/user-attachments/assets/cd57cde2-5b90-4139-af70-4021d51e1ba2)

 
* Registro de injeção de dependência para o banco de dados e repositório.

![image](https://github.com/user-attachments/assets/bd247f27-ca5a-487b-993d-47dabbfd4afc)


## Criação de tabela

* Criando a tabela no banco com as colunas e ColunasPadrões, arquivo `Versao0000001` que se encontra em: MinhaAgendaDeContatos.Infraestrutura\Migrations\Versoes

![image](https://github.com/user-attachments/assets/bd4155e3-c84f-40e1-aa19-005ef395a3a3)


* Criando tabelas no banco

![image](https://github.com/user-attachments/assets/55a8b967-78a6-48b2-b226-f3db45b4fbc3)
![image](https://github.com/user-attachments/assets/8dad3bd6-7c4b-41de-82b7-74d3689b4cbd)
  


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
## Regra de negocio

- No projeto MinhaAgendaDeConsultas.Application e na pasta UseCases consta toda a regra de negócio separadas por responsabilidade.

![image](https://github.com/user-attachments/assets/6985279d-fb51-4d56-9ccc-f3f260026803)


## Persistência dos dados

- Criado no projeto MinhaAgendaDeConsultas.Domain a pasta repositorio onde consta os arquivo que serão responsavéis em deletar, alterar, inserir e excluir.

![image](https://github.com/user-attachments/assets/fb355b4a-4285-44e5-8f93-daa016ed215a)

## Configuração da API

![image](https://github.com/user-attachments/assets/31bd6557-7105-45d4-a86d-eb95910bd750)


## Configuração do Swagger na API

  - O Swagger foi configurado no projeto para documentar e testar os endpoints de forma interativa. A configuração foi feita no `Program.cs` usando AddSwaggerGen, e inclui suporte para autenticação via JWT.
 
![image](https://github.com/user-attachments/assets/b2c58aa8-d056-4784-9a53-c40d5a7c947b)

## Testes Unitários

O projeto utiliza xUnit para a criação de testes unitários, garantindo a validação das regras de negócio de forma isolada. Além disso, utilizamos Moq para a criação de mocks e Bogus/AutoBogus para geração de dados fictícios nos testes.

#  Ferramentas Utilizadas

- xUnit → Framework de testes unitários para .NET.
- Moq → Biblioteca para mockar dependências.
- Bogus → Geração de dados aleatórios e realistas para os testes.
- FluentAssertions → Facilita a escrita de asserções mais legíveis e expressivas.

  Exemplo:

  ![image](https://github.com/user-attachments/assets/2e4f5c4c-b92e-4b86-b5f2-a421dc4f393b)





  
  
