# AspNetCoreEnterpriseApp


## Description

Projeto desenvolvido para aplicar os conhecimentos aprendidos no curso ASP.NET Core Enterprise Applications do desenvolvedor.io.

## Usage

### Running RabbitMQ

� partir diret�rio ```\AspNetCoreEnterpriseApp``` devemos subir o docker-compose ```docker-compose.yml``` executando o comando ```docker-compose up -d```.

Dessa forma, ele subir� um container do RabbitMQ expondo a porta 5672 a qual ser� utilizada por nossos servi�os, e a porta 15672 que ser� utilizada por n�s mesmos para o gerenciamento das filas do RabbitMQ.