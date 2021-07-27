FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Dotnet5Flash.Api.csproj", "./"]
RUN dotnet restore "Dotnet5Flash.Api.csproj"
COPY . .
RUN dotnet publish "Dotnet5Flash.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Dotnet5Flash.Api.dll"]
