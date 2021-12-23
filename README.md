# AspNetCoreEnterpriseApp


## Description

Projeto desenvolvido para aplicar os conhecimentos aprendidos no curso ASP.NET Core Enterprise Applications do desenvolvedor.io.

## Usage

### Running RabbitMQ

À partir diretório ```\AspNetCoreEnterpriseApp``` devemos subir o docker-compose ```docker-compose.yml``` executando o comando ```docker-compose up -d```.

Dessa forma, ele subirá um container do RabbitMQ expondo a porta 5672 a qual será utilizada por nossos serviços, e a porta 15672 que será utilizada por nós mesmos para o gerenciamento das filas do RabbitMQ.