# LogSentinel

LogSentinel is a cross‑platform security monitoring agent designed to collect logs, detect anomalies, and send real‑time alerts through multiple channels (Discord, Slack, Telegram, Email).  
Built with .NET 8 and designed for Windows, Linux, and future mobile/desktop dashboards.

---

## 🚀 Features

- Cross‑platform log collection (Windows Event Logs + Linux journalctl)
- Real‑time alert engine with rule‑based triggers
- Multi‑channel alerting:
  - Discord
  - Slack
  - Telegram
  - Email
- SQLite storage for logs and rules
- Background collector service
- REST API with Swagger UI
- Modular architecture (Agent, Desktop, Mobile)

---

## 📦 Installation

### Windows
dotnet publish -c Release -r win-x64 --self-contained false -o publish/win


### Linux
dotnet publish -c Release -r linux-x64 --self-contained false -o publish/linux


### ARM (optional)
dotnet publish -c Release -r linux-arm64 --self-contained false -o publish/arm


Download builds from **GitHub Releases**.

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


---

## 🧭 Roadmap

See **ROADMAP.md** for full future plans.

---

## 📄 License

MIT — see **LICENSE** for details.

