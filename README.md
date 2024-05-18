# Projeto Localiza

É uma API que permite o registro e controle de CEPs nos municípios para o correto endereçamento das localizações (casas, empresas etc.). A solução foi desenvolvida utilizando a linguagem C# na versão 7.0 do .NET Framework e Banco de Dados SQL Server e MySQL.

## Requisitos:

Disponibilizar endpoints para inclusão, alteração, busca geral/busca por id e exclusão de Usuários para que possam efetuar login na aplicação.

Disponibilizar endpoints de Login para que o usuário cadastrado possa realizar autenticação utilizando seu e-mail e token gerado para autorização na utilização dos recursos disponíveis na aplicação.

Disponibilizar endpoints para listar as Unidades Federativas (UFs), inclusive pelo seu código.

Disponibilizar endpoints para inclusão, alteração, exclusão de Municípios em suas respectivas UFs.
Os endpoints de buscas por Municípios devem ocorrer pelo seu código (ID) de forma simplificada e completa, como também pelo código do IBGE.

Disponibilizar endpoints para inclusão, alteração, exclusão e busca por id e pelo CEP para Ceps.

## Critérios de aceite:

O usuário deverá informar seu nome com tamanho máximo de 60 caracteres e um e-mail em formato válido, contendo no máximo 100 caracteres.

Para efetuar login, o usuário deverá informar seu e-mail cadastrado no endpoint Login, após isso, clicar no botão Authorize da Api e deverá informar a palavra Bearer seguido do Token gerado no botão de autorização, conforme exemplo abaixo:

```
{
  "email": "email@email.com"
}
```

```
Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6WyJwZXJlaXJhLm1hdGV1c3JhbW9zQGdtYWlsLmNvbSIsInBlcmVpcmEubWF0ZXVzcmFtb3NAZ21haWwuY29tIl0sImp0aSI6IjE5ZjJkNjFkLTc3N2UtNGRmNC1iYTJiLTZmOTJiZDUxN2RhMSIsIm5iZiI6MTcxNjA0OTE1OCwiZXhwIjoxNzE2MDc3OTU4LCJpYXQiOjE3MTYwNDkxNTgsImlzcyI6IkV4ZW1wbG9Jc3N1ZXIiLCJhdWQiOiJFeGVtcGxvQXVkaWVuY2UifQ.TbtbsohzHf594xq_-Y_XWabVXnMoop3lGgQZl86fsI-93hpnPcJX0O2Wah8uE6pGkQMAYaTWltaJ95Tin6OnUIyr4quUqJ9xlD6SL3ujAqjy6D5Ug0zMtLf-9MdpnQoW3K52MEW1uHgaA9MRQDTxmUehXg_iH7gtenj5tFNiapMPyfWEvOEXZLqwtg3qHySUeKCAu7b3gA1zb9EZZarFqE4NV1yWy0Oehi-4CFgPQGrIrK-lTZ3baCRPOQKuJlyynq_8Xp2I85TtQOeVKrcT86WC2ED4O8w2-j2z0xC4kGhXROk0jvDe2V4xfHrcPj-u9H3QZt5hC1B9vKvsylp1rw
```

Ao se criar um município, deverá ser informado um nome com no máximo 60 caracteres e o código da UF.

Para cadastro de um novo CEP, deverá ser informado o valor do CEP, o logradouro e o identificador único do município.

O CEP não será validado em um cadastro prévio no momento inicial, mas a funcionalidade possivelmente existirá numa versão futura.

## Execução:

Abra a pasta do projeto, preferencialmente, utilizando o VSCode

Restaure os pacotes dos projetos

Altere a DB_CONNECTION da base de dados .vscode\launch.json de acordo com sua preferência de banco de dados: SQL Server ou MySQL

Altere a string de conexão (connectionString) da base de dados (projeto_api\src\Api.Data\Context\ContextFactory.cs)

Rode o projeto

Caso utilize o VSCode para rodar o projeto e se depare com a mensagem:

"Não foi possível localizar um projeto para executar. Verifique se existe um projeto em C:\projeto_api\src ou passe o caminho para o projeto usando --project."

Utilize o comando conforme exemplo abaixo:

```
dotnet run --project C:\projeto_api\src\Api.Application
```

Ou navegue até Api.Application e após isso execute o comando:

```
dotnet run
```

## Execução dos Testes Unitários (XUnit):

Data (4 testes):

Navegue até o menu do VSCode:

Terminal -> Run Task -> Data.Testes -> Continue without scanning the task output

Service (23 testes):

Navegue até o menu do VSCode:

Terminal -> Run Task -> Service.Testes -> Continue without scanning the task output

Application (44 testes):

Navegue até o menu do VSCode:

Terminal -> Run Task -> Application.Testes -> Continue without scanning the task output

Integration (4 testes):

Navegue até o menu do VSCode:

Terminal -> Run Task -> Integration.Testes -> Continue without scanning the task output

## Banco de dados:

Utilizar o Entity Framework (pasta Migrations no projeto Api.Data)
Exemplo:

```
dotnet ef migrations add ApiMigration

dotnet ef database update
```

Obs.: Caso tenha algum retorno de mensagem conforme abaixo:
Não foi possível executar porque o comando ou o arquivo especificado não foi encontrado.
Possíveis motivos para isso incluem:

- Você digitou incorretamente um comando de dotnet interno.
- Você pretendia executar um programa .NET, mas dotnet-ef não existe.
- Você pretendia executar uma ferramenta global, mas não foi possível encontrar um executável com prefixo de dotnet com esse nome no CAMINHO.

Execute:

```
dotnet tool install --global dotnet-ef
```
