FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Restaurant.Domain/Restaurant.Domain.csproj Restaurant.Domain/
COPY Restaurant.Application/Restaurant.Application.csproj Restaurant.Application/
COPY Restaurant.Infra/Restaurant.Infra.csproj Restaurant.Infra/
COPY Restaurant.API/Restaurant.API.csproj Restaurant.API/

RUN dotnet restore Restaurant.API/Restaurant.API.csproj

COPY . .
WORKDIR /src/Restaurant.API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5202

ENTRYPOINT ["dotnet", "Cep.API.dll"]