# 🧮 ASP.NET Core Accounting Showcase

> A production-ready accounting system built with ASP.NET Core 8, EF Core, and Clean Architecture.

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![EF Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://learn.microsoft.com/ef/core/)
[![SQLite](https://img.shields.io/badge/SQLite-003B57?style=for-the-badge&logo=sqlite&logoColor=white)](https://www.sqlite.org/)
[![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)](https://www.docker.com/)
[![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)](LICENSE)

---

## 📖 Overview

**ASP.NET Core Accounting Showcase** is a full-featured accounting system designed to demonstrate enterprise-grade architecture, Clean Architecture principles, and modern .NET practices.

This project showcases:
- 🏗️ **Clean Architecture** — Domain, Infrastructure, and API layers
- 💼 **Complete accounting model** — Chart of accounts, Invoices, Journal entries
- 📊 **Double-entry bookkeeping** — Balanced journal entries validation
- 🗄️ **Entity Framework Core 8** — Code-first with SQLite
- 🚀 **Minimal API** — Modern, fast endpoints with Swagger
- 🐳 **Docker-ready** — One-command setup
- 📦 **Seed data** — Standard chart of accounts included

---

## 📸 Screenshots

<table>
  <tr>
    <td width="50%" align="center">
      <strong>📋 Swagger API Documentation</strong><br>
      <em>Interactive API documentation with all endpoints</em><br><br>
      <img src="screenshots/Accounting.Api.png" alt="Swagger UI" width="100%">
    </td>
    <td width="50%" align="center">
      <strong>📊 Chart of Accounts</strong><br>
      <em>25 standard accounting accounts in Persian</em><br><br>
      <img src="screenshots/api_accounts.png" alt="Accounts API" width="100%">
    </td>
  </tr>
  <tr>
    <td width="50%" align="center">
      <strong>🐳 Docker Container</strong><br>
      <em>Application running in Docker</em><br><br>
      <img src="screenshots/docker.png" alt="Docker Desktop" width="100%">
    </td>
    <td width="50%" align="center">
      <strong>💻 Development in VS Code</strong><br>
      <em>Full development environment</em><br><br>
      <img src="screenshots/VSCode.png" alt="VS Code" width="100%">
    </td>
  </tr>
</table>

---

## 🏗️ Architecture

```
AccountingShowcase/
├── Accounting.Domain/              # Entities + Enums (core business)
│   ├── Entities/
│   │   ├── Account.cs             # حساب
│   │   ├── Invoice.cs             # فاکتور
│   │   ├── InvoiceItem.cs         # ردیف فاکتور
│   │   ├── JournalEntry.cs        # سند حسابداری
│   │   └── JournalLine.cs         # ردیف سند
│   └── Enums/
│       ├── AccountType.cs
│       ├── InvoiceStatus.cs
│       └── JournalEntryStatus.cs
│
├── Accounting.Infrastructure/     # Data access (EF Core)
│   ├── Data/
│   │   ├── AccountingDbContext.cs
│   │   └── DatabaseSeeder.cs
│   └── Migrations/
│
└── Accounting.Api/                # Web API (Minimal API)
    ├── DTOs/                      # Data Transfer Objects
    ├── Endpoints/                 # REST endpoints
    └── Program.cs
```

---

## ✨ Features

### 📊 Complete Accounting Model

| Entity | Description |
|--------|-------------|
| **Account** | Chart of accounts with hierarchical structure (Assets, Liabilities, Equity, Revenue, Expenses) |
| **Invoice** | Sales invoices with items, tax, and status tracking |
| **InvoiceItem** | Invoice line items with auto-calculation |
| **JournalEntry** | Double-entry accounting journal with balanced validation |
| **JournalLine** | Debit/Credit lines with account references |

### 🔐 Double-Entry Bookkeeping

Every journal entry is validated:
```
Total Debit = Total Credit
```

API returns `400 Bad Request` if not balanced.

### 📦 Standard Chart of Accounts

Included seed data (**25 accounts**):
- **1** — دارایی‌ها (Assets)
  - **11** — دارایی‌های جاری
    - **1101** صندوق، **1102** بانک، **1103** حساب‌های دریافتنی، **1104** موجودی کالا
  - **12** — دارایی‌های ثابت
    - **1201** تجهیزات
- **2** — بدهی‌ها (Liabilities)
  - **21** — بدهی‌های جاری
    - **2101** حساب‌های پرداختنی، **2102** حقوق پرداختنی
- **3** — سرمایه (Equity)
  - **3101** سرمایه اولیه، **3102** سود و زیان انباشته
- **4** — درآمدها (Revenue)
  - **4101** درآمد فروش، **4102** درآمد خدمات
- **5** — هزینه‌ها (Expenses)
  - **51** — هزینه‌های عملیاتی
    - **5101** حقوق، **5102** اجاره، **5103** آب و برق، **5104** اداری

---

## 🚀 Quick Start

### Prerequisites

- .NET 8 SDK
- Docker Desktop (optional, for containerized run)

### Option 1: Run with Docker (Recommended)

```bash
git clone https://github.com/omid-sakaki-ghazvini/aspnet-accounting-showcase.git
cd aspnet-accounting-showcase
docker-compose up --build
```

API will be available at: **`http://localhost:5000`**
Swagger UI: **`http://localhost:5000/swagger`**

### Option 2: Run Locally

```bash
# Clone
git clone https://github.com/omid-sakaki-ghazvini/aspnet-accounting-showcase.git
cd aspnet-accounting-showcase

# Build
dotnet build

# Run
cd Accounting.Api
dotnet run
```

API will be available at: **`http://localhost:5232`**

---

## 🔌 API Endpoints

### Accounts

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/accounts` | List all accounts |
| GET | `/api/accounts/{id}` | Get account by ID |
| POST | `/api/accounts` | Create new account |
| DELETE | `/api/accounts/{id}` | Delete account |

**Example: Create Account**
```json
POST /api/accounts
{
  "code": "1105",
  "name": "تنخواه گردان",
  "type": 1,
  "parentAccountId": 2,
  "description": "تنخواه گردان اداری"
}
```

### Invoices

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/invoices` | List all invoices |
| GET | `/api/invoices/{id}` | Get invoice by ID |
| POST | `/api/invoices` | Create new invoice |
| DELETE | `/api/invoices/{id}` | Delete invoice |

**Example: Create Invoice**
```json
POST /api/invoices
{
  "taxAmount": 900000,
  "notes": "فاکتور نمونه",
  "items": [
    { "description": "خدمات مشاوره", "quantity": 1, "unitPrice": 5000000 },
    { "description": "پشتیبانی ماهانه", "quantity": 2, "unitPrice": 2000000 }
  ]
}
```

### Journal Entries

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/journal-entries` | List all journal entries |
| POST | `/api/journal-entries` | Create new journal entry |
| DELETE | `/api/journal-entries/{id}` | Delete journal entry |

**Example: Create Balanced Journal Entry**
```json
POST /api/journal-entries
{
  "description": "فروش نقدی",
  "reference": "INV-20261005-0001",
  "lines": [
    { "accountId": 3, "debit": 1000000, "credit": 0 },
    { "accountId": 17, "debit": 0, "credit": 1000000 }
  ]
}
```

---

## 🛠️ Tech Stack

| Category | Technology |
|----------|-----------|
| **Framework** | ASP.NET Core 8.0 |
| **ORM** | Entity Framework Core 8.0.10 |
| **Database** | SQLite (dev) / PostgreSQL-ready |
| **API Style** | Minimal API |
| **Documentation** | Swagger / OpenAPI |
| **Containerization** | Docker + Docker Compose |
| **Architecture** | Clean Architecture |

---

## 🌍 Language

- **UI/API**: Persian (فارسی) — with UTF-8 support
- **Documentation**: Bilingual (English/Persian)

---

## 📄 License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.

---

## 👨‍💻 Author

**Omid Sakaki** (امید سکاکی)

- 🎯 AI Architect | Senior Machine Learning Engineer
- 🔗 GitHub: [@omid-sakaki-ghazvini](https://github.com/omid-sakaki-ghazvini)
- 📧 Email: dr.omid.sakaki@gmail.com

---

<p align="center">
  Built with ❤️ using ASP.NET Core 8
</p>
