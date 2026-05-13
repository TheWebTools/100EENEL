# 100EENEL

100EENEL is a cross‑platform security log collection and alerting agent designed to monitor Windows and Linux systems in real time.  
It captures logs, parses them, stores them locally, and triggers alerts through multiple channels such as Discord, Slack, Telegram, and Email.

Built with .NET 8 and engineered for reliability, extensibility, and future cloud integration.

---

## 🔥 Features

- **Cross‑platform log collection**
  - Windows Event Logs
  - Linux journalctl
- **Real‑time alert engine**
  - Rule‑based triggers
  - Multi‑channel alerting
- **Supported alert channels**
  - Discord
  - Slack
  - Telegram
  - Email
- **SQLite‑backed storage**
  - Log entries
  - Alert rules
  - Alert history
- **REST API with Swagger UI**
- **Modular architecture**
  - Agent
  - Desktop UI (future)
  - Mobile UI (future)

---

## 📦 Installation

### Windows
dotnet publish -c Release -r win-x64 --self-contained false -o publish/win


### Linux
dotnet publish -c Release -r linux-x64 --self-contained false -o publish/linux


### ARM (optional)
dotnet publish -c Release -r linux-arm64 --self-contained false -o publish/arm

---

## ⚙️ Configuration

Alert rules are stored in SQLite and can be managed through the API.

Each rule includes:

- `Name`
- `Severity`
- `ContainsText`
- `Channel`
- `Target`
- `Enabled`

---

## 📡 API & Swagger

Start the agent and open:
http://localhost:5000/swagger


You can test log queries, alert rule management, and system endpoints directly from the UI.

---

## 🧭 Roadmap

See **ROADMAP.md** for the full development plan.

Key upcoming milestones:

- Desktop dashboard (MAUI)
- Mobile dashboard (MAUI)
- Cloud monitoring backend
- Multi‑agent management
- Trend analytics
- Webhooks + Teams integration

---

## 🛠️ Development

### Restore dependencies
dotnet restore


### Build
dotnet Build


### Run
dotnet run --project 100EENEL.Agent


---

## 📝 License

MIT License — see **LICENSE** for details.

---

## 🤝 Contributing

Pull requests are welcome.  
For major changes, please open an issue first to discuss what you’d like to change.

---

## ⭐ Support the Project

If you find 100EENEL useful, consider starring the repository to help others discover it.


