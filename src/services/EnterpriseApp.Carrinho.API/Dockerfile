FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/services/EnterpriseApp.Carrinho.API/EnterpriseApp.Carrinho.API.csproj", "src/services/EnterpriseApp.Carrinho.API/"]
RUN dotnet restore "src/services/EnterpriseApp.Carrinho.API/EnterpriseApp.Carrinho.API.csproj"
COPY . .
WORKDIR "/src/src/services/EnterpriseApp.Carrinho.API"
RUN dotnet build "EnterpriseApp.Carrinho.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EnterpriseApp.Carrinho.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnterpriseApp.Carrinho.API.dll"]