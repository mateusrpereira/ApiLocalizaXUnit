# Projeto Localiza
É uma API que permite o registro e controle de CEPs nos municípios para o correto endereçamento das localizações (casas, empresas etc.). A solução foi desenvolvida utilizando a linguagem C# na versão 7.0 do .NET Framework e Banco de Dados MySQL.

## Requisitos:
Disponibilizar endpoints para inclusão, alteração, busca geral/busca por id e exclusão de Usuários para que possam efetuar login na aplicação.

Disponibiliar endpoints de Login para que o usuário cadastrado possa realizar autenticação utilizando seu e-mail e token gerado para autorização na utilização dos recursos disponíveis na aplicação.

Disponibilizar endpoints para listar as Unidades Federativas (UFs), inclusive pelo seu código.

Disponiblizar endpoints para inclusão, alteração, exclusão de Municípios em suas respectivas UFs.
Os endpoints de buscas por Municípios devem ocorrer pelo seu código (ID) de forma simplificada e completa, como também pelo código do IBGE.

Disponiblizar endpoints para inclusão, alteração, exclusão e busca por id e pelo CEP.

## Critérios de aceite:
O usuário deverá informar seu nome com tamanho máximo de 60 cacacteres e um e-mail em formato válido, contendo no máximo 100 caracteres.

Para efetuar login, o usuário deverá informar seu e-mail cadastrado, após isso, deverá informar a palavra Bearer seguido do Token gerado no botão de autorização.

Ao se criar um município, deverá ser informado um nome com no máximo 60 caracteres e o código da UF.

Para cadastro de um novo CEP, deverá ser informado o valor do CEP, o logradouro e o identificador único do município.

O CEP não será validado em um cadastro prévio no momento inicial, mas a funcionalidade possivelmente existirá numa versão futura.

## Execução:
Abra a solução (Api.sln), preferencialmente, na versão 2022 ou posterior do Microsoft Visual Studio
Restaure os pacotes dos projetos
Altere a string de conexão (connectionString) da base de dados (projeto_api\src\Api.Data\Context\ContextFactory.cs) e (projeto_api\src\Api.CrossCutting\DependencyInjection\ConfigureRepository.cs)
Rode o projeto

Caso utilize o VSCode para rodar o projeto e se depare com a mensagem:
"Não foi possível localizar um projeto para executar. Verifique se existe um projeto em C:\projeto_api\src ou passe o caminho para o projeto usando --project."
Utilize o comando conforme exemplo abaixo: 
dotnet run --project C:\projeto_api\src\Api.Application

Ou navegue até Api.Application e após isso execute o comando:
dotnet run

## Banco de dados:
Utilizar o Entity Framework (pasta Migrations no projeto Api.Data)
Exemplo:
    * dotnet ef migrations add ApiMigration
    * dotnet ef database update

Obs.: Caso tenha algum retorno de mensagem conforme abaixo:
Não foi possível executar porque o comando ou o arquivo especificado não foi encontrado.
Possíveis motivos para isso incluem:
  * Você digitou incorretamente um comando de dotnet interno.
  * Você pretendia executar um programa .NET, mas dotnet-ef não existe.
  * Você pretendia executar uma ferramenta global, mas não foi possível encontrar um executável com prefixo de dotnet com esse nome no CAMINHO.
 
Execute: 
dotnet tool install --global dotnet-ef


