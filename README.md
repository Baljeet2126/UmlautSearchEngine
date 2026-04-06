# 🚀 UmlautSearchEngine (Clean Architecture Console App)

A clean architecture console application that performs umlaut-insensitive name search using C#, SQLite, and optimized query processing.

## 🧠 Overview

This project demonstrates:

Clean Architecture (Domain, Application, Infrastructure)
Umlaut transformation logic (AE → Ä, SS → ß)
Variation generation (multiple combinations)
SQL query optimization using IN clause
SQLite persistence with indexing
Dependency Injection using Microsoft.Extensions
Console-based execution flow


## 🧠 Architecture Flow

```

UmlautSearchEngine
├── Domain
│   ├── Services (VariationGenerator, UmlautConverter)
│   ├── Interfaces
│   └── Configuration (VariationConfig)
├── Application
│   ├── Services (NameProcessingService)
│   ├── Builders (QueryBuilder)
│   └── DTOs
├── Infrastructure
│   ├── Database (DatabaseInitializer)
│   ├── Repositories (DataRepository)
│   └── Providers (UmlautRuleProvider)
├── ConsoleApp
│   └── Program.cs, AppRunner
└── Tests (optional)

```
---

## ⚙️ Features
✅ Umlaut Conversion

Converts:

AE → Ä
OE → Ö
UE → Ü
SS → ß

---

## ✅ Variation Generation

Generates all possible valid combinations:
```
RUESSWURM →
- RUESSWURM
- RUEßWURM
- RÜSSWURM
- RÜßWURM

```
## ✅ Optimized SQL Query

Uses efficient IN clause:
```
SELECT * FROM tbl_phonebook 
WHERE lastname IN (...);

```

## 📁 Project Structure
```
UmlautSearchEngine
├── UmlautSearchEngine.Domain
├── UmlautSearchEngine.Application
├── UmlautSearchEngine.Infrastructure
├── UmlautSearch.ConsoleApp
└── README.md

```

## 🗄️ Database

```
Table Structure
CREATE TABLE tbl_phonebook (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    lastname TEXT NOT NULL UNIQUE
);
Index
CREATE INDEX IF NOT EXISTS idx_lastname 
ON tbl_phonebook(lastname);

```

## ⚙️ How It Works
```
1️⃣ Input
RUESSWURM

2️⃣ Processing
Normalize to uppercase

Generate variations
Remove duplicates

3️⃣ SQL Query
SELECT * FROM tbl_phonebook 
WHERE lastname IN ('RUESSWURM', 'RUEßWURM', 'RÜSSWURM', 'RÜßWURM');

4️⃣ Output

Input: RUESSWURM


Variations:

- RUESSWURM
- RUEßWURM
- RÜSSWURM
- RÜßWURM

Matches:
- RUESSWURM
- RUEßWURM
- RÜSSWURM
- RÜßWURM

```

## 🚀 Quick Start
Prerequisites
.NET 8 SDK
Git
## Clone & Run
```
git clone https://github.com/Baljeet2126/UmlautSearchEngine.git
cd UmlautSearchEngine
dotnet restore
dotnet run --project UmlautSearch.ConsoleApp
```
