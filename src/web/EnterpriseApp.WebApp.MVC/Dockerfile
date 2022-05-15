FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./web/EnterpriseApp.WebApp.MVC/EnterpriseApp.WebApp.MVC.csproj", "web/EnterpriseApp.WebApp.MVC/"]
COPY ["./building blocks/EnterpriseApp.Core/EnterpriseApp.Core.csproj", "building blocks/EnterpriseApp.Core/"]
COPY ["./building blocks/EnterpriseApp.API.Core/EnterpriseApp.API.Core.csproj", "building blocks/EnterpriseApp.API.Core/"]
RUN dotnet restore "web/EnterpriseApp.WebApp.MVC/EnterpriseApp.WebApp.MVC.csproj"
COPY . .
WORKDIR "web/EnterpriseApp.WebApp.MVC"
RUN dotnet build "EnterpriseApp.WebApp.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EnterpriseApp.WebApp.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnterpriseApp.WebApp.MVC.dll"]