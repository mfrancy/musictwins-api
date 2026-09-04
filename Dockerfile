FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["musictwins-api.csproj", "."]
RUN dotnet restore "musictwins-api.csproj"

COPY . .
RUN dotnet publish "musictwins-api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "musictwins-api.dll"]