FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./services/EnterpriseApp.Pedido.API/EnterpriseApp.Pedido.API.csproj", "services/EnterpriseApp.Pedido.API/"]
COPY ["./services/EnterpriseApp.Pedido.Application/EnterpriseApp.Pedido.Application.csproj", "services/EnterpriseApp.Pedido.Application/"]
COPY ["./services/EnterpriseApp.Pedido.Domain/EnterpriseApp.Pedido.Domain.csproj", "services/EnterpriseApp.Pedido.Domain/"]
COPY ["./services/EnterpriseApp.Pedido.Infrastructure/EnterpriseApp.Pedido.Infrastructure.csproj", "services/EnterpriseApp.Pedido.Infrastructure/"]
COPY ["./building blocks/EnterpriseApp.Core/EnterpriseApp.Core.csproj", "building blocks/EnterpriseApp.Core/"]
COPY ["./building blocks/EnterpriseApp.API.Core/EnterpriseApp.API.Core.csproj", "building blocks/EnterpriseApp.API.Core/"]
COPY ["./building blocks/EnterpriseApp.MessageBus/EnterpriseApp.MessageBus.csproj", "building blocks/EnterpriseApp.MessageBus/"]
RUN dotnet restore "services/EnterpriseApp.Pedido.API/EnterpriseApp.Pedido.API.csproj"
COPY . .
WORKDIR "services/EnterpriseApp.Pedido.API"
RUN dotnet build "EnterpriseApp.Pedido.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EnterpriseApp.Pedido.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnterpriseApp.Pedido.API.dll"]