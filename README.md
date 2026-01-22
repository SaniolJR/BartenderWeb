# BartenderWeb

## Opis projektu

BartenderWeb to aplikacja webowa stworzona, aby ułatwić zarządzanie i odkrywanie przepisów na drinki.

**Obecne funkcjonalności:**

- Dodawanie własnych przepisów na drinki.
- Przeglądanie i zaawansowane wyszukiwanie drinków (filtrowanie, paginacja).
- Dodawanie składników do bazy.

**Roadmapa (Planowane funkcjonalności):**

- [ ] **System kont:** Logowanie i rejestracja użytkowników (dodawanie treści tylko dla zalogowanych).
- [ ] **Interakcje:** Możliwość dodawania opinii i ocen.
- [ ] **Personalizacja:** Lista "Ulubione" dla każdego użytkownika.
- [ ] **Media:** Przechowywanie zdjęć drinków.
- [ ] **Panel Administratora:** Weryfikacja nowych drinków, zarządzanie składnikami i moderacja treści.

## Architektura

Projekt realizowany w oparciu o **Clean Architecture** z wyraźnym podziałem na warstwy:

- **Domain:** Encje biznesowe i interfejsy.
- **Application:** Logika aplikacji, DTO, mapowanie.
- **Infrastructure:** Dostęp do danych, implementacja repozytoriów, konfiguracja DB.
- **Presentation:** API (Kontrolery) i konfiguracja serwera.

## Technologie

### Backend

- **Framework:** .NET 8 (ASP.NET Core)
- **Baza danych:** MySQL
- **ORM:** Entity Framework Core
- **Inne:** AutoMapper, Serilog, Swagger, xUnit

### Frontend

- **Framework:** React + TypeScript
- **UI:** MaterialUI

## Struktura katalogów

```text
proj-zalicz-ii-2025/
│
├── backend/
│   ├── CA_Domain/         # Encje domenowe, interfejsy
│   ├── CA_Application/    # Serwisy, DTO, profile mapowania
│   ├── CA_Infrastructure/ # DbContext, Repozytoria, Migracje, Testy
│   └── CA_Presentation/   # Endpointy, Program.cs
│
├── frontend/              # Aplikacja kliencka (React)
│
├── README.md
└── Project Plan.txt
```

### Testy:

Testy jednostkowe i integracyjne znajdują się w warstwie CA_Infrastructure.
Decyzja projektowa: Testy zostały umieszczone w tej warstwie, ponieważ zawiera ona kluczową i najbardziej złożoną logikę dotyczącą budowania zapytań do bazy danych (zaawansowane filtrowanie, paginacja). Celem było zapewnienie poprawności zwracanych danych oraz optymalizacja zapytań SQL, aby zminimalizować ilość danych przesyłanych z serwera SQL do aplikacji.

### Jak uruchomić projekt

## Wymagania wstępne:

    -.NET SDK 8.0
    -Node.js & npm
    -Serwer MySQL

## Kroki

1. Upewnij się, że masz utworzoną pustą bazę danych w MySQL.
   Skonfiguruj Connection String w pliku backend/CA_Presentation/appsettings.json.
   Wykonaj migrację z poziomu folderu backend/CA_Presentation:
   dotnet ef database update --project ../CA_Infrastructure

2. Uruchom serwer API:
   cd backend/CA_Presentation
   dotnet run

3. Dodać do folderu frontend plik .env z zawartością;
   VITE_API_URL={TUTAJ WPISZ ADRES HTTP BACKENDU}
4. W nowym oknie terminala uruchom aplikację kliencką:
   cd frontend
   npm install
   npm run dev

Autor:
Mateusz Sadowski
mateusz.sadowski04@wp.pl
