# Address Book REST API

Address Book REST API uygulaması - .NET 9 Web API

## API Endpoints

- `GET /api/addresses` - Tüm adresleri getir
- `GET /api/addresses/{id}` - ID'ye göre adres getir
- `POST /api/addresses` - Yeni adres oluştur
- `PUT /api/addresses/{id}` - Adresi güncelle
- `DELETE /api/addresses/{id}` - Adresi sil
- `GET /api/addresses/search?q={term}` - Adresleri ara

## Model

```json
{
  "id": 1,
  "name": "Cemil",
  "city": "Fahreci ",
  "email": "cemilfahreci@gmail.com",
  "phone": "530000000"
}
```

## Swagger

- Swagger UI: `/swagger`

## Çalıştırma

```bash
cd AddressBookApi
dotnet run
```

## Teknolojiler

- .NET 9.0
- ASP.NET Core Web API
- Swashbuckle.AspNetCore (Swagger)
- In-Memory Database

