cd .\Biblioteca.API\

rmdir ".\Migrations\" /s /q
del .\bibliotecadb.db

dotnet ef migrations add InitialCreate -c AppDbContext
dotnet ef database update -c AppDbContext