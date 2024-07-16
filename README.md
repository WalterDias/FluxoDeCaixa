# Fluxo de Caixa

Este projeto é uma aplicação de "Fluxo de Caixa" desenvolvida em .NET Core. A aplicação permite realizar operações de crédito e débito, além de consultar o saldo atual.

## Estrutura do Projeto

- **src/API**: Contém a API principal da aplicação.
- **src/Core**: Contém as principais lógicas de negócio e modelos.
- **tests/Benchmark**: Contém testes de benchmark utilizando BenchmarkDotNet.
- **tests/Unidade**: Contém testes unitários da aplicação.

## Configuração e Execução

### Pré-requisitos

- Docker
- Docker Compose

### Passos para Configuração

1. Clone este repositório para sua máquina local:
   ```bash
   git clone https://github.com/WalterDias/FluxoDeCaixa.git
   cd FluxoDeCaixa

2. Execute o comando para compilar e rodar a aplicação no docker:
   ```bash
   docker-compose up --build

3. Para executar a aplicação utilize o endereço:
   ```bash
   http://localhost:5002/swagger/index.html

### Obs.: Sobre o banco de dados 
Caso queira debugar a aplicação não esqueça de modificar a connection string ou utilize a conections string criada pelo docker compose

Atenção durante a utilização via swagger:


## Diagramas de Arquitetura
### Diagrama de contexto
![C4 Diagrama de Contexto](https://github.com/WalterDias/FluxoDeCaixa/blob/main/docs/c4.contexto.png?raw=true)

### Diagrama de container
![C4 Diagrama de Container](https://github.com/WalterDias/FluxoDeCaixa/blob/main/docs/c4.container.png?raw=true)

### Diagrama de componente
![C4 Diagrama de Componente](https://github.com/WalterDias/FluxoDeCaixa/blob/main/docs/c4.componentes.png?raw=true)
