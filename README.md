# Projeto de Funções Azure para Manipulação de Filmes

Este projeto contém três funções Azure que permitem a manipulação de dados de filmes, incluindo armazenamento de dados no Cosmos DB, armazenamento de arquivos no Azure Blob Storage e recuperação de detalhes de filmes do Cosmos DB.

## Tecnologias Utilizadas

- .NET 8
- C# 12.0
- Azure Functions
- Azure Cosmos DB
- Azure Blob Storage
- Newtonsoft.Json

## Estrutura do Projeto

### 1. `fnPostDatabase`

Esta função é responsável por salvar os dados de um filme no Cosmos DB.

- **Endpoint:** `POST /api/movie`
- **Descrição:** Recebe uma requisição HTTP com os dados do filme em formato JSON e os salva no Cosmos DB.
- **Arquivo:** `FunctionMovieDatabase.cs`

### 2. `fnPostDataStorage`

Esta função é responsável por armazenar arquivos (imagens) no Azure Blob Storage.

- **Endpoint:** `POST /api/dataStorage`
- **Descrição:** Recebe uma requisição HTTP com um arquivo e o armazena no Azure Blob Storage.
- **Arquivo:** `FunctionDataStorage.cs`

### 3. `fnGetMovieDetail`

Esta função é responsável por recuperar os detalhes de um filme armazenado no Cosmos DB.

- **Endpoint:** `GET /api/detail`
- **Descrição:** Recebe uma requisição HTTP com o ID do filme e retorna os detalhes do filme armazenado no Cosmos DB.
- **Arquivo:** `FunctionMovieDetail.cs`

## Configuração

### Variáveis de Ambiente

- `CosmosDBConnection`: String de conexão para o Azure Cosmos DB.
- `AzureWebJobsStorage`: String de conexão para o Azure Blob Storage.
- `DatabaseName`: Nome do banco de dados no Cosmos DB.

### Dependências

- `Microsoft.Azure.Functions.Worker`
- `Microsoft.Extensions.Logging`
- `Microsoft.Azure.Cosmos`
- `Azure.Storage.Blobs`
- `Newtonsoft.Json`

## Como Executar

1. Clone o repositório.
2. Configure as variáveis de ambiente necessárias.
3. Compile o projeto utilizando o Visual Studio 2022 ou o .NET CLI.
4. Execute as funções utilizando o Azure Functions Core Tools ou publique-as no Azure.