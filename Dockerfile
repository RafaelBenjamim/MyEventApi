FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia só o .csproj (ele já está na raiz, sem subpasta)
COPY MyEventApi.csproj .

# Restaura os pacotes NuGet
RUN dotnet restore MyEventApi.csproj

# Copia todo o resto do código
COPY . .

# Compila e publica
RUN dotnet publish -c Release -o /app/publish

# ---------- ETAPA 2: RUNTIME ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "MyEventApi.dll"]