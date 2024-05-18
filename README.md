# Projeto Localiza

� uma API que permite o registro e controle de CEPs nos munic�pios para o correto endere�amento das localiza��es (casas, empresas etc.). A solu��o foi desenvolvida utilizando a linguagem C# na vers�o 7.0 do .NET Framework e Banco de Dados SQL Server e MySQL.

## Requisitos:

Disponibilizar endpoints para inclus�o, altera��o, busca geral/busca por id e exclus�o de Usu�rios para que possam efetuar login na aplica��o.

Disponibiliar endpoints de Login para que o usu�rio cadastrado possa realizar autentica��o utilizando seu e-mail e token gerado para autoriza��o na utiliza��o dos recursos dispon�veis na aplica��o.

Disponibilizar endpoints para listar as Unidades Federativas (UFs), inclusive pelo seu c�digo.

Disponiblizar endpoints para inclus�o, altera��o, exclus�o de Munic�pios em suas respectivas UFs.
Os endpoints de buscas por Munic�pios devem ocorrer pelo seu c�digo (ID) de forma simplificada e completa, como tamb�m pelo c�digo do IBGE.

Disponiblizar endpoints para inclus�o, altera��o, exclus�o e busca por id e pelo CEP para Ceps.

## Crit�rios de aceite:

O usu�rio dever� informar seu nome com tamanho m�ximo de 60 cacacteres e um e-mail em formato v�lido, contendo no m�ximo 100 caracteres.

Para efetuar login, o usu�rio dever� informar seu e-mail cadastrado, ap�s isso, dever� informar a palavra Bearer seguido do Token gerado no bot�o de autoriza��o.

Ao se criar um munic�pio, dever� ser informado um nome com no m�ximo 60 caracteres e o c�digo da UF.

Para cadastro de um novo CEP, dever� ser informado o valor do CEP, o logradouro e o identificador �nico do munic�pio.

O CEP n�o ser� validado em um cadastro pr�vio no momento inicial, mas a funcionalidade possivelmente existir� numa vers�o futura.

## Execu��o:

Abra a pasta do projeto, preferencialmente, utilizando o VSCode
Restaure os pacotes dos projetos
Altere a DB_CONNECTION da base de dados.vscode\launch.json de acordo com sua preferência de banco de dados: SQL Server ou MySQL
Altere a string de conex�o (connectionString) da base de dados (projeto_api\src\Api.Data\Context\ContextFactory.cs)
Rode o projeto

Caso utilize o VSCode para rodar o projeto e se depare com a mensagem:
"N�o foi poss�vel localizar um projeto para executar. Verifique se existe um projeto em C:\projeto_api\src ou passe o caminho para o projeto usando --project."
Utilize o comando conforme exemplo abaixo:
dotnet run --project C:\projeto_api\src\Api.Application

Ou navegue at� Api.Application e ap�s isso execute o comando:
dotnet run

## Execução dos Testes Unitários (XUnit):
Data (4 testes):
Navegue até o menu Terminal -> Run Task -> Data.Testes -> Continue without scanning the task output

Service (23 testes):
Navegue até o menu Terminal -> Run Task -> Service.Testes -> Continue without scanning the task output

Aplication (44 testes):
Navegue até o menu Terminal -> Run Task -> Aplication.Testes -> Continue without scanning the task output

Integration (4 testes):
Navegue até o menu Terminal -> Run Task -> Integration.Testes -> Continue without scanning the task output

## Banco de dados:

Utilizar o Entity Framework (pasta Migrations no projeto Api.Data)
Exemplo:
_ dotnet ef migrations add ApiMigration
_ dotnet ef database update

Obs.: Caso tenha algum retorno de mensagem conforme abaixo:
N�o foi poss�vel executar porque o comando ou o arquivo especificado n�o foi encontrado.
Poss�veis motivos para isso incluem:

- Voc� digitou incorretamente um comando de dotnet interno.
- Voc� pretendia executar um programa .NET, mas dotnet-ef n�o existe.
- Voc� pretendia executar uma ferramenta global, mas n�o foi poss�vel encontrar um execut�vel com prefixo de dotnet com esse nome no CAMINHO.

Execute:
dotnet tool install --global dotnet-ef
