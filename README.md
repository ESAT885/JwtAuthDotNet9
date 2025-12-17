JWT AUTH DOTNET 9

API DENEMESI
https://localhost:44386/scalar/v1

KULLANILAN TEKNOLOJILER
- .NET 9 / ASP.NET Core Web API
- Entity Framework Core (SQL Server)
- JWT Authentication
- FluentValidation
- ASP.NET Core Rate Limiting
- Scalar (OpenAPI UI)
- Middleware tabanli Exception Handling

JWT KIMLIK DOGRULAMA
JWT Bearer Authentication kullanilmaktadir.
Issuer, Audience, Lifetime ve Signing Key dogrulamasi yapilir.

MIGRATION KOMUTLARI
Add-Migration migration_ismi -OutputDir Data/Migrations
Update-Database

RATE LIMITING
Global Limiter:
- 100 istek / 1 dakika
- Queue Limit: 10
- 429 Too Many Requests

Login Endpoint:
- 5 istek / 1 dakika
- Queue Limit: 2

GLOBAL EXCEPTION MIDDLEWARE
- Tum hatalar merkezi olarak yakalanir
- Standart response formatinda doner
- traceId ile izlenebilir

VERITABANI
- Entity Framework Core
- SQL Server

APPSETTINGS AYARLARI
```json
{
  "AppSettings": {
    "Token": "YOUR_SECRET_KEY",
    "Issuer": "MyCompany.AuthService",
    "Audience": "MyCompany.WebApi"
  },
  "ConnectionStrings": {
    "UserDatabase": "YOUR_CONNECTION_STRING"
  }
}
```

PROJEYI CALISTIRMA
dotnet restore
dotnet run
