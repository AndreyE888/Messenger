# Messenger

Real-time chat application built with ASP.NET Core and SignalR.

## 🛠 Стек

- ASP.NET Core 8.0
- SignalR (WebSocket)
- SQLite + EF Core
- JWT + BCrypt

## 📁 Структура

```
Messenger/
├── Messenger.Domain/          # Сущности
├── Messenger.Application/     # Интерфейсы, DTO
├── Messenger.Infrastructure/  # DbContext, репозитории
├── Messenger.Server/          # API, SignalR
└── Messenger.Client.Web/      # HTML + JS
```

## 🚀 Запуск

### 1. Создайте `appsettings.Development.json` в `Messenger.Server`

```json
{
  "JwtSettings": {
    "SecretKey": "твой-секретный-ключ-32-символа"
  }
}
```

### 2. Накатите миграции

```bash
cd Messenger.Infrastructure
dotnet ef database update --startup-project ../Messenger.Server
```

### 3. Запустите сервер

```bash
cd ../Messenger.Server
dotnet run
```

### 4. Запустите клиент (новый терминал)

```bash
cd ../Messenger.Client.Web
dotnet run
```

### 5. Откройте браузер

```
http://localhost:5067
```

## 📡 API

| Метод | Endpoint | Что делает |
|-------|----------|------------|
| POST | `/api/auth/register` | Регистрация |
| POST | `/api/auth/login` | Логин (JWT) |
| POST | `/api/messages` | Отправить сообщение |
| GET | `/api/messages?chatId=1` | История чата |

## 🔌 SignalR

- **Hub:** `/chathub`
- **Метод:** `SendMessage(user, message)`
- **Событие:** `ReceiveMessage(user, message)`

## 📝 Лицензия

MIT
