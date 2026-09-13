# GameStore API

Proyek backend RESTful API sederhana untuk manajemen katalog game dan genre menggunakan **ASP.NET Core Minimal APIs**, **Entity Framework Core**, dan **SQLite**.

---

## 🚀 Fitur Utama

- **Minimal APIs**: Endpoint ringan dan terstruktur menggunakan `MapGroup`.
- **Entity Framework Core (SQLite)**: ORM untuk akses dan manipulasi database.
- **Auto Migration & Seeding**: Database otomatis menerapkan migrasi dan mengisi data awal (seeding) genre saat aplikasi berjalan.
- **Validasi Data**: Menggunakan Data Annotations (`[Required]`, `[StringLength]`, `[Range]`).
- **DTO Pattern**: Pemisahan model entity database dan model pertukaran data (Data Transfer Objects).

---

## 🛠️ Tech Stack

- **Framework**: .NET 10 / ASP.NET Core
- **Database**: SQLite (`GameStore.db`)
- **ORM**: Microsoft Entity Framework Core 10
  - `Microsoft.EntityFrameworkCore.Sqlite`
  - `Microsoft.EntityFrameworkCore.Design`

---

## 📁 Struktur Proyek

```text
GameStore/
├── GameStore.sln
├── README.md
└── GameStore.Api/
    ├── Data/
    │   ├── DataExtensions.cs          # Extension method untuk migrasi DB & seeding genre awal
    │   ├── GameStoreContext.cs        # EF Core DbContext untuk DbSet Game dan Genre
    │   └── Migrations/                # File migrasi database EF Core
    ├── Dtos/
    │   ├── CreateGameDto.cs           # DTO untuk membuat game baru (dengan validasi input)
    │   ├── UpdateGameDto.cs           # DTO untuk memperbarui data game
    │   ├── GameDetailsDto.cs          # DTO untuk detail satu game (GenreId)
    │   ├── GameSuummaryDto.cs         # DTO untuk daftar/summary game (Nama Genre)
    │   └── GenreDto.cs                # DTO representasi data genre
    ├── Endpoints/
    │   ├── GamesEndpoints.cs          # Routing & handler endpoint `/games` (CRUD)
    │   └── GenresEndpoints.cs         # Routing & handler endpoint `/genres` (GET)
    ├── Models/
    │   ├── Game.cs                    # Entitas database untuk Game
    │   └── Genre.cs                   # Entitas database untuk Genre
    ├── appsettings.json               # Konfigurasi aplikasi & Connection Strings
    ├── games.http                     # File HTTP request untuk pengujian endpoint
    └── Program.cs                     # Entry point aplikasi & konfigurasi middleware/pipeline
```

---

## 📡 Daftar Endpoint API

### 1. Games (`/games`)

| Method | Endpoint | Deskripsi | Status Code Response |
|---|---|---|---|
| `GET` | `/games` | Mengambil seluruh daftar ringkasan game beserta genre | `200 OK` |
| `GET` | `/games/{id}` | Mengambil detail spesifik game berdasarkan ID | `200 OK`, `404 Not Found` |
| `POST` | `/games` | Menambahkan data game baru | `201 Created`, `400 Bad Request` |
| `PUT` | `/games/{id}` | Memperbarui data game berdasarkan ID | `200 OK`, `404 Not Found`, `400 Bad Request` |
| `DELETE` | `/games/{id}` | Menghapus game berdasarkan ID | `204 NoContent` |

#### Format Body untuk POST (`CreateGameDto`):
```json
{
  "title": "The Witcher 3: Wild Hunt",
  "description": "Open world action RPG",
  "genreId": 1,
  "price": 39.99,
  "releaseDate": "2015-05-19"
}
```

#### Format Body untuk PUT (`UpdateGameDto`):
```json
{
  "title": "The Witcher 3: Wild Hunt - Complete Edition",
  "description": "Open world action RPG with all DLCs",
  "genreId": 1,
  "price": 49.99,
  "releaseDate": "2015-05-19"
}
```

---

### 2. Genres (`/genres`)

| Method | Endpoint | Deskripsi | Status Code Response |
|---|---|---|---|
| `GET` | `/genres` | Mengambil semua genre game yang tersedia | `200 OK` |

Default Seed Data:
- Action (ID: 1)
- Racing (ID: 2)
- Sports (ID: 3)
- Adventure (ID: 4)

---

## ⚙️ Cara Menjalankan Proyek

1. **Prasyarat**:
   - Pastikan telah menginstal [.NET SDK 10](https://dotnet.microsoft.com/download).

2. **Jalankan Aplikasi**:
   ```bash
   dotnet run --project GameStore.Api
   ```

3. **Akses API**:
   - HTTP: `http://localhost:5172`
   - HTTPS: `https://localhost:7256`

> **Catatan**: Saat aplikasi pertama kali dijalankan, migrasi EF Core akan diterapkan otomatis dan membuat file database `GameStore.db` serta menginisialisasi genre bawaan.

---

## 🧪 Pengujian Endpoint

Anda dapat menguji semua endpoint menggunakan file `GameStore.Api/games.http` secara langsung melalui:
- **VS Code**: Menggunakan ekstensi *REST Client*.
- **Visual Studio**: Menggunakan fitur bawaan *.http file editor*.
- **cURL / Postman**: Dengan mengarahkan request ke URL yang sesuai.
