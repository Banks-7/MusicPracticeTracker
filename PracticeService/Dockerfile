FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

COPY ["PracticeService/PracticeService.csproj", "PracticeService/"]
RUN dotnet restore "PracticeService/PracticeService.csproj"

COPY . .
WORKDIR /source/PracticeService
RUN dotnet publish "PracticeService.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
RUN mkdir -p /data && chown "$APP_UID:$APP_UID" /data
COPY --from=build /app/publish .

EXPOSE 8080
USER $APP_UID
ENTRYPOINT ["dotnet", "PracticeService.dll"]