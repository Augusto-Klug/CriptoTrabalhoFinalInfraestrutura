# Infraestrutura local

Esta pasta sobe a API e o SQL Server Developer Edition prontos para uso.

## Subir tudo

```bash
docker compose -f infraestrutura/docker-compose.yml up --build
```

## Portas

- SQL Server no host: `11433`
- API no host: `8080`

## Credenciais do SQL Server

- Usuário: `sa`
- Senha: `CriptoTrabalhoFinal@2026`

## Observações

- A `connectionString` da aplicação está hardcoded em `appsettings*.json` para o cenário local.
- No cenário containerizado, a `docker-compose.yml` sobrescreve a mesma `connectionString` apenas para apontar a API ao serviço `sqlserver`.
- As migrations são aplicadas automaticamente no startup da API, com tentativas de retry até o SQL Server ficar disponível.
