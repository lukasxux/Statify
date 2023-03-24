# Build container. Load full SDK as base image.
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# TODO: Adapt the directories!
COPY Statify.Application Statify.Application
COPY Statify.Webapi      Statify.Webapi

# Compile the app
RUN dotnet restore "Statify.Webapi"
RUN dotnet build   "Statify.Webapi" -c Release -o /app/build
RUN dotnet publish "Statify.Webapi" -c Release -o /app/publish /p:UseAppHost=false

# App container. Only needs runtime (smaller image)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
EXPOSE 80
EXPOSE 443
WORKDIR /app

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Statify.Webapi.dll"]