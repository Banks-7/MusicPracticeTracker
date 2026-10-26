FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

COPY ["ProgressService/ProgressService.csproj", "ProgressService/"]
RUN dotnet restore "ProgressService/ProgressService.csproj"

COPY . .
WORKDIR /source/ProgressService
RUN dotnet publish "ProgressService.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
USER $APP_UID
ENTRYPOINT ["dotnet", "ProgressService.dll"]