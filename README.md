# GitHub Commit Bot 🤖

Bot de Telegram desenvolvido em C# que permite realizar commits em um repositório do GitHub através de mensagens de texto enviadas pelo usuário.

## 📋 Sobre o projeto

O bot recebe mensagens do usuário via Telegram e as transforma em commits automáticos em um arquivo (`commit.txt`) dentro de um repositório do GitHub configurado previamente por cada usuário. As configurações de cada usuário (repositório, branch, token de acesso) são armazenadas em um banco de dados PostgreSQL.

## ⚙️ Como funciona

1. O usuário inicia a conversa com o bot enviando `/start`.
2. Configura seus dados com o comando:
   ```
   /config <usuário do github> <repositório> <branch> <token de acesso>
   ```
3. A partir daí, qualquer mensagem enviada ao bot é commitada automaticamente no arquivo `commit.txt` do repositório configurado.

## 🏗️ Arquitetura

O projeto é organizado em camadas, separando responsabilidades:

```
GithubCommitBot/
├── Program.cs                  # Ponto de entrada da aplicação
├── _bot/
│   └── TelegramBot.cs           # Comunicação com a API do Telegram
├── _messageHandler/
│   └── MessageHandler.cs        # Interpreta e roteia as mensagens do usuário
├── _database/
│   └── DataBase.cs               # Persistência de dados em PostgreSQL
├── _models/
│   └── UserConfig.cs             # Modelo de configuração do usuário
└── _services/
    ├── GitHubService.cs          # Integração com a API do GitHub (Octokit)
    └── GitHubServiceFactory.cs   # Factory responsável pela criação de GitHubService
```

## 🛠️ Tecnologias utilizadas

- **C# / .NET**
- **Telegram.Bot** — integração com a API do Telegram
- **Octokit** — integração com a API do GitHub
- **Npgsql** — driver de conexão com PostgreSQL
- **PostgreSQL** — banco de dados relacional

## 🔒 Segurança

- Credenciais sensíveis (token do bot e string de conexão com o banco) são carregadas via **variáveis de ambiente**, nunca hardcoded no código.
- Todas as queries ao banco de dados utilizam **parâmetros** (`$1, $2, ...`), prevenindo ataques de SQL Injection.

## 🚀 Como rodar o projeto

### Pré-requisitos
- .NET SDK instalado
- PostgreSQL instalado e rodando
- Um bot do Telegram criado via [BotFather](https://t.me/BotFather)

### Configuração do banco de dados

Crie a tabela necessária no PostgreSQL:

```sql
CREATE TABLE userconfig (
    id SERIAL PRIMARY KEY,
    telegram_chat_id BIGINT NOT NULL,
    github_user TEXT NOT NULL,
    user_repository TEXT NOT NULL,
    repository_branch TEXT NOT NULL,
    user_token TEXT NOT NULL
);
```

### Variáveis de ambiente

Configure as seguintes variáveis antes de rodar a aplicação:

```bash
export TELEGRAM_TOKEN="seu_token_do_bot_aqui"
export CONNECTION_STRING="Host=localhost;Username=postgres;Password=sua_senha;Database=GitHubCommitBot"
```

### Executando

```bash
dotnet run
```

## 📌 Roadmap

O projeto ainda está em desenvolvimento. Próximas implementações:

- [x] CREATE — configuração de usuário (`/config`)
- [x] READ — busca de configuração para realizar commits
- [ ] **UPDATE** — permitir que o usuário atualize sua configuração sem precisar recriá-la
- [ ] **DELETE** — permitir que o usuário remova sua configuração

## 📄 Licença

Este projeto foi desenvolvido para fins de estudo e portfólio.
