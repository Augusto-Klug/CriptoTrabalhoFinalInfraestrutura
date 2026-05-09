# CriptoTrabalhoFinalInfraestrutura

Esta API é uma solução robusta para monitoramento de dados do mercado de criptoativos, integrando-se diretamente com a API da Binance para fornecer informações em tempo real sobre preços, trades recentes e cotações de referência.

## 🚀 O que a API faz?

- **Monitoramento de Preços:** Consulta o preço atual de qualquer par de ativos (ex: BTCUSDT).
- **Histórico de Negociações:** Recupera os trades mais recentes executados no mercado.
- **Preços de Referência:** Fornece cotações de referência para análise de mercado.
- **Log de Operações:** Sistema interno de persistência para auditoria de consultas realizadas.

---

## 🏗️ Arquitetura e Camadas

O projeto segue os princípios de separação de responsabilidades para facilitar a manutenção e testabilidade:

1. **Controllers:** Porta de entrada da API. Validam as requisições (`ModelState`) e gerenciam os retornos HTTP.
2. **Services:** Contêm a lógica de negócio e orquestram a comunicação entre a integração externa e o repositório.
3. **Integracao (Binance):** Camada de infraestrutura responsável pela comunicação direta com a API externa via `HttpClient`.
4. **Repositories:** Camada de acesso a dados (Entity Framework Core) para persistência dos logs.
5. **DTOs/Entities:** Objetos de transferência de dados e entidades do banco de dados.

---

## 🔐 Segurança e Configurações Sensíveis

### Por que nunca devemos commitar credenciais?
O commit de senhas, chaves de API ou strings de conexão no código-fonte expõe o sistema a ataques graves. Uma vez no histórico do Git, a informação é difícil de remover totalmente. Por isso, este projeto utiliza:
- **Variáveis de Ambiente:** Para configurações dinâmicas e sensíveis.
- **GitHub Secrets:** No fluxo de CI/CD para proteger credenciais em ambientes de automação.
- **.gitignore:** Configurado para nunca subir arquivos `.env`.

### Configuração das Variáveis de Ambiente

1. Copie o arquivo `.env.example` para um novo arquivo chamado `.env`:
   ```bash
   cp .env.example .env
   ```
2. No arquivo `.env`, defina a variável `DB_PASSWORD`.

---

## 🛠️ Guia de Execução

### Opção 1: Com Docker (Recomendado)
A maneira mais rápida de subir o ambiente completo (API + SQL Server):

1. Defina a variável de ambiente:
   - **Windows (PS):** `$env:DB_PASSWORD="SuaSenha"`
   - **Linux/macOS:** `export DB_PASSWORD="SuaSenha"`
2. Execute o comando:
   ```bash
   docker compose up --build
   ```
A API estará disponível em `http://localhost:8080/scalar/v1`.

### Opção 2: Sem Docker (Local)
Para rodar apenas a API localmente:

1. Tenha um SQL Server disponível.
2. Configure a string de conexão no `appsettings.json` ou via variável de ambiente `ConnectionStrings__DefaultConnection`.
3. Execute:
   ```bash
   dotnet build
   dotnet run
   ```

---

## 🧪 Testes Unitários

O projeto possui uma suíte de 10 testes unitários automatizados cobrindo Controllers e Services. 
Para executá-los:
```bash
dotnet test
```

---

## 🔄 CI/CD e Secrets

A pipeline de CI/CD (`cicd.yaml`) automatiza o build e os testes. Ela demonstra o uso seguro de secrets referenciando `${{ secrets.DB_CONNECTION_STRING }}`. 

> Para que a pipeline complete com sucesso em um ambiente real, o secret `DB_CONNECTION_STRING` deve ser cadastrado nas configurações do repositório no GitHub.
