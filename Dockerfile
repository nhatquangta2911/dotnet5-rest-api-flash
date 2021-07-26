FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["dotnet-5-rest-api-flash.csproj", "./"]
RUN dotnet restore "dotnet-5-rest-api-flash.csproj"
COPY . .
RUN dotnet publish "dotnet-5-rest-api-flash.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "dotnet-5-rest-api-flash.dll"]
