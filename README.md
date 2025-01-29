# valida-cpf-azure-function
# Azure Function para Validação de CPF

Este projeto implementa uma Azure Function em C# para validar números de CPF (Cadastro de Pessoas Físicas) do Brasil. A função é acionada via HTTP e retorna se um CPF fornecido é válido ou não.

## Pré-requisitos

- Conta no [Azure](https://azure.microsoft.com/)
- [Azure Functions Core Tools](https://docs.microsoft.com/azure/azure-functions/functions-run-local) instalado
- [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/) com a extensão do Azure Functions

## Configuração do Projeto

1. **Crie um novo projeto Azure Functions:**

   ```bash
   func init MyFunctionApp --dotnet
