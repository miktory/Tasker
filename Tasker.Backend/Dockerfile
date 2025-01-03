# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 7138

# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Tasker.WebApi/Tasker.WebApi.csproj", "Tasker.WebApi/"]
COPY ["Messaging.Kafka/Tasker.Messaging.Kafka.csproj", "Messaging.Kafka/"]
COPY ["Tasker.Application/Tasker.Application.csproj", "Tasker.Application/"]
COPY ["Tasker.Domain/Tasker.Domain.csproj", "Tasker.Domain/"]
COPY ["Tasker.Shared/Tasker.Shared.csproj", "Tasker.Shared/"]
COPY ["Tasker.Persistence/Tasker.Persistence.csproj", "Tasker.Persistence/"]
RUN dotnet restore "./Tasker.WebApi/Tasker.WebApi.csproj"
COPY . .
WORKDIR "/src/Tasker.WebApi"
RUN dotnet build "./Tasker.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Tasker.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasker.WebApi.dll"]