# CriptoTrabalhoFinalInfraestrutura

API para monitoramento de dados do mercado de criptoativos, com integração à API da Binance para informações em tempo real.

## 👥 Integrantes
- Augusto Klug, Lucas Parisotto, Thiago Jung, Thyago Floriano, Lauro Pereira Neto Schautica, André Victor Duarte Zeni


## 🛠️ Stack Utilizada
- .NET 10 (C#), Entity Framework Core, SQL Server
- Docker & Docker Compose
- GitHub Actions (CI/CD)
- xUnit / Moq

## 🚀 Funcionalidades
- **Monitoramento de Preços:** Preço atual de pares de ativos (ex: BTCUSDT).
- **Histórico de Negociações:** Últimos trades executados no mercado.
- **Preços de Referência:** Cotações para análise.
- **Log de Operações:** Persistência interna para auditoria.

## 🏗️ Arquitetura
1. **Controllers:** Porta de entrada e validação.
2. **Services:** Lógica de negócio e orquestração.
3. **Integração (Binance):** Comunicação com a API externa.
4. **Repositories:** Acesso a dados via EF Core.

## 🛠️ Configuração e Execução

### Variáveis de Ambiente
Copie o `.env.example` para `.env` e defina sua senha de banco de dados (`DB_PASSWORD`).
> **Atenção:** Mantenha suas credenciais seguras utilizando variáveis de ambiente. O arquivo `.env` está no `.gitignore`.

### Rodando com Docker (Recomendado)
```bash
docker compose up --build
```
A API estará disponível em `http://localhost:8080/scalar/v1`.

### Rodando Local (Sem Docker)
Certifique-se de ter um SQL Server rodando, ajuste a `ConnectionStrings__DefaultConnection` e execute:
```bash
dotnet build
dotnet run
```

## 🧪 Testes Unitários
Para rodar os testes automatizados:
```bash
dotnet test
```

## 🔄 CI/CD
A pipeline do GitHub Actions valida automaticamente a aplicação. Lembre-se de configurar o secret `DB_CONNECTION_STRING` no repositório.

---

## 📋 Relatório

### 3. O que acontece se um teste falhar propositalmente?

Para validar o comportamento da pipeline de CI/CD diante de falhas, foi realizado um teste através do **Pull Request #9** (`TJ:develop: Teste para CI barrar MR`)

**O que foi feito:**
Foi introduzido um erro de sintaxe proposital no arquivo `Controllers/LogsController.cs` quebrando a compilação do projeto.

**O que aconteceu:**
Ao abrir o PR, a pipeline `CI/CD Pipeline` foi disparada automaticamente (run #12). O job `build-and-test` falhou na compilação detectados pelo `dotnet build`:

- `Identifier expected`
- `Syntax error, ',' expected`
- `Process completed with exit code 1`

O PR ficou com o check da pipeline marcado como **falho**, sinalizando claramente que o código não está apto para merge. O merge pôde ser bloqueado pela proteção de branch configurada, impedindo que código quebrado chegasse à branch principal.


#### 4. Por que nunca devemos commitar credenciais no código?

Commitar senhas, strings de conexão ou chaves de API no repositório representa um risco grave de segurança pelos seguintes motivos:

- **O histórico do Git é permanente:** mesmo que a credencial seja removida em um commit posterior, ela continua acessível via `git log` ou ferramentas de busca em histórico.
- **Repositórios públicos expõem instantaneamente:** bots varrem o GitHub continuamente em busca de credenciais expostas. Uma chave vazada pode ser explorada em minutos.
- **Repositórios privados também oferecem risco:** qualquer pessoa com acesso ao repositório passa a ter acesso às credenciais de produção, mesmo que não precise delas..

A solução adotada neste projeto — variáveis de ambiente localmente via `.env` e GitHub Secrets na pipeline.

#### 5. Em que cenário real isso seria útil?

- **Entrega entre times:** o time de infraestrutura pode baixar o binário gerado pelo
time de desenvolvimento e fazer o deploy manualmente em um servidor, sem precisar buildar
o projeto localmente nem ter acesso ao código-fonte.

- **Rastreabilidade de versões:** é possível associar exatamente qual binário foi gerado
a partir de qual commit, facilitando auditorias e rollbacks. Se um bug aparecer em
produção, basta identificar o run correspondente e baixar o artefato daquele momento.

- **Ambientes sem acesso ao repositório:** servidores de produção frequentemente não
têm acesso ao código-fonte por questões de segurança. O artefato publicado resolve
esse problema, entregando apenas o necessário para executar a aplicação.