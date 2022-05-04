FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./api gateways/EnterpriseApp.BFF.Compras/EnterpriseApp.BFF.Compras.csproj", "api gateways/EnterpriseApp.BFF.Compras/"]
COPY ["./building blocks/EnterpriseApp.Core/EnterpriseApp.Core.csproj", "building blocks/EnterpriseApp.Core/"]
COPY ["./building blocks/EnterpriseApp.API.Core/EnterpriseApp.API.Core.csproj", "building blocks/EnterpriseApp.API.Core/"]
COPY ["./building blocks/EnterpriseApp.MessageBus/EnterpriseApp.MessageBus.csproj", "building blocks/EnterpriseApp.MessageBus/"]
RUN dotnet restore "api gateways/EnterpriseApp.BFF.Compras/EnterpriseApp.BFF.Compras.csproj"
COPY . .
WORKDIR "api gateways/EnterpriseApp.BFF.Compras"
RUN dotnet build "EnterpriseApp.BFF.Compras.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EnterpriseApp.BFF.Compras.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnterpriseApp.BFF.Compras.dll"]