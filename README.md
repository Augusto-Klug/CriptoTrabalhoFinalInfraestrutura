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
2. No arquivo `.env`, defina a variável `DB_PASSWORD` e `DOCKER_IMAGE`.

---

## 🛠️ Guia de Execução

### Opção 1: Com Docker (Recomendado)
A maneira mais rápida de subir o ambiente completo (API + SQL Server):

1. Defina a variável de ambiente:
   - `$env:DB_PASSWORD="SuaSenha"`
   - `$env:DOCKER_IMAGE=thiagojm23/trabalho-cripto-final-infraestrutura:latest`
- Também pode ser criado um arquivo .env dentro da pasta raiz e colocar as variáveis lá dentro.
- **Se não quiser clonar o repositório interiro  pode apenas copiar o arquivo docker-compose.yaml e colar em alguma pasta vazia no seu PC, criar o .env lá dentro e executar o comando abaixo:**
2. Execute o comando:
   ```bash
   docker compose up --build
   ```
A API estará disponível em `http://localhost:8080/scalar/v1`.

---
