# Target Sistemas - Desafios Técnicos

API REST desenvolvida em .NET 8 que implementa as soluções para os 5 desafios técnicos propostos pela Target Sistemas.

## Requisitos

- .NET 8.0 SDK
- Visual Studio 2022 ou VS Code

## Como Executar

1. Clone o repositório
2. Abra o projeto no Visual Studio ou VS Code
3. Execute o projeto (F5 no Visual Studio ou `dotnet run` no terminal)
4. Acesse o Swagger UI em `https://localhost:7xxx/swagger` (a porta pode variar)

## Endpoints Implementados

### 1. Soma Sequencial (/ProcessoSeletivo/1)
- **Método**: GET
- **Descrição**: Calcula a soma dos números de 1 até 13
- **Resposta**: Retorna o valor 91 (soma de 1 a 13)

### 2. Fibonacci (/ProcessoSeletivo/2/{numero})
- **Método**: GET
- **Parâmetro**: número inteiro positivo
- **Descrição**: Verifica se um número pertence à sequência de Fibonacci
- **Resposta**: Indica se o número pertence ou não à sequência

### 3. Estatísticas de Faturamento (/ProcessoSeletivo/3)
- **Método**: POST
- **Corpo**: JSON ou XML com dados de faturamento diário
- **Descrição**: Calcula estatísticas do faturamento mensal
- **Resposta**: Retorna menor valor, maior valor e quantidade de dias acima da média
- **Formato do corpo da requisição**:
```json
{
  "rows": [
    {
      "dia": 1,
      "valor": 1000.00
    }
  ]
}
```

### 4. Percentual por Estado (/ProcessoSeletivo/4)
- **Método**: GET
- **Descrição**: Calcula o percentual de representação do faturamento por estado
- **Resposta**: Retorna dicionário com os percentuais de cada estado

### 5. Inverter String (/ProcessoSeletivo/5/{texto})
- **Método**: GET
- **Parâmetro**: texto a ser invertido
- **Descrição**: Inverte os caracteres de uma string sem usar funções prontas
- **Resposta**: Retorna o texto original e sua versão invertida

## Implementação

- Projeto desenvolvido em ASP.NET Core Web API
- Documentação via Swagger/OpenAPI
- Suporte a JSON e XML para o exercício 3
- Tratamento de erros e validações básicas
- Comentários XML para documentação da API
