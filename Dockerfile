FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5202

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["API/RestaurantAPI.csproj", "RestaurantAPI/"]
RUN dotnet restore "RestaurantAPI/RestaurantAPI.csproj"

WORKDIR "/src/RestaurantAPI"
COPY . .

RUN dotnet build "RestaurantAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestaurantAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestaurantAPI.dll"]




