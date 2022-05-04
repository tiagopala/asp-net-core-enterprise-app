FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./services/EnterpriseApp.Pagamento.API/EnterpriseApp.Pagamento.API.csproj", "services/EnterpriseApp.Pagamento.API/"]
COPY ["./services/External.Payments.Gateway.Payme/External.Payments.Gateway.Payme.csproj", "services/EnterpriseApp.Pagamento.API/"]
COPY ["./building blocks/EnterpriseApp.Core/EnterpriseApp.Core.csproj", "building blocks/EnterpriseApp.Core/"]
COPY ["./building blocks/EnterpriseApp.API.Core/EnterpriseApp.API.Core.csproj", "building blocks/EnterpriseApp.API.Core/"]
COPY ["./building blocks/EnterpriseApp.MessageBus/EnterpriseApp.MessageBus.csproj", "building blocks/EnterpriseApp.MessageBus/"]
RUN dotnet restore "services/EnterpriseApp.Pagamento.API/EnterpriseApp.Pagamento.API.csproj"
COPY . .
WORKDIR "services/EnterpriseApp.Pagamento.API"
RUN dotnet build "EnterpriseApp.Pagamento.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EnterpriseApp.Pagamento.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnterpriseApp.Pagamento.API.dll"]