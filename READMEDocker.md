# Docker

Para subir a API junto com o SQL Server Developer Edition:

```bash
docker compose up --build
```

Portas expostas no host:

- API: `8080`
- SQL Server: `11433`

Credenciais do SQL Server:

- Usuário: `sa`
- Senha: Definida pela variável de ambiente `DB_PASSWORD`.

Para rodar via Docker Compose, você deve exportar a variável antes ou criar um arquivo `.env`:

```bash
# No Windows (PowerShell)
$env:DB_PASSWORD="SuaSenhaSegura"
docker compose up --build

# No Linux/macOS
export DB_PASSWORD="SuaSenhaSegura"
docker compose up --build
```

Observações:

- A `connectionString` em `appsettings*.json` atende a execução local da API fora do container.
- No `docker-compose.yml`, a API recebe uma sobrescrita da `connectionString` para falar com `sqlserver`, que é o nome do serviço dentro da rede Docker.
- As migrations são aplicadas automaticamente no startup da API.
