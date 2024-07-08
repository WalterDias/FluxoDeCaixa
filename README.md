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

### Obs.: Sobre o banco de dados 
Caso queira debugar a aplicação não esqueça de modificar a connection string ou utilize a conections string criada pelo docker compose

